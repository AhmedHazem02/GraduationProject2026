using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Infastructure.Context
{
	public class ApplicationUser : IdentityUser
	{
		// Mentor fields (nullable لو مش كل المستخدمين Mentor)
		public string? Linkedin { get; set; }
		public string? Field { get; set; }
		public byte[]? CvFile { get; set; }
		public string? CvFileName { get; set; }
		public string? Bio { get; set; }
		public string? Status { get; set; }

		// New fields
		public string? University { get; set; }
		public string? Skills { get; set; }   // stored as JSON: ["C#","Python"]
		public bool? IsActive { get; set; }
		public int? Paid { get; set; }
	}
}
