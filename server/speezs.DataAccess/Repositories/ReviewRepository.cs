﻿using speezs.DataAccess.Models;
using speezs.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.DataAccess.Repositories
{
	public class ReviewRepository : GenericRepository<Review>
	{
		public ReviewRepository(SpeezsDbContext context) : base(context)
		{
		}
	}
}
