using G_P2026.Core.Bases;
using G_P2026.Data.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace G_P2026.Core.Features.Users.Command.Models
{
	public class UpdateUserModel : IRequest<Response<UpdateUserDto>>
	{
		public string UserId { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Linkedin { get; set; }
		public string? Field { get; set; }
		public string? Bio { get; set; }
		public string? Status { get; set; }
		public IFormFile? CvFile { get; set; }
	}
}
