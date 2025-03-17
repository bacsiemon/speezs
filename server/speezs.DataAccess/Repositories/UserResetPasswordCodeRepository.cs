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
	public class UserResetPasswordCodeRepository : GenericRepository<UserResetPasswordCode>
	{
		public UserResetPasswordCodeRepository(SpeezsDbContext context) : base(context)
		{
		}

		public async Task<UserResetPasswordCode?> GetByEmailAsync(string email)
		{
			return await _context.Userresetpasswordcodes.FirstOrDefaultAsync(u => u.Email.Equals(email));
		}
	}
}
