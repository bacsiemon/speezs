using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Transaction
{
	public class CreateTransactionRequest
	{
		public int UserId { get; set; }
		public decimal Amount { get; set; }

		[StringLength(100)]
		public string Description { get; set; }

		[StringLength(30)]
		public string FullName { get; set; }

		[StringLength(20)]
		public string Country { get; set; }

		[StringLength(30)]
		public string City { get; set; }

		[StringLength(10)]
		public string ZipCode { get; set; }

		[StringLength(50)]
		public string Address { get; set; }
	}
}
