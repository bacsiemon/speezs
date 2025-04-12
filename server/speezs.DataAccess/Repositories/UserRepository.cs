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

		public new async Task<List<User>> GetAllAsync()
		{
			return await _context.Users.Where( u=> u.IsDeleted != true).ToListAsync();
		}

		public new async Task<User?> GetByIdAsync(int id)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id && u.IsDeleted != true);
		}

		public async Task<User?> GetByEmailAsync(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.IsDeleted != true);
		}

		public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
		{
			return await _context.Users.FirstOrDefaultAsync(u =>u.PhoneNumber.Equals(phoneNumber) && u.IsDeleted == false);
		}

		public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken.Equals(refreshToken) && u.IsDeleted != true);
		}
	}
}
