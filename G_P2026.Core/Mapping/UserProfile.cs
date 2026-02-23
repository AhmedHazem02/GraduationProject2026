using AutoMapper;
using G_P2026.Core.Features.Users.Command.Models;
using G_P2026.Data.DTOs.Auth;
using G_P2026.Infastructure.Context;
using Microsoft.AspNetCore.Http;

namespace G_P2026.Core.Mapping
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<ApplicationUser, UsersDTO>();

			// Mapping from UpdateUserModel to ApplicationUser with custom handling for CvFile
			// Null values are ignored so partial updates (PATCH) work correctly
			CreateMap<UpdateUserModel, ApplicationUser>()
				.ForMember(dest => dest.UserName,    opt => { opt.Condition(src => src.UserName != null);    opt.MapFrom(src => src.UserName); })
				.ForMember(dest => dest.Email,       opt => opt.Ignore())
				.ForMember(dest => dest.PhoneNumber, opt => { opt.Condition(src => src.PhoneNumber != null); opt.MapFrom(src => src.PhoneNumber); })
				.ForMember(dest => dest.Linkedin,    opt => { opt.Condition(src => src.Linkedin != null);    opt.MapFrom(src => src.Linkedin); })
				.ForMember(dest => dest.Field,       opt => { opt.Condition(src => src.Field != null);       opt.MapFrom(src => src.Field); })
				.ForMember(dest => dest.Bio,         opt => { opt.Condition(src => src.Bio != null);         opt.MapFrom(src => src.Bio); })
				.ForMember(dest => dest.Status,      opt => { opt.Condition(src => src.Status != null);      opt.MapFrom(src => src.Status); })
				.ForMember(dest => dest.University,  opt => { opt.Condition(src => src.University != null);  opt.MapFrom(src => src.University); })
				.ForMember(dest => dest.Skills,      opt => { opt.Condition(src => src.Skills != null);      opt.MapFrom(src => src.Skills); })
				.ForMember(dest => dest.IsActive,    opt => { opt.Condition(src => src.IsActive.HasValue);   opt.MapFrom(src => src.IsActive); })
				.ForMember(dest => dest.Paid,        opt => { opt.Condition(src => src.Paid.HasValue);       opt.MapFrom(src => src.Paid); })
				.ForMember(dest => dest.CvFile,      opt => opt.MapFrom(src =>
					src.CvFile != null ? ConvertFormFileToByteArray(src.CvFile) : null))
				.ForMember(dest => dest.CvFileName,  opt => opt.MapFrom(src =>
					src.CvFile != null ? src.CvFile.FileName : null));
			
			// Mapping from ApplicationUser to UpdateUserDto (exclude CvFile binary data)
			CreateMap<ApplicationUser, UpdateUserDto>();
			
			CreateMap<ApplicationUser, UpdateUserModel>();
		}

		private static byte[]? ConvertFormFileToByteArray(IFormFile? file)
		{
			if (file == null || file.Length == 0)
				return null;

			using (var memoryStream = new MemoryStream())
			{
				file.CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}
	}
}

