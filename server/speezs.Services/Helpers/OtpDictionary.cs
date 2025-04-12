using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Helpers
{
	public class OtpDictionary
	{
		private static Dictionary<string, string> Instance = new();

		public static Dictionary<string, string> GetInstance()
		{
			if (Instance == null)
			{
				Instance = new Dictionary<string, string>();
			}
			return Instance;
		}

		public static void SetValue(string key, string value)
		{
			if (Instance == null)
			{
				Instance = new Dictionary<string, string>();
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

		public static string GetValue(string key)
		{
			if (Instance == null)
			{
				Instance = new Dictionary<string, string>();
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
				Instance = new Dictionary<string, string>();
			}
			if (Instance.ContainsKey(key))
			{
				Instance.Remove(key);
			}
		}
	}
}
