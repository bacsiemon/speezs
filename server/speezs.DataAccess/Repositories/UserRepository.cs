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
	public class UserRepository : GenericRepository<User>
	{
		public UserRepository(SpeezsDbContext context) : base(context)
		{
		}

		public async Task<User?> GetByEmailAsync(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
		}
	}
}
