using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Helpers
{
	public enum EStatusCode
	{
		SUCCESS = 200,
		CREATED = 201,
		NO_CONTENT = 204,
		NOT_FOUND = 404,
		INTERNAL_SERVER_ERROR = 500,
	}
}
