using G_P2026.Core.Bases;
using G_P2026.Data.DTOs.Auth;
using MediatR;

namespace G_P2026.Core.Features.Authentications.Commands.Models
{
	public class LoginModel : IRequest<Response<AuthResponseDto>>
	{
		public string EmailOrUsername { get; set; }
		public string Password { get; set; }
	}
}
