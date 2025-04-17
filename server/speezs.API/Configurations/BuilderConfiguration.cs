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
using System.Text.Json.Serialization;

namespace speezs.API.Configurations
{
	public static class BuilderConfiguration
	{
		
		public static void Configure(WebApplicationBuilder builder)
		{
			ConfigureRepositories(builder);
			ConfigureServices(builder);
			ConfigureControllers(builder);
			ConfigureSerialization(builder);
			ConfigureJWT(builder);
			ConfigureRedis(builder);
		}

		public static void ConfigureControllers(WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IMakeupProductService, MakeupProductService>();
			builder.Services.AddScoped<IImageService, ImageService>();
			builder.Services.AddScoped<ILookService, LookService>();
			builder.Services.AddScoped<ITransferService, TransferService>();
			builder.Services.AddScoped<ITransactionService, TransactionService>();
			builder.Services.AddScoped<ISubscriptionTierService, SubscriptionTierService>();
			builder.Services.AddScoped<IReviewService, ReviewService>();
			builder.Services.AddScoped<IRoleService, RoleService>();
			builder.Services.AddScoped<IUserRoleService, UserRoleService>();
			builder.Services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
		}

		public static void ConfigureServices(WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<UnitOfWork>();
			builder.Services.AddScoped<PasswordHelper>();
			builder.Services.AddScoped<GmailHelper>();

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
			builder.Services.AddDbContext<SpeezsDbContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STR_POSTGRES")));
		}

		public static void ConfigureSerialization(WebApplicationBuilder builder)
		{
			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
				options.JsonSerializerOptions.AllowTrailingCommas = false;
			});
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

				options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "basic",
					In = ParameterLocation.Header,
					Description = "Basic Authorization header using the Basic scheme."
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

		public static void ConfigureRedis(WebApplicationBuilder builder)
		{
			builder.Services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = builder.Configuration["ConnectionStrings:RedisHttps"];
				options.InstanceName = "Speezs_";
			});
		}
	}
}
