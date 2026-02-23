using G_P2026.Core.Bases;
using G_P2026.Data.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace G_P2026.Core.Features.Authentications.Commands.Models
{
	public class RegisterModel : IRequest<Response<AuthResponseDto>>
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Linkedin { get; set; }
		public string? Field { get; set; }
		public string? Bio { get; set; }
		public IFormFile? CvFile { get; set; }
		public string? Role { get; set; }

		// New fields
		public List<string>? Skills { get; set; }
		public string? University { get; set; }
		public bool? IsActive { get; set; }
		public int? Paid { get; set; }

		[JsonIgnore]
		public string? BaseUrl { get; set; }
	}
}
