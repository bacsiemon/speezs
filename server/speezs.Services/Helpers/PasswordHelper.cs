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
	}
}
