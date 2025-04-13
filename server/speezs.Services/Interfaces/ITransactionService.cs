using Net.payOS.Types;
using speezs.Services.Base;
using speezs.Services.Models.Transaction;

namespace speezs.Services.Interfaces
{
	public interface ITransactionService
	{
		Task<IServiceResult> CancelTransactionAsync(long orderCode);
		Task<IServiceResult> CreateAsync(CreateTransactionRequest request);
		Task<IServiceResult> GetByIdAsync(int id);
		Task<IServiceResult> GetByUserIdAsync(int userId, int page, int size);
		Task<IServiceResult> GetPaginateAsync(int page, int size);
		Task<IServiceResult> ProcessTransactionAsync(ProcessTransactionRequest request);
		Task<IServiceResult> ProcessWebHookAsync(WebhookType webhookBody);
	}
}