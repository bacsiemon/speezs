using speezs.Services.Helpers;
using StackExchange.Redis;

namespace speezs.API.Configurations
{
	public class RedisConfiguration
	{
		public static void Configure(WebApplicationBuilder builder)
		{
			ConfigureRedis(builder);
		}

		public static void ConfigureRedis(WebApplicationBuilder builder)
		{
			builder.Services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = builder.Configuration["ConnectionStrings:Redis"];
				options.InstanceName = "Speezs_";
			});
		}

	}
}
