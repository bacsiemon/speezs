using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
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
using System.Text.Json;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class TransferService : ITransferService
	{
		private UnitOfWork _unitOfWork;
		private IMapper _mapper;
		private IImageService _imageService;
		private IUserSubscriptionService _userSubscriptionService;

		public TransferService(UnitOfWork unitOfWork, IMapper mapper, IImageService imageService, IUserSubscriptionService userSubscriptionService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_imageService = imageService;
			_userSubscriptionService = userSubscriptionService;
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
				var look = await _unitOfWork.LookRepository.GetByIdAsync(request.LookId);
				if (look == null)
					return new ServiceResult(404, "Look not found");
				var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
				if (user == null)
					return new ServiceResult(404, "User not found");

				var userSubscription = _userSubscriptionService.GetByUserIdAsync(request.UserId).Result.Data as DataAccess.Models.Usersubscription;
				if (userSubscription == null)
					return new ServiceResult(404, "Subscription not found");
				if (userSubscription.TransfersLeft.GetValueOrDefault() == 0)
					return new ServiceResult(500, "No Transfers left");

				//var userSubscription = await _unitOfWork.UserSubscriptionRepository.GetByUserIdAsync(request.UserId);
				var watch = System.Diagnostics.Stopwatch.StartNew();

				var processingResult = await _imageService.CreateAsync(request.Image);
				if (!processingResult.IsSuccess())
					return processingResult;

				var sourceUrl = processingResult.Data as string;
				var resultUrl = await ProcessImageAsync(request.Image, "vFG137.png");
				if (resultUrl.IsNullOrEmpty())
					return new ServiceResult(500, "Image processing failed");

				watch.Stop();
				var entity = new Transfer();
				entity.UserId = request.UserId;
				entity.LookId = request.LookId;
				entity.SourceImageUrl = sourceUrl;
				entity.ResultImageUrl = resultUrl; //resultUrl;
				entity.Status = "Success";
				entity.ProcessingTime = Convert.ToDecimal(watch.ElapsedMilliseconds / 1000);
				entity.AiModelVersion = "v1.0";
				_unitOfWork.TransferRepository.Create(entity);

				userSubscription.TransfersLeft--;
				_unitOfWork.UserSubscriptionRepository.Update(userSubscription);
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

		public async Task<string> ProcessImageAsync(IFormFile request, string color)
		{
			try
			{
				HttpClient httpClient = new HttpClient();
				httpClient.BaseAddress = new Uri("https://speezsaimodel.azurewebsites.net");
				using var multipartContent = new MultipartFormDataContent();
				using var streamContent = new StreamContent(request.OpenReadStream());
				streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

				// Add the file to the form data
				multipartContent.Add(
					streamContent,
					"no_makeup",  // form field name
					request.FileName
				);

				multipartContent.Add(new StringContent(color), "makeup_template");


				var response = await httpClient.PostAsync($"/apply-makeup/", multipartContent);
				if (!response.IsSuccessStatusCode)
				{
					Console.WriteLine(response.Content);
					return null;
				}
				byte[] responseData = Convert.FromBase64String(JsonDocument.Parse(response.Content.ReadAsStringAsync().Result).RootElement.GetProperty("image_base64").GetString());

				var image = new Image();
				image.ContentType = request.ContentType;
				image.UploadedAt = DateTime.Now;
				using (var ms = new MemoryStream())
				{
					image.Data = responseData;
				}
				_unitOfWork.ImageRepository.Create(image);
				await _unitOfWork.SaveChangesAsync();
				return $"https://api.speezsai.com/api/image/{image.Id}";
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}
	}

	public class ImageProcessRequest()
	{
		public byte[] Image { get; set; }
		public string Color { get; set; }	
	}
}
