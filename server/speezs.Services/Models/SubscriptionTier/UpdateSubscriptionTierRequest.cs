using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.SubscriptionTier
{
	public class UpdateSubscriptionTierRequest
	{
		public int TierId { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }

		public decimal? Price { get; set; }

		public int? DurationDays { get; set; }

		public int? MaxTransfers { get; set; }

		public int? MaxCollections { get; set; }

		public bool? AllowsCommercialUse { get; set; }

		public bool? PriorityProcessing { get; set; }

		public bool? IsActive { get; set; }
	}
}
