using speezs.DataAccess;
using speezs.DataAccess.Models;
using speezs.Services.Base;
using speezs.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services
{
	public class UserSubscriptionService : IUserSubscriptionService
	{
		private UnitOfWork _unitOfWork;
		public UserSubscriptionService(UnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
		}

		public async Task<IServiceResult> GetByUserIdAsync(int userId)
		{
			try
			{
				var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
					if (user == null) 
					return new ServiceResult(404, "User Not Found");
				var result = await _unitOfWork.UserSubscriptionRepository.GetByUserIdAsync(userId);
					if (result == null || result.EndDate < DateTime.Now)
					return await CreateFreeSubscriptionAsync(userId);

				return new ServiceResult(200, "Success", result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Abort();
				Console.WriteLine(ex.ToString());
				return new ServiceResult(500, ex.Message);
			}
		}


		public async Task<IServiceResult> CreateFreeSubscriptionAsync(int userId)
		{
			try
			{
				var subscription = new Usersubscription
				{
					UserId = userId,
					TierId = 1, // Assuming 1 is the ID for the free tier
					StartDate = DateTime.Now,
					EndDate = DateTime.Now.AddDays(30),
					Status = "PAID",
					AutoRenew = false,
					IsDeleted = false,
					TransfersLeft = 10
				};
				_unitOfWork.UserSubscriptionRepository.Create(subscription);
				await _unitOfWork.SaveChangesAsync();
				return new ServiceResult(200, "Success", subscription);
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
