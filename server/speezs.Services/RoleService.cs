using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.DataAccess.Paging;
using speezs.Services.Base;
using speezs.Services.Interfaces;
using speezs.Services.Models.Role;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class RoleService : IRoleService
	{
		private UnitOfWork _unitOfWork;

		public RoleService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IServiceResult> GetAllAsync()
		{
			try
			{
				return new ServiceResult(200, "Success", await _unitOfWork.RoleRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> GetByIdAsync(int id)
		{
			try
			{
				var result = await _unitOfWork.RoleRepository.GetByIdAsync(id);
				if (result == null)
					return new ServiceResult(404, "Not Found");
				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> GetPaginateAsync(int page, int size)
		{
			try
			{
				IPaginate<DataAccess.Models.Role> result = await _unitOfWork.RoleRepository.GetPagingListAsync(page: page, size: size);
				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}


		public async Task<IServiceResult> CreateAsync(CreateRoleRequest request)
		{
			try
			{
				var existingRole = await _unitOfWork.RoleRepository.GetByNameAsync(request.RoleName);
				if (existingRole != null)
					return new ServiceResult(400, "Role already exists.");

				var role = new DataAccess.Models.Role()
				{
					RoleName = request.RoleName.ToUpper()
				};

				_unitOfWork.RoleRepository.Create(role);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(200, "Success", role);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> UpdateAsync(UpdateRoleRequest request)
		{
			try
			{
				var existingRole = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id);
				if (existingRole == null)
					return new ServiceResult(404, "Role Not Found");

				if (!string.IsNullOrEmpty(request.RoleName)) existingRole.RoleName = request.RoleName.ToUpper();
				_unitOfWork.RoleRepository.Update(existingRole);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(200, "Success");

			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> DeleteAsync(int id)
		{
			try
			{
				var existingRole = await _unitOfWork.RoleRepository.GetByIdAsync(id);
				if (existingRole == null)
					return new ServiceResult(404, "Role Not Found");

				var count = await _unitOfWork.UserRoleRepository.GetRoleCountAsync(id);
				if (count > 0)
					return new ServiceResult(400, "Some users are having this role. Cannot delete.");

				_unitOfWork.RoleRepository.Remove(existingRole);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(204);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

	}
}