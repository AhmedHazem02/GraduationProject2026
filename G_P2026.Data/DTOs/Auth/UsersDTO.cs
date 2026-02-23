using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Data.DTOs.Auth
{
	public class UsersDTO
	{
		public string? UserId { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public bool EmailConfirmed { get; set; }
		public string? PhoneNumber { get; set; }
		public List<string> Roles { get; set; } = new List<string>();
		public string? Status { get; set; }

		// Mentor / Profile fields
		public string? Linkedin { get; set; }
		public string? Field { get; set; }
		public string? CvFileName { get; set; }
		public string? CvFileBase64 { get; set; }
		public string? Bio { get; set; }
		public string? University { get; set; }
		public string? Skills { get; set; }
		public bool? IsActive { get; set; }
		public int? Paid { get; set; }
	}
}
