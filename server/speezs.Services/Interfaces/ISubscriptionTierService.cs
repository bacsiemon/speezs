using speezs.Services.Base;
using speezs.Services.Models.SubscriptionTier;

namespace speezs.Services.Interfaces
{
	public interface ISubscriptionTierService
	{
		Task<IServiceResult> CreateAsync(CreateSubscriptionTierRequest request);
		Task<IServiceResult> DeleteAsync(int id);
		Task<IServiceResult> GetAllAsync();
		Task<IServiceResult> GetByIdAsync(int id);
		Task<IServiceResult> GetPaginateAsync(int page, int size);
		Task<IServiceResult> UpdateAsync(UpdateSubscriptionTierRequest request);
	}
}