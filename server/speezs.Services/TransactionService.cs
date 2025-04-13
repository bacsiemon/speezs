using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Net.payOS;
using Net.payOS.Types;
using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.DataAccess.Paging;
using speezs.Services.Base;
using speezs.Services.Helpers;
using speezs.Services.Interfaces;
using speezs.Services.Models.Transaction;
using speezs.Services.Models.UserSubscription;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace speezs.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly string STATUS_PENDING = "PENDING";
		private readonly string STATUS_PROCESSING = "PROCESSING";
		private readonly string STATUS_COMPLETED = "PAID";
		private readonly string STATUS_FAILED = "FAILED";
		private readonly string STATUS_REFUNDED = "REFUNDED";
		private readonly string STATUS_CANCELED = "CANCELLED";

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

		public async Task<IServiceResult> CreateAsync(CreateTransactionRequest request)
		{
			try
			{
				var subscriptionTier = await _unitOfWork.SubscriptionTierRepository.GetByIdAsync(request.SubscriptionTierId);
				if (subscriptionTier == null)
					return new ServiceResult(HttpStatus.NOT_FOUND, "Subscription Tier Not Found");

				var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
				if (user == null)
					return new ServiceResult(HttpStatus.NOT_FOUND, "User Not Found");

				var entity = _mapper.Map<DataAccess.Models.Transaction>(request);
				entity.Status = STATUS_PENDING;
				entity.Amount = subscriptionTier.Price;
				entity.SubscriptionTierId = request.SubscriptionTierId;
				entity.Description = request.Description;
				_unitOfWork.TransactionRepository.Create(entity);
				await _unitOfWork.SaveChangesAsync();

				PayOS payOS = new PayOS(
					_configuration["PayOS:ClientId"],
					_configuration["PayOS:ApiKey"],
					_configuration["PayOS:ChecksumKey"]);


				var items = new List<Net.payOS.Types.ItemData>()
					{
					new(name : subscriptionTier.Name,
						quantity:1,
						price:Convert.ToInt32(subscriptionTier.Price))
					};

				var paymentData = new Net.payOS.Types.PaymentData(
					orderCode: Convert.ToInt64(entity.Id),
					amount: Convert.ToInt32(subscriptionTier.Price),
					description: $"SPEEZS_{request.UserId}_{entity.Description}",
					items: items,
					cancelUrl: request.CancelUrl,
					returnUrl: request.ReturnUrl
					);
				var result = await payOS.createPaymentLink(paymentData);
				return new ServiceResult(HttpStatus.OK, "Success", result.checkoutUrl);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(HttpStatus.INTERNAL_SERVER_ERROR, ex.Message);
			}

		}

		public async Task<IServiceResult> CancelTransactionAsync(long orderCode)
		{
			try
			{
				var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(Convert.ToInt32(orderCode));
				if (transaction == null)
					return new ServiceResult(HttpStatus.NOT_FOUND, "Transaction Not Found");

				PayOS payOS = new PayOS(
						_configuration["PayOS:ClientId"],
						_configuration["PayOS:ApiKey"],
						_configuration["PayOS:ChecksumKey"]);

				var cancelInfo = await payOS.cancelPaymentLink(orderCode);
				if (cancelInfo == null)
					return new ServiceResult(HttpStatus.NOT_FOUND, "OrderCode Not Found");

				transaction.Status = STATUS_CANCELED;
				transaction.CompletedAt = DateTime.Now;
				_unitOfWork.TransactionRepository.Update(transaction);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(HttpStatus.OK, "Success");
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(HttpStatus.INTERNAL_SERVER_ERROR, ex.Message);
			}
		}

		public async Task<IServiceResult> ProcessTransactionAsync(ProcessTransactionRequest request)
		{
			try
			{
				var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(Convert.ToInt32(request.OrderCode));
				if (transaction == null)
					return new ServiceResult(HttpStatus.NOT_FOUND, "Transaction Not Found");
				transaction.Status = request.Status;
				transaction.Description = request.Id;
				_unitOfWork.TransactionRepository.Update(transaction);
				await _unitOfWork.SaveChangesAsync();

				PayOS payOS = new PayOS(
						_configuration["PayOS:ClientId"],
						_configuration["PayOS:ApiKey"],
						_configuration["PayOS:ChecksumKey"]);

				var linkInfo = await payOS.getPaymentLinkInformation(request.OrderCode);
				transaction.Status = linkInfo.status;
				transaction.CompletedAt = DateTime.Now;
				_unitOfWork.TransactionRepository.Update(transaction);
				await _unitOfWork.SaveChangesAsync();
				if (request.Status.Equals(STATUS_COMPLETED))
					return await OnTransactionSuccessAsync(transaction.UserId, transaction.SubscriptionTierId.Value);
				return new ServiceResult(HttpStatus.OK, "Success", transaction);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(HttpStatus.INTERNAL_SERVER_ERROR, ex.Message);
			}
		}

		public async Task<IServiceResult> ProcessWebHookAsync(WebhookType webhookBody)
		{
			try
			{
				var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(Convert.ToInt32(webhookBody.code));
				if (transaction == null)
					return new ServiceResult(HttpStatus.NOT_FOUND, "Transaction Not Found");

				PayOS payOS = new PayOS(
						_configuration["PayOS:ClientId"],
						_configuration["PayOS:ApiKey"],
						_configuration["PayOS:ChecksumKey"]);
				payOS.confirmWebhook(_configuration["PayOS:WebhookUrl"]);
				WebhookData webhookData = payOS.verifyPaymentWebhookData(webhookBody);
				transaction.Status = webhookBody.success ? STATUS_COMPLETED : STATUS_FAILED;
				transaction.AccountNumber = webhookData.accountNumber;

				_unitOfWork.TransactionRepository.Update(transaction);
				await _unitOfWork.SaveChangesAsync();

				if (!webhookBody.success)
					return new ServiceResult(HttpStatus.OK, "Thành Công");
				return await OnTransactionSuccessAsync(transaction.UserId, transaction.SubscriptionTierId.Value);

			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(HttpStatus.INTERNAL_SERVER_ERROR, ex.Message);
			}
		}


		private async Task<IServiceResult> OnTransactionSuccessAsync(int userId, int tierId)
		{
			try
			{
				var userSubscription = await _unitOfWork.UserSubscriptionRepository.GetByUserIdAsync(userId);
				bool isCreate = userSubscription == null;
				if (userSubscription == null)
					userSubscription = new();
					userSubscription.UserId = userId;
					userSubscription.TierId = tierId;
					userSubscription.StartDate = DateTime.Now;
					userSubscription.EndDate = DateTime.Now.AddDays(30);
					userSubscription.Status = STATUS_COMPLETED;
					userSubscription.PaymentMethod = "Chuyển Khoản";
					userSubscription.AutoRenew = false;
					userSubscription.LastBillingDate = DateTime.Now;
					userSubscription.NextBillingDate = DateTime.Now.AddDays(30);
				if (isCreate)	
					_unitOfWork.UserSubscriptionRepository.Create(userSubscription);
				else
					_unitOfWork.UserSubscriptionRepository.Update(userSubscription);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(HttpStatus.OK, "Success");
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