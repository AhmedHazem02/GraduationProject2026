using AutoMapper;
using G_P2026.Core.Features.Authentications.Commands.Models;
using G_P2026.Data.DTOs.Auth;

namespace G_P2026.Core.Mapping
{
	public class AuthenticationProfile : Profile
	{
		public AuthenticationProfile()
		{
			CreateMap<RegisterModel, RegisterDto>();
			CreateMap<LoginModel, LoginDto>();
			CreateMap<ResetPasswordModel, ResetPasswordDto>();
		}
	}
}

