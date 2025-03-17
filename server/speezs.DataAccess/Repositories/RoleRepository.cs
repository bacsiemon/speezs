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
	public class RoleRepository : GenericRepository<Role>
	{
		public RoleRepository(SpeezsDbContext context) : base(context)
		{
			
		}

		public async Task<Role?> GetByNameAsync(string name)
		{
			return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName.Equals(name));
		}
	}
}
