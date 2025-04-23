using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.Services.Base;
using speezs.Services.Helpers;
using speezs.Services.Interfaces;
using speezs.Services.Models.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class AuthService : IAuthService
	{
		private UnitOfWork _unitOfWork;
		private PasswordHelper _passwordHelper;
		private IUserRoleService _userRoleService;
		private IConfiguration _configuration;
		private GmailHelper _gmailHelper;

		public AuthService(UnitOfWork unitOfWork, PasswordHelper passwordHelper, IUserRoleService userRoleService, IConfiguration configuration, GmailHelper gmailHelper)
		{
			_unitOfWork = unitOfWork;
			_passwordHelper = passwordHelper;
			_userRoleService = userRoleService;
			_configuration = configuration;
			_gmailHelper = gmailHelper;
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

				var accessToken = await GenerateAccessTokenAsync(user);
				var refreshToken = await GenerateRefreshTokenAsync();
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
				return new ServiceResult(500, ex.ToString());
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
				_unitOfWork.UserRoleRepository.Create(new Userrole()
				{
					UserId = user.UserId,
					RoleId = 2, // User Role
				});
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(200, "Success");
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.ToString());
			}
		}

		public async Task<IServiceResult> ForgotPassword(string email)
		{
			try
			{
				var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(email);
				if (existingUser == null)
					return new ServiceResult(HttpStatus.NOT_FOUND, "Not Found");

				var code =  await _passwordHelper.CreateResetPasswordCode(existingUser.UserId);
				_gmailHelper.SendEmailSingle(new EmailRequestModel()
				{
					ReceiverEmail = existingUser.Email,
					EmailSubject = "Mã Đặt Lại Mật Khẩu",
					EmailBody = EmailTemplates.ResetPasswordEmail(code),
					IsHtml = true
				});
				return new ServiceResult(HttpStatus.OK, "Success");
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.ToString());
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
				return new ServiceResult(500, ex.ToString());
			}
		}

		public async Task<IServiceResult> RefreshToken(string refreshToken)
		{
			try
			{
				var user = await _unitOfWork.UserRepository.GetByRefreshTokenAsync(refreshToken);
				if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow.AddHours(7))
					return new ServiceResult(HttpStatus.UNAUTHORIZED);

				var accessToken = await GenerateAccessTokenAsync(user);
				refreshToken = await GenerateRefreshTokenAsync();
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
				return new ServiceResult(500, ex.ToString());
			}
		}

		public async Task<IServiceResult> Logout(string accessToken)
		{
			try
			{
				var handler = new JwtSecurityTokenHandler();
				var token = handler.ReadJwtToken(accessToken);
				var userId = int.Parse(token.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
				var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
				if (user == null) return new ServiceResult(HttpStatus.NOT_FOUND, "Không tìm thấy");
				user.RefreshToken = null;
				user.RefreshTokenExpiry = null;
				user.DateModified = DateTime.Now;
				_unitOfWork.UserRepository.Update(user);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(HttpStatus.NO_CONTENT);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.ToString());
			}
		}

		public async Task<string> GenerateAccessTokenAsync(User user)
		{
			var userRole = await _unitOfWork.UserRoleRepository.GetByIdAsync(user.UserId);
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
			var tokenDescription = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new System.Security.Claims.Claim("UserId", user.UserId.ToString()),
					new System.Security.Claims.Claim(ClaimTypes.Name, user.FullName),
					new System.Security.Claims.Claim(ClaimTypes.Email, user.Email),
					new System.Security.Claims.Claim("PhoneNumber", user.PhoneNumber),
					new System.Security.Claims.Claim(ClaimTypes.Role, userRole.RoleId.ToString()),
				}),
				Expires = DateTime.Now.AddMinutes(120),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = jwtTokenHandler.WriteToken(jwtTokenHandler.CreateToken(tokenDescription));
			return token;
		}

		public async Task<string> GenerateRefreshTokenAsync()
		{
			var random = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(random); //dien so vao mang
				return Convert.ToBase64String(random); //chuyen mang byte thanh base64
			}
		}
	}
}
