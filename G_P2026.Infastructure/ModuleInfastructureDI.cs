using G_P2026.Infastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace G_P2026.Infastructure
{
	public static class ModuleInfastructureDI
	{
		public static IServiceCollection AddInfastructureDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			// Configure Identity
			return services;
		}
	}
}
