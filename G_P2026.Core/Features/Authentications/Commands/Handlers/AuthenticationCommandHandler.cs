using AutoMapper;
using G_P2026.Core.Bases;
using G_P2026.Core.Features.Authentications.Commands.Models;
using G_P2026.Data.DTOs.Auth;
using G_P2026.Services.Interfaces;
using MediatR;

namespace G_P2026.Core.Features.Authentications.Commands.Handlers
{
	public class AuthenticationCommandHandler : ResponseHandler,
		IRequestHandler<RegisterModel, Response<AuthResponseDto>>,
		IRequestHandler<LoginModel, Response<AuthResponseDto>>,
		IRequestHandler<LogoutModel, Response<bool>>,
		IRequestHandler<ConfirmEmailModel, Response<bool>>,
		IRequestHandler<ResendConfirmEmailModel, Response<bool>>,
		IRequestHandler<ForgotPasswordModel, Response<bool>>,
		IRequestHandler<ResetPasswordModel, Response<bool>>
	{
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;

		public AuthenticationCommandHandler(IAuthService authService, IMapper mapper)
		{
			_authService = authService;
			_mapper = mapper;
		}

		public async Task<Response<AuthResponseDto>> Handle(RegisterModel request, CancellationToken cancellationToken)
		{
			try
			{
				var registerDto = _mapper.Map<RegisterDto>(request);
				var result = await _authService.RegisterAsync(registerDto, request.BaseUrl ?? "");
				return Success(result);
			}
			catch (Exception ex)
			{
				return BadRequest<AuthResponseDto>(ex.Message);
			}
		}

		public async Task<Response<AuthResponseDto>> Handle(LoginModel request, CancellationToken cancellationToken)
		{
			try
			{
				var loginDto = _mapper.Map<LoginDto>(request);
				
				var result = await _authService.LoginAsync(loginDto);
				return Success(result);
			}
			catch (Exception ex)
			{
				return Unauthorized<AuthResponseDto>(ex.Message);
			}
		}

		public async Task<Response<bool>> Handle(LogoutModel request, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _authService.LogoutAsync(request.UserId);
				return Success(result);
			}
			catch (Exception ex)
			{
				return BadRequest<bool>(ex.Message);
			}
		}

		public async Task<Response<bool>> Handle(ConfirmEmailModel request, CancellationToken cancellationToken)
		{
			try
			{
				// Decode the token (it was URL-encoded in the email link)
				var decodedToken = System.Net.WebUtility.UrlDecode(request.Token);
				var result = await _authService.ConfirmEmailAsync(request.UserId, decodedToken);
				return Success(result);
			}
			catch (Exception ex)
			{
				return BadRequest<bool>(ex.Message);
			}
		}

		public async Task<Response<bool>> Handle(ForgotPasswordModel request, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _authService.ForgotPasswordAsync(request.Email, request.BaseUrl);
				return Success(result);
			}
			catch (Exception ex)
			{
				return BadRequest<bool>(ex.Message);
			}
		}

		public async Task<Response<bool>> Handle(ResendConfirmEmailModel request, CancellationToken cancellationToken)
		{
			try
			{
				var result = await _authService.ResendConfirmEmailAsync(request.Email, request.BaseUrl);
				return Success(result);
			}
			catch (Exception ex)
			{
				return BadRequest<bool>(ex.Message);
			}
		}

		public async Task<Response<bool>> Handle(ResetPasswordModel request, CancellationToken cancellationToken)
		{
			try
			{
				// Decode the token (it was URL-encoded in the reset email link)
				var decodedToken = System.Net.WebUtility.UrlDecode(request.Token);
				request.Token = decodedToken;
				var resetDto = _mapper.Map<ResetPasswordDto>(request);
				var result = await _authService.ResetPasswordAsync(resetDto);
				return Success(result);
			}
			catch (Exception ex)
			{
				return BadRequest<bool>(ex.Message);
			}
		}
	}
}
