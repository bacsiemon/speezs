using speezs.DataAccess;
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
				var result = await _unitOfWork.UserSubscriptionRepository.GetByUserIdAsync(userId);
				return new ServiceResult(200, "Success", result);
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
