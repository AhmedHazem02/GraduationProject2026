using G_P2026.Core.Bases;
using G_P2026.Core.Features.Authentications.Queries.Models;
using G_P2026.Core.Features.Users.Queries.Models;
using G_P2026.Data.DTOs.Auth;
using G_P2026.Services.Interfaces;
using MediatR;

namespace G_P2026.Core.Features.Authentications.Queries.Handlers
{
	public class AuthenticationQueryHandler : ResponseHandler,
		IRequestHandler<CheckEmailExistsModel, Response<bool>>,
		IRequestHandler<CheckUsernameExistsModel, Response<bool>>
		
	{
		private readonly IAuthService _authService;

		public AuthenticationQueryHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<Response<bool>> Handle(CheckEmailExistsModel request, CancellationToken cancellationToken)
		{
			var exists = await _authService.IsEmailExistsAsync(request.Email);
			return Success(exists);
		}

		public async Task<Response<bool>> Handle(CheckUsernameExistsModel request, CancellationToken cancellationToken)
		{
			var exists = await _authService.IsUsernameExistsAsync(request.Username);
			return Success(exists);
		}

		

	}
}
