using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Transaction
{
	public class CreateTransactionResponse
	{
		public long OrderCode { get; set; }

		public string PaymentUrl { get; set; }
	}
}
