using speezs.Services.Base;
using speezs.Services.Models.MakeupProduct;

namespace speezs.Services.Interfaces
{
	public interface IMakeupProductService
	{
		Task<IServiceResult> CreateAsync(CreateMakeupProductRequest request);
		Task<IServiceResult> DeleteAsync(int id);
		Task<IServiceResult> GetAllAsync();
		Task<IServiceResult> GetByIdAsync(int id);
		Task<IServiceResult> GetByLookIdAsync(int lookId);
		Task<IServiceResult> GetPaginateAsync(int page, int size);
		Task<IServiceResult> UpdateAsync(UpdateMakeupProductRequest request);
	}
}