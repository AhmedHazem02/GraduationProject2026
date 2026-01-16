namespace G_P2026.Data.DTOs.Auth
{
	public class AuthResponseDto
	{
		public string? UserId { get; set; }
		public string?UserName { get; set; }
		public string? Email { get; set; }
		public string? Token { get; set; }
		public DateTime ExpiresOn { get; set; }
		public List<string> Roles { get; set; } = new List<string>();
		public string? Status { get; set; }
		public string? CvFileName { get; set; }
	}
}
