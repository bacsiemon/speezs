﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Auth
{
	public class LoginResponseModel
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public int UserId { get; set; }
	}
}
