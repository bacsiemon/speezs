using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Look
{
	public class UpdateLookRequest
	{
		public int LookId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public bool? IsPublic { get; set; }
		public string? Category { get; set; }
		public string? ThumbnailUrl { get; set; }
		public string? Color { get; set; }
	}
}
