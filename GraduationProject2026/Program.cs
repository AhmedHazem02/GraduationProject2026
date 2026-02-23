using G_P2026.API.Extensions;
using G_P2026.Core;
using G_P2026.Infastructure;
using G_P2026.Infastructure.Context;
using G_P2026.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add Swagger documentation
builder.Services.AddEndpointsApiExplorer();


#region SQL Configuration
builder.Services.AddDbContext<APP_Identity>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

#endregion

#region Dependencies Injection
builder.Services.AddInfastructureDependencies(builder.Configuration).AddServiceDependencies().AddCore_DI()
	.AddServiceRegisteration(builder.Configuration);
#endregion

var app = builder.Build();

// Seed Roles
try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        Console.WriteLine("--> Seeding Roles...");
        await G_P2026.Infastructure.SeedData.RoleSeeder.SeedRolesAsync(services);
        Console.WriteLine("--> Seeding Completed Successfully.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"--> Error during seeding: {ex.Message}");
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Graduate_Project_2026 v1");
    c.RoutePrefix = string.Empty;  // Swagger UI accessible at root: https://mentorly.runasp.net/
});

app.UseHttpsRedirection();

// Enable static files
app.UseStaticFiles(); // For wwwroot

// Serve AuthTestUI files
app.UseDefaultFiles();
var authTestUiPath = Path.Combine(builder.Environment.ContentRootPath, "..", "AuthTestUI");
if (Directory.Exists(authTestUiPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(authTestUiPath),
        RequestPath = "" // Accessible directly at root
    });
}
app.UseRouting();
// Enable CORS
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
