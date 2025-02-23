using speezs.Services.Base;
using speezs.Services.Models.User;

namespace speezs.Services.Interfaces
{
	public interface IUserService
	{
		Task<IServiceResult> GetAllAsync();
		Task<IServiceResult> GetByIdAsync(int id);
		Task<IServiceResult> CreateAsync(CreateUserRequest request);
		Task<IServiceResult> UpdateAsync(UpdateUserRequest request);
		Task<IServiceResult> DeleteAsync(int id);
	}
}