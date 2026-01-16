using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using G_P2026.Infastructure;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Annotations;
using G_P2026.Infastructure.Context;
using System.IdentityModel.Tokens.Jwt;
using G_P2026.Data.Helpers;


namespace G_P2026.API.Extensions
{
	public static class ServiceRegisteration
	{
		public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration _configuration)
		{
			services.AddIdentity<ApplicationUser, IdentityRole>(option =>
			{
				// Password settings.
				option.Password.RequireDigit = true;
				option.Password.RequireLowercase = true;
				option.Password.RequireNonAlphanumeric = true;
				option.Password.RequireUppercase = true;
				option.Password.RequiredLength = 6;
				option.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				option.Lockout.MaxFailedAccessAttempts = 5;
				option.Lockout.AllowedForNewUsers = true;

				// User settings.
				option.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				option.User.RequireUniqueEmail = true;
				option.SignIn.RequireConfirmedEmail = true;

			}).AddEntityFrameworkStores<APP_Identity>()
			  .AddDefaultTokenProviders();

			//JWT Authentication
			var jwtSettings = new JwtSettings();
			_configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
			services.AddSingleton(jwtSettings);

			// Email Settings - using IOptions pattern for DI
			services.Configure<EmailSetting>(_configuration.GetSection("emailSettings"));



			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
		   .AddJwtBearer(x =>
		   {
			   x.RequireHttpsMetadata = false;
			   x.SaveToken = true;
			   x.TokenValidationParameters = new TokenValidationParameters
			   {
				   NameClaimType = JwtRegisteredClaimNames.Sub,
				   ValidateIssuer = true,
				   ValidIssuers = new[] { jwtSettings.Issuer },
				   ValidateIssuerSigningKey = true,
				   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
				   ValidAudience = jwtSettings.Audience,
				   ValidateAudience = true,
				   ValidateLifetime = true

			   };
		   });







			//Swagger Gn
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Graduate_Project_2026", Version = "v1" });
				c.EnableAnnotations();
				c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = JwtBearerDefaults.AuthenticationScheme
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
				{
				   new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = JwtBearerDefaults.AuthenticationScheme
				}
			},
			Array.Empty<string>()
			}
		   });
			});
			return services;
		}
	}
}
