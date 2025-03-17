using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Models.Role
{
	public class UpdateRoleRequest
	{
		public int Id { get; set; }

		public string? RoleName {  get; set; }
	}
}
