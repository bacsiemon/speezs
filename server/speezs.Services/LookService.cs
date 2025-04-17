using AutoMapper;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.Services.Base;
using speezs.Services.Interfaces;
using speezs.Services.Models.Look;
using speezs.Services.Models.MakeupProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class LookService : ILookService
	{
		private UnitOfWork _unitOfWork;
		private IMapper _mapper;

		public LookService(UnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IServiceResult> GetAllAsync()
		{
			try
			{
				return new ServiceResult(200, "Success", await _unitOfWork.LookRepository.GetAllAsync());
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
				var result = await _unitOfWork.LookRepository.GetByIdAsync(id);
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
				var result = await _unitOfWork.LookRepository.GetPagingListAsync(page: page, size: size);
				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> CreateAsync(CreateLookRequest request)
		{
			try
			{
				var entity = _mapper.Map<Look>(request);
				entity.DateModified = DateTime.Now;
				entity.DateCreated = DateTime.Now;
				_unitOfWork.LookRepository.Create(entity);
				entity.AvgRating = 0;
				entity.TotalTransfers = 0;
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

		public async Task<IServiceResult> UpdateAsync(UpdateLookRequest request)
		{
			try
			{
				var entity = await _unitOfWork.LookRepository.GetByIdAsync(request.LookId);
				if (entity == null)
					return new ServiceResult(404, "Not found");

				entity = _mapper.Map<UpdateLookRequest, Look>(request, entity);
				entity.DateModified = DateTime.Now;
				_unitOfWork.LookRepository.Update(entity);
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
				var entity = await _unitOfWork.LookRepository.GetByIdAsync(id);
				if (entity == null)
					return new ServiceResult(404, "Not found");

				_unitOfWork.LookRepository.Remove(entity);
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
