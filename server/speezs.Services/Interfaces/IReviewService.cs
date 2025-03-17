using speezs.Services.Base;
using speezs.Services.Models.Review;

namespace speezs.Services.Interfaces
{
	public interface IReviewService
	{
		Task<IServiceResult> CreateAsync(CreateReviewRequest request);
		Task<IServiceResult> DeleteAsync(int id);
		Task<IServiceResult> GetAllAsync();
		Task<IServiceResult> GetByIdAsync(int id);
		Task<IServiceResult> GetPaginateAsync(int page, int size);
		Task<IServiceResult> UpdateAsync(UpdateReviewRequest request);
	}
}