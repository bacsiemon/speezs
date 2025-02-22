using speezs.DataAccess.Models;
using speezs.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.DataAccess.Repositories
{
	public class LookRepository : GenericRepository<Look>
	{
		public LookRepository(SpeezsDbContext context) : base(context)
		{
		}
	}
}
