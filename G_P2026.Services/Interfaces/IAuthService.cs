using G_P2026.Data.DTOs.Auth;

namespace G_P2026.Services.Interfaces
{
	public interface IAuthService
	{
		Task<AuthResponseDto> RegisterAsync(RegisterDto model, string baseUrl);
		Task<AuthResponseDto> LoginAsync(LoginDto model);
		Task<bool> IsEmailExistsAsync(string email);
		Task<bool> IsUsernameExistsAsync(string username);
		Task<bool> IsEmailConfirmedAsync(string email);

		/// <summary>
		/// Logs out a user by invalidating their refresh token
		/// </summary>
		Task<bool> LogoutAsync(string userId);

		/// <summary>
		/// Confirms user email with the provided token
		/// </summary>
		Task<bool> ConfirmEmailAsync(string userId, string token);

		/// <summary>
		/// Resends email confirmation token to the user's email
		/// </summary>
		Task<bool> ResendConfirmEmailAsync(string email, string baseUrl);

		/// <summary>
		/// Generates and sends password reset token to user's email
		/// </summary>
		Task<bool> ForgotPasswordAsync(string email, string baseUrl);

		/// <summary>
		/// Resets user password using the reset token
		/// </summary>
		Task<bool> ResetPasswordAsync(ResetPasswordDto model);
	}
}
