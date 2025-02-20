using Microsoft.Extensions.Logging;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.Services.Base;
using speezs.Services.Helpers;
using speezs.Services.Interfaces;
using speezs.Services.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class AuthService : IAuthService
	{
		private UnitOfWork _unitOfWork;
		private JwtHelper _jwtHelper;

		public AuthService(JwtHelper jwtHelper)
		{
			_jwtHelper = jwtHelper;
		}

		public AuthService()
		{
			if (null == _unitOfWork) _unitOfWork = new UnitOfWork();
		}

		public async Task<IServiceResult> Login(LoginRequestModel request)
		{
			try
			{
				var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
				if (user == null)
					return new ServiceResult(404, "Username not found");

				if (user.PasswordHash != HashPassword(request.Password, user.PasswordSalt))
					return new ServiceResult(401, "Incorrect Password");

				var accessToken = await _jwtHelper.GenerateAccessTokenAsync(user);
				var refreshToken = await _jwtHelper.GenerateRefreshTokenAsync();
				user.RefreshToken = refreshToken;
				user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);


				await _unitOfWork.UserRepository.UpdateAsync(user);
				return new ServiceResult(200, "Thành Công", new LoginResponseModel()
				{
					AccessToken = accessToken,
					RefreshToken = refreshToken,
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> Register(RegisterRequest request)
		{

			try
			{
				var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
				if (existingUser != null) return new ServiceResult(400, "Email is already used");

				string passwordSalt = GetPasswordSalt();
				var user = new User()
				{
					Email = request.Email,
					FullName = request.FullName,
					PasswordSalt = passwordSalt,
					PasswordHash = HashPassword(request.Password, passwordSalt),
				};

				await _unitOfWork.UserRepository.CreateAsync(user);
				return new ServiceResult(200, "Success");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString);
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> ForgotPassword(string email)
		{
			try
			{
				var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(email);
				if (existingUser == null)
					return new ServiceResult(200, "Success");

				var code = Convert.ToBase64String(RandomNumberGenerator.GetBytes(128 / 8)).Substring(0, 6);
				var entity = new UserResetPasswordCode()
				{
					Email = existingUser.Email,
					CodeHash = HashPassword(code, existingUser.PasswordSalt),
					Expire = DateTime.UtcNow.AddDays(1)
				};

				await _unitOfWork.UserResetPasswordCodeRepository.CreateAsync(entity);
				return new ServiceResult(200, "Success");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString);
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> ResetPassword(ResetPasswordRequest request)
		{
			try
			{
				var code = await _unitOfWork.UserResetPasswordCodeRepository.GetByEmailAsync(request.Email);
				if (code == null) return new ServiceResult(404, "Not Found");

				var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
				if (existingUser == null) return new ServiceResult(404, "Not Found");

				if (code.CodeHash != HashPassword(request.Code, existingUser.PasswordSalt))
					if (code == null) return new ServiceResult(404, "Not Found");

				existingUser.PasswordSalt = GetPasswordSalt();
				existingUser.PasswordHash = HashPassword(request.Password, existingUser.PasswordSalt);

				await _unitOfWork.UserRepository.UpdateAsync(existingUser);
				await _unitOfWork.UserResetPasswordCodeRepository.RemoveAsync(code);

				return new ServiceResult(200, "Success");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString);
				return new ServiceResult(500, ex.Message);
			}
		}


		private string HashPassword(string password, string salt)
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

		private string GetPasswordSalt()
		{
			return Convert.ToBase64String(RandomNumberGenerator.GetBytes(128 / 8)).Substring(0, 64);
		}


	}
}
