using AutoMapper;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.Services.Base;
using speezs.Services.Interfaces;
using speezs.Services.Models.MakeupProduct;
using speezs.Services.Models.SubscriptionTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class SubscriptionTierService : ISubscriptionTierService
	{
		private UnitOfWork _unitOfWork;
		private IMapper _mapper;

		public SubscriptionTierService(IMapper mapper, UnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<IServiceResult> GetAllAsync()
		{
			try
			{
				return new ServiceResult(200, "Success", await _unitOfWork.SubscriptionTierRepository.GetAllAsync());
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
				var result = await _unitOfWork.SubscriptionTierRepository.GetByIdAsync(id);
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
				var result = await _unitOfWork.SubscriptionTierRepository.GetPagingListAsync(page: page, size: size);
				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> CreateAsync(CreateSubscriptionTierRequest request)
		{
			try
			{
				var entity = _mapper.Map<Subscriptiontier>(request);
				entity.DateModified = DateTime.Now;
				entity.DateCreated = DateTime.Now;
				_unitOfWork.SubscriptionTierRepository.Create(entity);
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

		public async Task<IServiceResult> UpdateAsync(UpdateSubscriptionTierRequest request)
		{
			try
			{
				var entity = await _unitOfWork.SubscriptionTierRepository.GetByIdAsync(request.TierId);
				if (entity == null)
					return new ServiceResult(404, "Not found");

				entity = _mapper.Map<UpdateSubscriptionTierRequest, Subscriptiontier>(request, entity);
				entity.DateModified = DateTime.Now;
				_unitOfWork.SubscriptionTierRepository.Update(entity);
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

		public async Task<IServiceResult> DeleteAsync(int id)
		{
			try
			{
				var entity = await _unitOfWork.SubscriptionTierRepository.GetByIdAsync(id);
				if (entity == null)
					return new ServiceResult(404, "Not found");

				_unitOfWork.SubscriptionTierRepository.Remove(entity);
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
	}
}
