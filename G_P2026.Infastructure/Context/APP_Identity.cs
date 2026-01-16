using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Infastructure.Context
{
	public class APP_Identity : IdentityDbContext<ApplicationUser, IdentityRole, string>
	{
		public APP_Identity(DbContextOptions<APP_Identity> options) : base(options) { }
	}
}
