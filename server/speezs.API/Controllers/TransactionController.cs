using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using speezs.Services;
using speezs.Services.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace speezs.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private IConfiguration _configuration;

		public TransactionController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpPost("token_generate")]
		public IActionResult GenerateToken([FromHeader] string Authorization)
		{
			// Kiểm tra Authorization header
			if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Basic "))
			{
				return StatusCode(400, "Authorization header không hợp lệ");
			}

			// Giải mã Base64
			var base64Credentials = Authorization.Substring("Basic ".Length).Trim();
			var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(base64Credentials));
			var values = credentials.Split(':', 2);

			if (values.Length != 2)
			{
				return StatusCode(400, "Authorization header không hợp lệ");
			}

			var username = values[0];
			var password = values[1];
			var validUsername = _configuration["Vietqr:Username"];
			var validPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(_configuration["Vietqr:SecretKey"]));

			// Kiểm tra username và password
			if (username.Equals(validUsername) && password.Equals(validPassword))
			{
				var token = GenerateJwtToken(username);
				return StatusCode(200, token);
			}
			else
			{
				return StatusCode(401, "Username & Password không đúng");
			}
		}

		// Hàm tạo JWT token
		private string GenerateJwtToken(string username)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var secretKey = _configuration["Vietqr:SecretKey"];
			if (secretKey == null)
			{
				throw new Exception("Secret key chưa được cấu hình");
			}
			var key = Encoding.ASCII.GetBytes(secretKey);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, username)
				}),
				Expires = DateTime.UtcNow.AddMinutes(5), // Token hết hạn sau 5 phút
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}


		private const string BEARER_PREFIX = "Bearer ";
		[HttpPost("transaction-sync")]
		public IActionResult TransactionSync([FromBody] TransactionCallback transactionCallback)
		{
			// Lấy token từ header Authorization
			string authHeader = Request.Headers["Authorization"];
			if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith(BEARER_PREFIX))
			{
				return StatusCode(401, new ErrorResponse
				{
					Error = true,
					ErrorReason = "INVALID_AUTH_HEADER",
					ToastMessage = "Authorization header is missing or invalid",
					Object = null
				});
			}

			string token = authHeader.Substring(BEARER_PREFIX.Length).Trim();

			// Xác thực token
			if (!ValidateToken(token))
			{
				return StatusCode(401, new ErrorResponse
				{
					Error = true,
					ErrorReason = "INVALID_TOKEN",
					ToastMessage = "Invalid or expired token",
					Object = null
				});
			}

			// Xử lý logic của transaction
			try
			{
				// Ví dụ xử lý nghiệp vụ và sinh mã reftransactionid
				string refTransactionId = "GeneratedRefTransactionId"; // Tạo ID của giao dịch

				// Trả về response 200 OK với thông tin giao dịch
				return Ok(new SuccessResponse
				{
					Error = false,
					ErrorReason = null,
					ToastMessage = "Transaction processed successfully",
					Object = new TransactionResponseObject
					{
						reftransactionid = refTransactionId
					}
				});
			}
			catch (Exception ex)
			{
				// Trả về lỗi trong trường hợp có exception
				return StatusCode(400, new ErrorResponse
				{
					Error = true,
					ErrorReason = "TRANSACTION_FAILED",
					ToastMessage = ex.Message,
					Object = null
				});
			}
		}

		// Phương thức để xác thực token JWT
		private bool ValidateToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Vietqr:SecretKey"]);

			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero, // Không cho phép độ trễ thời gian
				}, out SecurityToken validatedToken);

				return true;
			}
			catch
			{
				return false;
			}
		}

		
	}

	// Lớp model cho request body
	public class TransactionCallback
	{
		public string transactionid { get; set; }
		public long transactiontime { get; set; }
		public string referencenumber { get; set; }
		public decimal amount { get; set; }
		public string content { get; set; }
		public string bankaccount { get; set; }
		public string orderId { get; set; }
		public string sign { get; set; }
		public string terminalCode { get; set; }
		public string urlLink { get; set; }
		public string serviceCode { get; set; }
		public string subTerminalCode { get; set; }
	}

	// Lớp model cho success response
	public class SuccessResponse
	{
		public bool Error { get; set; }
		public string ErrorReason { get; set; }
		public string ToastMessage { get; set; }
		public TransactionResponseObject Object { get; set; }
	}

	// Lớp model cho lỗi response
	public class ErrorResponse
	{
		public bool Error { get; set; }
		public string ErrorReason { get; set; }
		public string ToastMessage { get; set; }
		public object Object { get; set; }
	}

	// Lớp model cho object trả về trong success response
	public class TransactionResponseObject
	{
		public string reftransactionid { get; set; }
	}
}
