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
	public class MakeupProductRepository : GenericRepository<Makeupproduct>
	{
		public MakeupProductRepository(SpeezsDbContext context) : base(context)
		{
		}

		public async Task<List<Makeupproduct>> GetByLookIdAsync(int lookId)
		{
			return await _context.Makeupproducts.Include(x => x.Lookproducts).Where(x =>x.Lookproducts.Any(y => y.LookId == lookId))
				.ToListAsync();
		}
	}
}
