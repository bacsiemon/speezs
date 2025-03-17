using speezs.DataAccess.Models;
using speezs.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.DataAccess.Repositories
{
	public class UserSubscriptionRepository : GenericRepository<UserSubscription>
	{
		public UserSubscriptionRepository(SpeezsDbContext context) : base(context)
		{
			// Any additional initialization if needed
		}
	}
}
