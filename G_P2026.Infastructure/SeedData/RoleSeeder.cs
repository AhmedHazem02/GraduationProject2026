using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace G_P2026.Infastructure.SeedData
{
	public static class RoleSeeder
	{
		public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			string[] roles = { "Admin", "Mentor", "User", "Student" };

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}
	}
}
