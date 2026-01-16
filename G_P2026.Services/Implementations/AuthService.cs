using G_P2026.Data.DTOs.Auth;
using G_P2026.Infastructure.Context;
using G_P2026.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace G_P2026.Services.Implementations
{
	public class AuthService : IAuthService
	{
		// Constants for magic strings
		private const string DefaultRole = "Student";
		private const string DefaultStatus = "Active";
		
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ITokenServices _tokenService;
		private readonly IFileService _fileService;
		private readonly IEmailService _emailService;

		public AuthService(
			UserManager<ApplicationUser> userManager, 
			ITokenServices tokenService, 
			IFileService fileService,
			IEmailService emailService)
		{
			_userManager = userManager;
			_tokenService = tokenService;
			_fileService = fileService;
			_emailService = emailService;
		}

		public async Task<AuthResponseDto> RegisterAsync(RegisterDto model, string baseUrl)
		{
			// Validate input
			if (string.IsNullOrWhiteSpace(model.Email))
				throw new ArgumentException("Email is required", nameof(model.Email));

			if (string.IsNullOrWhiteSpace(model.UserName))
				throw new ArgumentException("Username is required", nameof(model.UserName));

			if (string.IsNullOrWhiteSpace(model.Password))
				throw new ArgumentException("Password is required", nameof(model.Password));

			// Check if email already exists
			if (await IsEmailExistsAsync(model.Email))
			{
				throw new Exception("Email is already registered");
			}

			// Check if username already exists
			if (await IsUsernameExistsAsync(model.UserName))
			{
				throw new Exception("Username is already taken");
			}

			// Convert CV file to byte array if provided
			byte[]? cvFileData = null;
			string? cvFileName = null;
			if (model.CvFile != null && model.CvFile.Length > 0)
			{
				cvFileData = await _fileService.ConvertToByteArrayAsync(model.CvFile);
				cvFileName = model.CvFile.FileName;
			}

			var user = new ApplicationUser
			{
				UserName = model.UserName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				Linkedin = model.Linkedin,
				Field = model.Field,
				Bio = model.Bio,
				CvFile = cvFileData,
				CvFileName = cvFileName,
				Status = DefaultStatus
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				var errors = string.Join(", ", result.Errors.Select(e => e.Description));
				throw new Exception($"Registration failed: {errors}");
			}

			// Assign role based on frontend selection
			var role = string.IsNullOrWhiteSpace(model.Role) ? DefaultRole : model.Role;
			await _userManager.AddToRoleAsync(user, role);

			// Generate Confirmation Token and Send Email
			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			
			// Send email in background to not block the user response
			_ = _emailService.SendConfirmationEmailAsync(user.Email!, user.Id, code, baseUrl);

			return await GenerateAuthResponse(user);
		}

		public async Task<AuthResponseDto> LoginAsync(LoginDto model)
		{
			// Validate input
			if (string.IsNullOrWhiteSpace(model.EmailOrUsername))
				throw new ArgumentException("Email or Username is required", nameof(model.EmailOrUsername));

			if (string.IsNullOrWhiteSpace(model.Password))
				throw new ArgumentException("Password is required", nameof(model.Password));

			// Find user by email or username (optimized: check email format first)
			var isEmail = model.EmailOrUsername.Contains('@');
			var user = isEmail 
				? await _userManager.FindByEmailAsync(model.EmailOrUsername)
				: await _userManager.FindByNameAsync(model.EmailOrUsername);

			if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
			{
				throw new Exception("Email/Username or Password is incorrect");
			}

			// Check if email is confirmed AFTER verifying user exists and password is correct
			if (!user.EmailConfirmed)
			{
				throw new Exception("Please confirm your email before logging in. Check your inbox.");
			}

			return await GenerateAuthResponse(user);
		}

		public async Task<bool> IsEmailExistsAsync(string email)
		{
			return await _userManager.FindByEmailAsync(email) != null;
		}

		public async Task<bool> IsUsernameExistsAsync(string username)
		{
			return await _userManager.FindByNameAsync(username) != null;
		}

		private async Task<AuthResponseDto> GenerateAuthResponse(ApplicationUser user)
		{
			var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
			var token = GenerateJwtToken(user, userRoles);

			return new AuthResponseDto
			{
				UserId = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				Token = token,
				ExpiresOn = DateTime.UtcNow.AddMinutes(_tokenService.GetAccessTokenExpirationMinutes()),
				Roles = userRoles,
				Status = user.Status,
				CvFileName = user.CvFileName
			};
		}

		private string GenerateJwtToken(ApplicationUser user, List<string> roles)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
				new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Sub, user.Id),
				new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
			};

			// Add roles as claims
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			return _tokenService.GenerateAccessToken(claims);
		}

		#region Logout, ConfirmEmail, ForgotPassword, ResetPassword

		public async Task<bool> LogoutAsync(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
				throw new ArgumentException("UserId is required", nameof(userId));

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
				throw new Exception("User not found");

			// Update security stamp to invalidate all existing tokens
			await _userManager.UpdateSecurityStampAsync(user);
			
			return true;
		}

		public async Task<bool> ConfirmEmailAsync(string userId, string token)
		{
			if (string.IsNullOrWhiteSpace(userId))
				throw new ArgumentException("UserId is required", nameof(userId));

			if (string.IsNullOrWhiteSpace(token))
				throw new ArgumentException("Token is required", nameof(token));

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
				throw new Exception("User not found");

			if (user.EmailConfirmed)
				throw new Exception("Email is already confirmed");

			var result = await _userManager.ConfirmEmailAsync(user, token);
			if (!result.Succeeded)
			{
				var errors = string.Join(", ", result.Errors.Select(e => e.Description));
				throw new Exception($"Email confirmation failed: {errors}");
			}

			return true;
		}

		public async Task<bool> ForgotPasswordAsync(string email, string baseUrl)
		{
			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentException("Email is required", nameof(email));

			var user = await _userManager.FindByEmailAsync(email);
			
			// For security reasons, always return success even if user doesn't exist
			// This prevents email enumeration attacks
			if (user == null)
				return true;

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			
			await _emailService.SendPasswordResetEmailAsync(email, token, baseUrl);
			
			return true;
		}

		public async Task<bool> ResetPasswordAsync(ResetPasswordDto model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			if (string.IsNullOrWhiteSpace(model.Email))
				throw new ArgumentException("Email is required", nameof(model.Email));

			if (string.IsNullOrWhiteSpace(model.Token))
				throw new ArgumentException("Token is required", nameof(model.Token));

			if (string.IsNullOrWhiteSpace(model.NewPassword))
				throw new ArgumentException("NewPassword is required", nameof(model.NewPassword));

			if (model.NewPassword != model.ConfirmPassword)
				throw new Exception("Passwords do not match");

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
				throw new Exception("User not found");

			var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
			if (!result.Succeeded)
			{
				var errors = string.Join(", ", result.Errors.Select(e => e.Description));
				throw new Exception($"Password reset failed: {errors}");
			}

			// Update security stamp to invalidate old tokens
			await _userManager.UpdateSecurityStampAsync(user);

			return true;
		}

		#endregion
	}
}
