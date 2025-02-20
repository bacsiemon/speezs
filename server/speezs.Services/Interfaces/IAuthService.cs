using speezs.Services.Base;
using speezs.Services.Models.Auth;

namespace speezs.Services.Interfaces
{
	public interface IAuthService
	{
		Task<IServiceResult> ForgotPassword(string email);
		Task<IServiceResult> Login(LoginRequestModel request);
		Task<IServiceResult> Register(RegisterRequest request);
		Task<IServiceResult> ResetPassword(ResetPasswordRequest request);
	}
}