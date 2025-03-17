namespace speezs.Services.Models.UserSubscription
{
	public class CreateUserSubscriptionRequest
	{
		public int? UserId { get; set; }

		public int? TierId { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Status { get; set; }

		public string PaymentMethod { get; set; }

		public bool? AutoRenew { get; set; }
	}
}