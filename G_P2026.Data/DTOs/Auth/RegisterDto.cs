using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace G_P2026.Data.DTOs.Auth
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "Username is required")]
		[StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters", MinimumLength = 3)]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email format")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[StringLength(100, ErrorMessage = "Password must be at least 6 characters", MinimumLength = 6)]
		public string? Password { get; set; }

		[Required(ErrorMessage = "Confirm password is required")]
		[Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
		public string? ConfirmPassword { get; set; }
		[Phone(ErrorMessage = "Invalid phone number format")]
		public string? PhoneNumber { get; set; }

		// Mentor specific fields (optional)
		public string? Linkedin { get; set; }
		public string? Field { get; set; }
		public string? Bio { get; set; }
		public IFormFile? CvFile { get; set; }

		// New fields
		public List<string>? Skills { get; set; }
		public string? University { get; set; }
		public bool? IsActive { get; set; }
		public int? Paid { get; set; }

		[Required(ErrorMessage = "Role is required")]
		public string? Role { get; set; }
	}
}
