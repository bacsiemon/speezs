using speezs.DataAccess.Base;
using speezs.DataAccess.Models;
using speezs.DataAccess.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace speezs.DataAccess.Repositories
{
	public class TransactionRepository : GenericRepository<Transaction>
	{
		public TransactionRepository(SpeezsDbContext context) : base(context)
		{
			
		}
	}
}
