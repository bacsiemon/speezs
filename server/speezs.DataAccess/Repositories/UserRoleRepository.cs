using Microsoft.EntityFrameworkCore;
using speezs.DataAccess.Base;
using speezs.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.DataAccess.Repositories
{
	public class UserRoleRepository : GenericRepository<UserRole>
	{
		public UserRoleRepository(SpeezsDbContext context) : base(context)
		{
			
		}

		public async Task<int> GetRoleCountAsync(int roleId)
		{
			return await _context.Userroles.CountAsync(b => b.RoleId == roleId);
		}
	}
}
