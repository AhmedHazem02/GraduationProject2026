using G_P2026.Services.Interfaces;
using G_P2026.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace G_P2026.Services
{
	public static class ModuleServiceDependencies
	{
		public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
		{
			// Register Authentication Service
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<ITokenServices, TokenService>();
			services.AddScoped<IFileService, FileService>();
			services.AddScoped<IUsersService, UsersService>();
			services.AddScoped<IEmailService, EmailService>();

			return services;
		}
	}
}

