using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.Services.Base;
using speezs.Services.Helpers;
using speezs.Services.Interfaces;
using speezs.Services.Models.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class ImageService : IImageService
	{
		private UnitOfWork _unitOfWork;

		public ImageService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IServiceResult> GetByIdAsync(int id)
		{
			try
			{
				var image = await _unitOfWork.ImageRepository.GetByIdAsync(id);
				if (image == null)
				{
					return new ServiceResult(HttpStatus.NOT_FOUND, "Image not found");
				}
				return new ServiceResult(200, "Success", image);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return new ServiceResult(HttpStatus.INTERNAL_SERVER_ERROR, ex.Message);
			}
		}

		public async Task<IServiceResult> CreateAsync(IFormFile request)
		{
			try
			{
				if (request.Length == 0)
				{
					return new ServiceResult(400, "Image cannot be empty");
				}

				if (request.Length > 10 * 1024 * 1024)
				{
					return new ServiceResult(400, "Image size cannot exceed 10MB");
				}
				var image = new Image();
				image.ContentType = request.ContentType;
				image.UploadedAt = DateTime.Now;
				using (var ms = new MemoryStream())
				{
					await request.CopyToAsync(ms);
					image.Data = ms.ToArray();
				}
				_unitOfWork.ImageRepository.Create(image);
				await _unitOfWork.SaveChangesAsync();

				return new ServiceResult(200, "Success", $"https://api.speezsai.com/api/image/{image.Id}");
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}


		public async Task<IServiceResult> TransferTestAsync(IFormFile request)
		{
			try
			{
				if (request.Length == 0)
				{
					return new ServiceResult(400, "Image cannot be empty");
				}

				if (request.Length > 10 * 1024 * 1024)
				{
					return new ServiceResult(400, "Image size cannot exceed 10MB");
				}
				var image = new Image();
				image.ContentType = request.ContentType;
				image.UploadedAt = DateTime.Now;
				using (var ms = new MemoryStream())
				{
					await request.CopyToAsync(ms);
					image.Data = ms.ToArray();
				}
				_unitOfWork.ImageRepository.Create(image);
				await _unitOfWork.SaveChangesAsync();

				return new ServiceResult(200, "Success", $"https://api.speezsai.com/api/image/{image.Id}");
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
