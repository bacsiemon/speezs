using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Transfer
{
	public class CreateTransferRequest
	{
		public int UserId { get; set; }
		public int LookId { get; set; }
		public IFormFile Image { get; set; }
	}
}
