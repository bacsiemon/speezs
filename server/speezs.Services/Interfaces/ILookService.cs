using speezs.Services.Base;
using speezs.Services.Models.Look;

namespace speezs.Services.Interfaces
{
	public interface ILookService
	{
		Task<IServiceResult> CreateAsync(CreateLookRequest request);
		Task<IServiceResult> DeleteAsync(int id);
		Task<IServiceResult> GetAllAsync();
		Task<IServiceResult> GetByIdAsync(int id);
		Task<IServiceResult> GetPaginateAsync(int page, int size);
		Task<IServiceResult> UpdateAsync(UpdateLookRequest request);
	}
}