using speezs.Services.Base;

namespace speezs.Services.Interfaces
{
	public interface IUserSubscriptionService
	{
		Task<IServiceResult> GetByUserIdAsync(int userId);
	}
}