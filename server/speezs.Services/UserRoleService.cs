using Microsoft.AspNetCore.Mvc.RazorPages;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.DataAccess.Paging;
using speezs.DataAccess.Repositories;
using speezs.Services.Base;
using speezs.Services.Interfaces;
using speezs.Services.Models.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class UserRoleService : IUserRoleService
	{
		private UnitOfWork _unitOfWork;

		public UserRoleService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		public async Task<IServiceResult> GetAllAsync()
		{
			try
			{
				return new ServiceResult(200, "Success", await _unitOfWork.UserRoleRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> GetByUserIdAsync(int userId)
		{
			try
			{
				var result = await _unitOfWork.UserRoleRepository.GetByIdAsync(userId);
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
				IPaginate<DataAccess.Models.Userrole> result = await _unitOfWork.UserRoleRepository.GetPagingListAsync(page: page, size: size);
				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> CreateAsync(CreateUserRoleRequest request)
		{
			try
			{
				var userRole = _unitOfWork.UserRoleRepository.GetByIdAsync(request.UserId);
				var user = _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
				var role = _unitOfWork.RoleRepository.GetByIdAsync(request.RoleId);

				await Task.WhenAll(userRole, user, role);

				if (user.Result == null)
					return new ServiceResult(404, "User not found");

				if (userRole.Result != null)
					return new ServiceResult(400, "User already has a role");

				if (role.Result == null)
					return new ServiceResult(404, "Role not found");

				var entity = new Userrole()
				{
					UserId = request.UserId,
					RoleId = request.RoleId,
				};
				_unitOfWork.UserRoleRepository.Create(entity);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(200, "Success", entity);

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
