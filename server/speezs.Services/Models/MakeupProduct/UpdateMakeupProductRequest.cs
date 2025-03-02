using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.MakeupProduct
{
	public class UpdateMakeupProductRequest
	{
		public int ProductId { get; set; }

		public string? Name { get; set; }

		public string? Brand { get; set; }

		public string? Category { get; set; }

		public string? ColorCode { get; set; }

		public string? Description { get; set; }

		public string? ImageUrl { get; set; }

		public bool? IsVerified { get; set; }
	}
}
