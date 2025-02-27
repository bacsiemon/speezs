using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.DataAccess.Paging;
using speezs.Services.Base;
using speezs.Services.Helpers;
using speezs.Services.Interfaces;
using speezs.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class UserService : IUserService
	{
		private UnitOfWork _unitOfWork;
		private IMapper _mapper;
		private PasswordHelper _passwordHelper;

		public UserService(UnitOfWork unitOfWork, IMapper mapper, PasswordHelper passwordHelper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_passwordHelper = passwordHelper;
		}


		public async Task<IServiceResult> GetAllAsync()
		{
			try
			{
				return new ServiceResult(200, "Success", await _unitOfWork.UserRepository.GetAllAsync());
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
				var result = await _unitOfWork.UserRepository.GetByIdAsync(id);
				if (result == null)
					return new ServiceResult(404, "Not Found");
				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> CreateAsync(CreateUserRequest request)
		{
			try
			{
				var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
				if (existingUser != null)
					return new ServiceResult(400, "Email is already used");

				existingUser = await _unitOfWork.UserRepository.GetByPhoneNumberAsync(request.PhoneNumber);
				if (existingUser != null)
					return new ServiceResult(400, "PhoneNumber is already used");

				var user = _mapper.Map<User>(request);
				user.PasswordSalt = _passwordHelper.GetSalt();
				user.PasswordHash = _passwordHelper.HashPassword(request.Password, user.PasswordSalt);
				user.DateCreated = DateTime.Now;
				user.DateModified = DateTime.Now;

				_unitOfWork.UserRepository.Create(user);
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

		public async Task<IServiceResult> UpdateAsync(UpdateUserRequest request)
		{
			try
			{
				if (request.Email != null)
				{
					var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
					if (existingUser != null)
						return new ServiceResult(400, "Email is already used");
				}
				if (request.PhoneNumber != null)
				{
					var existingUser = await _unitOfWork.UserRepository.GetByPhoneNumberAsync(request.PhoneNumber);
					if (existingUser != null)
						return new ServiceResult(400, "PhoneNumber is already used");
				}
				var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
				if (user == null) 
					return new ServiceResult(404, "User not found");

				user = _mapper.Map<UpdateUserRequest, User>(request, user);
				if (request.Password != null) 
				{
					user.PasswordSalt = _passwordHelper.GetSalt();
					user.PasswordHash = _passwordHelper.HashPassword(request.Password, user.PasswordSalt);
				}
				user.DateCreated = DateTime.Now;
				user.DateModified = DateTime.Now;

				_unitOfWork.UserRepository.Update(user);
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
				var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
				if (user == null)
					return new ServiceResult(404, "User not found");
				
				_unitOfWork.UserRepository.Remove(user);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(204, "Success");
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> GetPaginateAsync(int page, int size)
		{
			try
			{
				IPaginate<User> result = await _unitOfWork.UserRepository.GetPagingListAsync(page : page,size:size);
				return new ServiceResult(200, "Success", result);
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
