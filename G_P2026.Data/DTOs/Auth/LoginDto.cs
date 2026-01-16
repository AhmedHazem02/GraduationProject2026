using System.ComponentModel.DataAnnotations;

namespace G_P2026.Data.DTOs.Auth
{
	public class LoginDto
	{
		[Required(ErrorMessage = "Email or Username is required")]
		public string? EmailOrUsername { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string? Password { get; set; }
	}
}
