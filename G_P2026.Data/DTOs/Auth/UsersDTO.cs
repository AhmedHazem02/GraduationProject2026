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
		public DateTime ExpiresOn { get; set; }
		public List<string> Roles { get; set; } = new List<string>();
		public string? Status { get; set; }
	}
}
