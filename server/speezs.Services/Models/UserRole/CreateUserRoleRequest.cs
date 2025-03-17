using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.UserRole
{
	public class CreateUserRoleRequest
	{
		public int UserId { get; set; }
		public int RoleId { get; set; }
	}
}
