﻿using AutoMapper;
using speezs.DataAccess.Models;
using speezs.DataAccess;
using speezs.Services.Base;
using speezs.Services.Interfaces;
using speezs.Services.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class ReviewService : IReviewService
	{
		private UnitOfWork _unitOfWork;
		private IMapper _mapper;

		public ReviewService(IMapper mapper, UnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<IServiceResult> GetAllAsync()
		{
			try
			{
				return new ServiceResult(200, "Success", await _unitOfWork.ReviewRepository.GetAllAsync());
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
				var result = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
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
				var result = await _unitOfWork.ReviewRepository.GetPagingListAsync(page: page, size: size);
				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}

		public async Task<IServiceResult> CreateAsync(CreateReviewRequest request)
		{
			try
			{
				var entity = _mapper.Map<Review>(request);
				entity.DateModified = DateTime.Now;
				entity.DateCreated = DateTime.Now;
				_unitOfWork.ReviewRepository.Create(entity);
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

		public async Task<IServiceResult> UpdateAsync(UpdateReviewRequest request)
		{
			try
			{
				var entity = await _unitOfWork.ReviewRepository.GetByIdAsync(request.ReviewId);
				if (entity == null)
					return new ServiceResult(404, "Not found");

				entity = _mapper.Map<UpdateReviewRequest, Review>(request, entity);
				entity.DateModified = DateTime.Now;
				_unitOfWork.ReviewRepository.Update(entity);
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
				var entity = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
				if (entity == null)
					return new ServiceResult(404, "Not found");

				_unitOfWork.ReviewRepository.Remove(entity);
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
