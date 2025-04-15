using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Look
{
	public class CreateLookRequest
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int? CreatedBy { get; set; }
		public bool? IsPublic { get; set; }
		public string Category { get; set; }
		public string ThumbnailUrl { get; set; }
		public string Color { get; set; }
	}
}
