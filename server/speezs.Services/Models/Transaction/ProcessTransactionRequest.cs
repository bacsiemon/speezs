using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Transaction
{
	public class ProcessTransactionRequest
	{
		public string Code { get; set; }
		public string Id { get; set; }
		public bool Cancel { get; set; }
		public string Status { get; set; }
		public long OrderCode { get; set; }
	}
}
