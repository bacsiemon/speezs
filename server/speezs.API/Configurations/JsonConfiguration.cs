using System.Text.Json.Serialization;

namespace speezs.API.Configurations
{
	public static class JsonConfiguration
	{
		public static void Configure(WebApplicationBuilder builder)
		{
			ConfigureSerialization(builder);
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
	}
}
