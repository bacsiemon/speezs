﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Review
{

	public class CreateReviewRequest
	{
		public int LookId { get; set; }
		public int UserId { get; set; }

		[Range(1,5)]
		public int Rating { get; set; }
		public string Comment { get; set; }
	}
}
