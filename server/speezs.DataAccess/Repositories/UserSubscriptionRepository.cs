using speezs.DataAccess.Models;
using speezs.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace speezs.DataAccess.Repositories
{
	public class UserSubscriptionRepository : GenericRepository<Usersubscription>
	{
		public UserSubscriptionRepository(SpeezsDbContext context) : base(context)
		{
			// Any additional initialization if needed
		}

		public async Task<Usersubscription?> GetByUserIdAsync(int userId)
		{
			return await _context.Usersubscriptions.OrderBy(u => u.UserSubscriptionId)
				.LastOrDefaultAsync(us => us.UserId == userId && us.IsDeleted != true);
		}


		
	}
}
