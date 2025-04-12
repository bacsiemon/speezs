using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Helpers
{
	public class PasswordHelper
	{
		public PasswordHelper()
		{
			
		}

		public string HashPassword(string password, string salt)
		{
			var saltBytes = Encoding.UTF8.GetBytes(salt);
			var hash = Rfc2898DeriveBytes.Pbkdf2(
		Encoding.UTF8.GetBytes(password),
		saltBytes,
		350000,
		HashAlgorithmName.SHA512,
		64);
			return Convert.ToHexString(hash);
		}

		public string GetSalt()
		{
			return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
		}

		public async Task<string> CreateResetPasswordCode(int userId)
		{
			var code = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16)).Substring(0,5);
			OtpDictionary.SetValue($"RESETPASS_{userId}", code);
			Console.WriteLine(code);
			return code;
		}

		public async Task<bool> CheckResetPasswordCode(int userId, string code)
		{
			var result = OtpDictionary.GetValue($"RESETPASS_{userId}");
			if (result == null || !result.Equals(code)) return false;
			OtpDictionary.RemoveValue($"RESETPASS_{userId}");
			return true;
		}
	}
}
