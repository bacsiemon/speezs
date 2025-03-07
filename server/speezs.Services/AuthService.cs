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
		private PasswordHelper _passwordHelper;
		public AuthService(JwtHelper jwtHelper, UnitOfWork unitOfWork, PasswordHelper passwordHelper)
		{
			_jwtHelper = jwtHelper;
			_unitOfWork = unitOfWork;
			_passwordHelper = passwordHelper;
		}

		public async Task<IServiceResult> Login(LoginRequestModel request)
		{
			try
			{
				var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
				if (user == null)
					return new ServiceResult(404, "Username not found");

				if (user.PasswordHash != _passwordHelper.HashPassword(request.Password, user.PasswordSalt))
					return new ServiceResult(401, "Incorrect Password");

				var accessToken = await _jwtHelper.GenerateAccessTokenAsync(user);
				var refreshToken = await _jwtHelper.GenerateRefreshTokenAsync();
				user.RefreshToken = refreshToken;
				user.RefreshTokenExpiry = DateTime.Now.AddDays(30);
				user.DateModified = DateTime.Now;

				_unitOfWork.UserRepository.Update(user);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(200, "Thành Công", new LoginResponseModel()
				{
					AccessToken = accessToken,
					RefreshToken = refreshToken,
				});
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> Register(RegisterRequest request)
		{
			try
			{
				var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
				if (existingUser != null) return new ServiceResult(400, "Email is already used");

				string passwordSalt = _passwordHelper.GetSalt();
				var user = new User()
				{
					Email = request.Email,
					PhoneNumber = request.PhoneNumber,
					FullName = request.FullName,
					PasswordSalt = passwordSalt,
					PasswordHash = _passwordHelper.HashPassword(request.Password, passwordSalt),
				};
				user.DateCreated = DateTime.Now;
				user.DateModified = DateTime.Now;

				_unitOfWork.UserRepository.Create(user);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(200, "Success");
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> ForgotPassword(string email)
		{
			try
			{
				var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(email);
				if (existingUser == null)
					return new ServiceResult(404, "Not Found");

				await _passwordHelper.CreateResetPasswordCode(existingUser.UserId);
				return new ServiceResult(200, "Success");
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> ResetPassword(ResetPasswordRequest request)
		{
			try
			{

				var user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
				if (user == null) return new ServiceResult(404, "Not Found");

				if (!await _passwordHelper.CheckResetPasswordCode(user.UserId, request.Code))
					return new ServiceResult(404, "Incorrect code");

				user.PasswordSalt = _passwordHelper.GetSalt();
				user.PasswordHash = _passwordHelper.HashPassword(request.Password, user.PasswordSalt);
				user.DateModified = DateTime.Now;

				_unitOfWork.UserRepository.Update(user);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(200, "Success");
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}


	}
}
