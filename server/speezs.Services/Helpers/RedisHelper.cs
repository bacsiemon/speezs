using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace speezs.Services.Helpers
{
	public static class RedisHelper
	{
		

		public static async Task SetRecordAsync<T>(this IDistributedCache cache, string id, T data, TimeSpan? absoluteExpireTime = null,
			TimeSpan? unusedExpireTime = null)
		{
			var options = new DistributedCacheEntryOptions();
			options.AbsoluteExpirationRelativeToNow = absoluteExpireTime;
			options.SlidingExpiration = unusedExpireTime;

			var jsonData = JsonSerializer.Serialize(data);
			await cache.SetStringAsync(id, jsonData, options);
		}

		public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache, string id)
		{
			try
			{
				var jsonData = await cache.GetStringAsync(id);
				if (jsonData == null)
					return default(T);

				return JsonSerializer.Deserialize<T>(jsonData);
			}
			catch (Exception ex)
			{
				return default(T);
			}
		}

	}
}
