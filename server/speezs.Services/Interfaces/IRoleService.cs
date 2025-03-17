using speezs.Services.Base;
using speezs.Services.Models.Role;

namespace speezs.Services.Interfaces
{
	public interface IRoleService
	{
		Task<IServiceResult> CreateAsync(CreateRoleRequest request);
		Task<IServiceResult> DeleteAsync(int id);
		Task<IServiceResult> GetAllAsync();
		Task<IServiceResult> GetByIdAsync(int id);
		Task<IServiceResult> GetPaginateAsync(int page, int size);
		Task<IServiceResult> UpdateAsync(UpdateRoleRequest request);
	}
}