using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.Services;
using speezs.Services.Helpers;
using speezs.Services.Interfaces;
using System.Text;

namespace speezs.API.Configurations
{
	public static class DependencyInjectionConfiguration
	{
		
		public static void Configure(WebApplicationBuilder builder)
		{
			ConfigureRepositories(builder);
			ConfigureServices(builder);
			ConfigureControllers(builder);
		}

		public static void ConfigureControllers(WebApplicationBuilder builder)
		{
			
			builder.Services.AddScoped<IAuthService, AuthService>();
		}

		public static void ConfigureServices(WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<UnitOfWork>();
			builder.Services.AddScoped<JwtHelper>();
		}

		public static void ConfigureRepositories(WebApplicationBuilder builder) 
		{
			var config = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddJsonFile("appsettings.json")
			.Build();
			builder.Services.AddSingleton<IConfiguration>(config);
			builder.Services.AddDbContext<SpeezsDbContext>(options => options.UseNpgsql(builder.Configuration["ConnectionStrings:DBConnection"]));
		}

		
	}
}
