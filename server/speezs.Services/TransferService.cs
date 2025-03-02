using AutoMapper;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.DataAccess.Paging;
using speezs.Services.Base;
using speezs.Services.Interfaces;
using speezs.Services.Models.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class TransferService : ITransferService
	{
		private UnitOfWork _unitOfWork;
		private IMapper _mapper;

		public TransferService(UnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IServiceResult> GetAllAsync()
		{
			try
			{
				return new ServiceResult(200, "Success", await _unitOfWork.TransferRepository.GetAllAsync());
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
				var result = await _unitOfWork.TransferRepository.GetByIdAsync(id);
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
				IPaginate<Transfer> result = await _unitOfWork.TransferRepository.GetPagingListAsync(page: page, size: size);
				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> CreateAsync(CreateTransferRequest request)
		{
			try
			{
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
				var transfer = await _unitOfWork.TransferRepository.GetByIdAsync(id);
				if (transfer == null)
					return new ServiceResult(404, "Transfer not found");

				_unitOfWork.TransferRepository.Remove(transfer);
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
	}
}
