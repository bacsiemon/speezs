using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using speezs.DataAccess;
using speezs.DataAccess.Paging;
using speezs.Services.Base;
using speezs.Services.Helpers;
using speezs.Services.Models.Transaction;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace speezs.Services
{
	public class TransactionService
	{
		private readonly string STATUS_PENDING = "Pending";
		private readonly string STATUS_PROCESSING = "Processing";
		private readonly string STATUS_COMPLETED = "Completed";
		private readonly string STATUS_FAILED = "Failed";
		private readonly string STATUS_REFUNDED = "Refunded";
		private readonly string STATUS_CANCELED = "Canceled";

		private UnitOfWork _unitOfWork;
		private IMapper _mapper;
		private IConfiguration _configuration;

		public TransactionService(UnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
		{
			this._unitOfWork = unitOfWork;
			_mapper = mapper;
			this._configuration = configuration;
		}

		public async Task<IServiceResult> GetPaginateAsync(int page, int size)
		{
			try
			{
				IPaginate<DataAccess.Models.Transaction> result = await _unitOfWork.TransactionRepository.GetPagingListAsync(page: page, size: size);
				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> GetByIdAsync(int id)
		{
			try
			{
				var result = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
				if (result == null)
					return new ServiceResult(HttpStatus.NOT_FOUND, "Not Found");
				return new ServiceResult(HttpStatus.OK, "Success", result);
			}
			catch (Exception ex)
			{
				return new ServiceResult(HttpStatus.INTERNAL_SERVER_ERROR, ex.Message);
			}
		}

		public async Task<IServiceResult> GetByUserIdAsync(int userId, int page, int size)
		{
			try
			{
				IPaginate<DataAccess.Models.Transaction> result = await _unitOfWork.TransactionRepository.GetPagingListAsync(
					predicate: x => x.UserId == userId, 
					page: page, 
					size: size);
				return new ServiceResult(HttpStatus.OK, "Success", result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(HttpStatus.INTERNAL_SERVER_ERROR, ex.Message);

			}
		}

		public async Task<IServiceResult> CreateAsync(CreateTransactionRequest transaction)
		{
			try
			{
				var entity = _mapper.Map<DataAccess.Models.Transaction>(transaction);
				await _unitOfWork.TransactionRepository.CreateAsync(entity);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(HttpStatus.OK, "Success", transaction);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(HttpStatus.INTERNAL_SERVER_ERROR, ex.Message);
			}
		}


	}
	
}