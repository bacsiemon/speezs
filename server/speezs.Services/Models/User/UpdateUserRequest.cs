﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.User
{
	public class UpdateUserRequest
	{
		public int Id { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? PhoneNumber { get; set; }
		public string? FullName { get; set; }
		public string? ProfileImageUrl { get; set; }
		public int? RoleId { get; set; }
	}
}
