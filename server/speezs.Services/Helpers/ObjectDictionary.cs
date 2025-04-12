using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Helpers
{
	public class ObjectDictionary
	{
		private static Dictionary<string, object> Instance = new();

		public static Dictionary<string, object> GetInstance()
		{
			if (Instance == null)
			{
				Instance = new Dictionary<string, object>();
			}
			return Instance;
		}

		public static void SetValue(string key, object value)
		{
			if (Instance == null)
			{
				Instance = new Dictionary<string, object>();
			}
			if (Instance.ContainsKey(key))
			{
				Instance[key] = value;
			}
			else
			{
				Instance.Add(key, value);
			}
		}

		public static object GetValue(string key)
		{
			if (Instance == null)
			{
				Instance = new Dictionary<string, object>();
			}
			if (Instance.ContainsKey(key))
			{
				return Instance[key];
			}
			else
			{
				return null;
			}
		}
		public static void RemoveValue(string key)
		{
			if (Instance == null)
			{
				Instance = new Dictionary<string, object>();
			}
			if (Instance.ContainsKey(key))
			{
				Instance.Remove(key);
			}
		}
	}
}
