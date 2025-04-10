using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Helpers
{
	public class GmailHelper
	{
		private IConfiguration _configuration;

		public GmailHelper(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		//private void InitializeConfiguration()
		//{
		//	var path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
		//	_configuration = new ConfigurationBuilder()
		//		.SetBasePath(path + "\\EduToyRent.API")
		//		.AddJsonFile("appsettings.json")
		//		.Build();
		//}

		public bool SendEmailSingle(EmailRequestModel request)
		{
			try
			{
				var message = new MailMessage();
				message.From = new MailAddress(_configuration["Smtp:Email"]);
				message.To.Add(new MailAddress(request.ReceiverEmail));
				message.Subject = request.EmailSubject;
				message.Body = request.EmailBody;
				message.IsBodyHtml = request.IsHtml;

				var smtpClient = new SmtpClient("smtp.gmail.com")
				{
					Port = int.Parse(_configuration["Smtp:Port"]),
					Credentials = new NetworkCredential(_configuration["Smtp:Email"], _configuration["Smtp:Password"]),
					EnableSsl = true,
				};

				smtpClient.Send(message);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}
	}
	public class EmailRequestModel
	{
		public string ReceiverEmail { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public bool IsHtml { get; set; }
	}

	public class EmailTemplates
	{
		public static string ResetPasswordEmail(string code)
		{
			return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Password Reset OTP</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333333;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }}
        .header {{
            text-align: center;
            padding: 20px 0;
            background-color: #f8f9fa;
        }}
        .content {{
            padding: 30px 20px;
            background-color: #ffffff;
        }}
        .otp-container {{
            text-align: center;
            padding: 20px;
            margin: 20px 0;
            background-color: #f8f9fa;
            border-radius: 5px;
        }}
        .otp-code {{
            font-size: 32px;
            font-weight: bold;
            letter-spacing: 5px;
            color: #007bff;
        }}
        .footer {{
            text-align: center;
            padding: 20px;
            font-size: 12px;
            color: #666666;
        }}
        @media only screen and (max-width: 600px) {{
            .container {{
                width: 100% !important;
            }}
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h2>Password Reset Code</h2>
        </div>
        
        <div class=""content"">
            <p>Hello,</p>
            
            <p>We received a request to reset your password. Please use the following One-Time Password (OTP) to complete your password reset:</p>
            
            <div class=""otp-container"">
                <div class=""otp-code"">{code}</div>
            </div>
            
            <p>This code will expire in 30 minutes.</p>
            
            <p>Best regards,<br>Speezs</p>
        </div>
        
        <div class=""footer"">
            <p>This is an automated message, please do not reply to this email.</p>
            <p>© 2025 Speezs. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
		}
	}
}
