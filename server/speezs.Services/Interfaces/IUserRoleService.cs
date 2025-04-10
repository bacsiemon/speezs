using speezs.Services.Base;
using speezs.Services.Models.UserRole;

namespace speezs.Services.Interfaces
{
	public interface IUserRoleService
	{
		Task<IServiceResult> CreateAsync(CreateUserRoleRequest request);
		Task<IServiceResult> GetAllAsync();
		Task<IServiceResult> GetByUserIdAsync(int userId);
		Task<IServiceResult> GetPaginateAsync(int page, int size);
	}
}