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
			CreateMap<UpdateUserModel, ApplicationUser>()
				.ForMember(dest => dest.CvFile, opt => opt.MapFrom(src => 
					src.CvFile != null ? ConvertFormFileToByteArray(src.CvFile) : null))
				.ForMember(dest => dest.CvFileName, opt => opt.MapFrom(src => 
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

