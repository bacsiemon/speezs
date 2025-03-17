using AutoMapper;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.DataAccess.Paging;
using speezs.DataAccess.Repositories;
using speezs.Services.Base;
using speezs.Services.Interfaces;
using speezs.Services.Models.MakeupProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class MakeupProductService : IMakeupProductService
	{
		private UnitOfWork _unitOfWork;
		private IMapper _mapper;

		public MakeupProductService(UnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IServiceResult> GetAllAsync()
		{
			try
			{
				return new ServiceResult(200, "Success", await _unitOfWork.MakeupProductRepository.GetAllAsync());
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
				var result = await _unitOfWork.MakeupProductRepository.GetByIdAsync(id);
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
				var result = await _unitOfWork.MakeupProductRepository.GetPagingListAsync(page: page, size: size);
				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> CreateAsync(CreateMakeupProductRequest request)
		{
			try
			{
				var entity = _mapper.Map<MakeupProduct>(request);
				entity.DateModified = DateTime.Now;
				entity.DateCreated = DateTime.Now;
				_unitOfWork.MakeupProductRepository.Create(entity);
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

		public async Task<IServiceResult> UpdateAsync(UpdateMakeupProductRequest request)
		{
			try
			{
				var entity = await _unitOfWork.MakeupProductRepository.GetByIdAsync(request.ProductId);
				if (entity == null)
					return new ServiceResult(404, "Not found");

				entity = _mapper.Map<UpdateMakeupProductRequest, MakeupProduct>(request, entity);
				entity.DateModified = DateTime.Now;
				_unitOfWork.MakeupProductRepository.Update(entity);
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
				var entity = await _unitOfWork.MakeupProductRepository.GetByIdAsync(id);
				if (entity == null)
					return new ServiceResult(404, "Not found");

				_unitOfWork.MakeupProductRepository.Remove(entity);
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
