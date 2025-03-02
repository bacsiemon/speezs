using speezs.Services.Base;
using speezs.Services.Models.Transfer;

namespace speezs.Services.Interfaces
{
	public interface ITransferService
	{
		Task<IServiceResult> CreateAsync(CreateTransferRequest request);
		Task<IServiceResult> DeleteAsync(int id);
		Task<IServiceResult> GetAllAsync();
		Task<IServiceResult> GetByIdAsync(int id);
		Task<IServiceResult> GetPaginateAsync(int page, int size);
	}
}