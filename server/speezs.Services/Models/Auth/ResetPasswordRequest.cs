using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Auth
{
	public class ResetPasswordRequest
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Code { get; set; }

		[Required]
		[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least 8 characters, 1 uppercase character, 1 lowercase character, and 1 number")]
		public string Password { get; set; }
	}
}
