namespace G_P2026.Data.DTOs.Auth
{
	public class UpdateUserDto
	{
		public string? Id { get; set; }
		public string? UserName { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Linkedin { get; set; }
		public string? Field { get; set; }
		public string? Bio { get; set; }
		public string? Status { get; set; }
		public string? CvFileName { get; set; }
		public string? University { get; set; }
		public string? Skills { get; set; }
		public bool? IsActive { get; set; }
		public int? Paid { get; set; }
	}
}
