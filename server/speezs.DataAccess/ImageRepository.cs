using speezs.DataAccess.Base;
using speezs.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.DataAccess
{
	public class ImageRepository : GenericRepository<Image>
	{
		public ImageRepository(SpeezsDbContext context) : base(context)
		{
			
		}
	}
}
