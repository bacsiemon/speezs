using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.Services;
using speezs.Services.Configurations;
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
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IMakeupProductService, MakeupProductService>();
			builder.Services.AddScoped<ILookService, LookService>();
		}

		public static void ConfigureServices(WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<UnitOfWork>();
			builder.Services.AddScoped<JwtHelper>();
			builder.Services.AddScoped<PasswordHelper>();

			var configuration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MapperProfile>(); // Add your mapping profile(s)
			});
			IMapper mapper = configuration.CreateMapper();

			//Registering for Dependency Injection
			builder.Services.AddSingleton(mapper);

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
