﻿using speezs.DataAccess.Models;
using speezs.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.DataAccess.Repositories
{
	public class CollectionLookRepository : GenericRepository<Collectionlook>
	{
		public CollectionLookRepository(SpeezsDbContext context) : base(context)
		{
		}
	}
}
