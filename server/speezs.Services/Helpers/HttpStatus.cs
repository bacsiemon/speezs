using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Helpers
{
	public class HttpStatus
	{
		// 1xx Informational
		/// <summary>100 Continue</summary>
		public static readonly int CONTINUE = 100;
		/// <summary>101 Switching Protocols</summary>
		public static readonly int SWITCHING_PROTOCOLS = 101;
		/// <summary>102 Processing</summary>
		public static readonly int PROCESSING = 102;
		/// <summary>103 Early Hints</summary>
		public static readonly int EARLY_HINTS = 103;

		// 2xx Success
		/// <summary>200 OK</summary>
		public static readonly int OK = 200;
		/// <summary>201 Created</summary>
		public static readonly int CREATED = 201;
		/// <summary>202 Accepted</summary>
		public static readonly int ACCEPTED = 202;
		/// <summary>203 Non-Authoritative Information</summary>
		public static readonly int NON_AUTHORITATIVE_INFORMATION = 203;
		/// <summary>204 No Content</summary>
		public static readonly int NO_CONTENT = 204;
		/// <summary>205 Reset Content</summary>
		public static readonly int RESET_CONTENT = 205;
		/// <summary>206 Partial Content</summary>
		public static readonly int PARTIAL_CONTENT = 206;
		/// <summary>207 Multi-Status</summary>
		public static readonly int MULTI_STATUS = 207;
		/// <summary>208 Already Reported</summary>
		public static readonly int ALREADY_REPORTED = 208;
		/// <summary>226 IM Used</summary>
		public static readonly int IM_USED = 226;

		// 3xx Redirection
		/// <summary>300 Multiple Choices</summary>
		public static readonly int MULTIPLE_CHOICES = 300;
		/// <summary>301 Moved Permanently</summary>
		public static readonly int MOVED_PERMANENTLY = 301;
		/// <summary>302 Found</summary>
		public static readonly int FOUND = 302;
		/// <summary>303 See Other</summary>
		public static readonly int SEE_OTHER = 303;
		/// <summary>304 Not Modified</summary>
		public static readonly int NOT_MODIFIED = 304;
		/// <summary>307 Temporary Redirect</summary>
		public static readonly int TEMPORARY_REDIRECT = 307;
		/// <summary>308 Permanent Redirect</summary>
		public static readonly int PERMANENT_REDIRECT = 308;

		// 4xx Client Error
		/// <summary>400 Bad Request</summary>
		public static readonly int BAD_REQUEST = 400;
		/// <summary>401 Unauthorized</summary>
		public static readonly int UNAUTHORIZED = 401;
		/// <summary>402 Payment Required</summary>
		public static readonly int PAYMENT_REQUIRED = 402;
		/// <summary>403 Forbidden</summary>
		public static readonly int FORBIDDEN = 403;
		/// <summary>404 Not Found</summary>
		public static readonly int NOT_FOUND = 404;
		/// <summary>405 Method Not Allowed</summary>
		public static readonly int METHOD_NOT_ALLOWED = 405;
		/// <summary>406 Not Acceptable</summary>
		public static readonly int NOT_ACCEPTABLE = 406;
		/// <summary>407 Proxy Authentication Required</summary>
		public static readonly int PROXY_AUTHENTICATION_REQUIRED = 407;
		/// <summary>408 Request Timeout</summary>
		public static readonly int REQUEST_TIMEOUT = 408;
		/// <summary>409 Conflict</summary>
		public static readonly int CONFLICT = 409;
		/// <summary>410 Gone</summary>
		public static readonly int GONE = 410;
		/// <summary>411 Length Required</summary>
		public static readonly int LENGTH_REQUIRED = 411;
		/// <summary>412 Precondition Failed</summary>
		public static readonly int PRECONDITION_FAILED = 412;
		/// <summary>413 Payload Too Large</summary>
		public static readonly int PAYLOAD_TOO_LARGE = 413;
		/// <summary>414 URI Too Long</summary>
		public static readonly int URI_TOO_LONG = 414;
		/// <summary>415 Unsupported Media Type</summary>
		public static readonly int UNSUPPORTED_MEDIA_TYPE = 415;
		/// <summary>416 Range Not Satisfiable</summary>
		public static readonly int RANGE_NOT_SATISFIABLE = 416;
		/// <summary>417 Expectation Failed</summary>
		public static readonly int EXPECTATION_FAILED = 417;
		/// <summary>418 I'm a teapot</summary>
		public static readonly int IM_A_TEAPOT = 418;
		/// <summary>421 Misdirected Request</summary>
		public static readonly int MISDIRECTED_REQUEST = 421;
		/// <summary>422 Unprocessable Entity</summary>
		public static readonly int UNPROCESSABLE_ENTITY = 422;
		/// <summary>423 Locked</summary>
		public static readonly int LOCKED = 423;
		/// <summary>424 Failed Dependency</summary>
		public static readonly int FAILED_DEPENDENCY = 424;
		/// <summary>425 Too Early</summary>
		public static readonly int TOO_EARLY = 425;
		/// <summary>426 Upgrade Required</summary>
		public static readonly int UPGRADE_REQUIRED = 426;
		/// <summary>428 Precondition Required</summary>
		public static readonly int PRECONDITION_REQUIRED = 428;
		/// <summary>429 Too Many Requests</summary>
		public static readonly int TOO_MANY_REQUESTS = 429;
		/// <summary>431 Request Header Fields Too Large</summary>
		public static readonly int REQUEST_HEADER_FIELDS_TOO_LARGE = 431;
		/// <summary>451 Unavailable For Legal Reasons</summary>
		public static readonly int UNAVAILABLE_FOR_LEGAL_REASONS = 451;

		// 5xx Server Error
		/// <summary>500 Internal Server Error</summary>
		public static readonly int INTERNAL_SERVER_ERROR = 500;
		/// <summary>501 Not Implemented</summary>
		public static readonly int NOT_IMPLEMENTED = 501;
		/// <summary>502 Bad Gateway</summary>
		public static readonly int BAD_GATEWAY = 502;
		/// <summary>503 Service Unavailable</summary>
		public static readonly int SERVICE_UNAVAILABLE = 503;
		/// <summary>504 Gateway Timeout</summary>
		public static readonly int GATEWAY_TIMEOUT = 504;
		/// <summary>505 HTTP Version Not Supported</summary>
		public static readonly int HTTP_VERSION_NOT_SUPPORTED = 505;
		/// <summary>506 Variant Also Negotiates</summary>
		public static readonly int VARIANT_ALSO_NEGOTIATES = 506;
		/// <summary>507 Insufficient Storage</summary>
		public static readonly int INSUFFICIENT_STORAGE = 507;
		/// <summary>508 Loop Detected</summary>
		public static readonly int LOOP_DETECTED = 508;
		/// <summary>510 Not Extended</summary>
		public static readonly int NOT_EXTENDED = 510;
		/// <summary>511 Network Authentication Required</summary>
		public static readonly int NETWORK_AUTHENTICATION_REQUIRED = 511;
	}
}
