using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using speezs.DataAccess.Models;
using System.Text;

namespace speezs.API.Configurations
{
	public static class DependencyInjection
	{
		
		public static void Configure(WebApplicationBuilder builder)
		{
			ConfigureJWT(builder);
			ConfigureServices(builder);
			ConfigureRepositories(builder);
		}

		public static void ConfigureServices(WebApplicationBuilder builder)
		{

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

		public static void ConfigureJWT(WebApplicationBuilder builder)
		{
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
.AddJwtBearer(options =>
{
options.TokenValidationParameters = new TokenValidationParameters
{
	ValidateIssuer = true,
	ValidateAudience = true,
	ValidateLifetime = true,
	ValidateIssuerSigningKey = true,
	ValidIssuer = builder.Configuration["Jwt:Issuer"],
	ValidAudience = builder.Configuration["Jwt:Audience"],
	IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
	ClockSkew = TimeSpan.Zero
};
});

			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
			});
		}
	}
}
