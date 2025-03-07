using speezs.DataAccess.Base;
using speezs.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.DataAccess.Repositories
{
	public class UserRoleRepository : GenericRepository<Userrole>
	{
		public UserRoleRepository(SpeezsDbContext context) : base(context)
		{
			
		}
	}
}
