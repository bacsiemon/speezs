﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Image
{
	public class CreateImageRequest
	{
		public IFormFile Image { get; set; }
	}
}
