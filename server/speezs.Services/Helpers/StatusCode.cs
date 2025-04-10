using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Services.Helpers
{
	public class StatusCode
	{
	// 1xx Informational
	public static readonly int CONTINUE = 100;
	public static readonly int SWITCHING_PROTOCOLS = 101;
	public static readonly int PROCESSING = 102;
	public static readonly int EARLY_HINTS = 103;

	// 2xx Success
	public static readonly int OK = 200;
	public static readonly int CREATED = 201;
	public static readonly int ACCEPTED = 202;
	public static readonly int NON_AUTHORITATIVE_INFORMATION = 203;
	public static readonly int NO_CONTENT = 204;
	public static readonly int RESET_CONTENT = 205;
	public static readonly int PARTIAL_CONTENT = 206;
	public static readonly int MULTI_STATUS = 207;
	public static readonly int ALREADY_REPORTED = 208;
	public static readonly int IM_USED = 226;

	// 3xx Redirection
	public static readonly int MULTIPLE_CHOICES = 300;
	public static readonly int MOVED_PERMANENTLY = 301;
	public static readonly int FOUND = 302;
	public static readonly int SEE_OTHER = 303;
	public static readonly int NOT_MODIFIED = 304;
	public static readonly int TEMPORARY_REDIRECT = 307;
	public static readonly int PERMANENT_REDIRECT = 308;

	// 4xx Client Error
	public static readonly int BAD_REQUEST = 400;
	public static readonly int UNAUTHORIZED = 401;
	public static readonly int PAYMENT_REQUIRED = 402;
	public static readonly int FORBIDDEN = 403;
	public static readonly int NOT_FOUND = 404;
	public static readonly int METHOD_NOT_ALLOWED = 405;
	public static readonly int NOT_ACCEPTABLE = 406;
	public static readonly int PROXY_AUTHENTICATION_REQUIRED = 407;
	public static readonly int REQUEST_TIMEOUT = 408;
	public static readonly int CONFLICT = 409;
	public static readonly int GONE = 410;
	public static readonly int LENGTH_REQUIRED = 411;
	public static readonly int PRECONDITION_FAILED = 412;
	public static readonly int PAYLOAD_TOO_LARGE = 413;
	public static readonly int URI_TOO_LONG = 414;
	public static readonly int UNSUPPORTED_MEDIA_TYPE = 415;
	public static readonly int RANGE_NOT_SATISFIABLE = 416;
	public static readonly int EXPECTATION_FAILED = 417;
	public static readonly int IM_A_TEAPOT = 418;
	public static readonly int MISDIRECTED_REQUEST = 421;
	public static readonly int UNPROCESSABLE_ENTITY = 422;
	public static readonly int LOCKED = 423;
	public static readonly int FAILED_DEPENDENCY = 424;
	public static readonly int TOO_EARLY = 425;
	public static readonly int UPGRADE_REQUIRED = 426;
	public static readonly int PRECONDITION_REQUIRED = 428;
	public static readonly int TOO_MANY_REQUESTS = 429;
	public static readonly int REQUEST_HEADER_FIELDS_TOO_LARGE = 431;
	public static readonly int UNAVAILABLE_FOR_LEGAL_REASONS = 451;

	// 5xx Server Error
	public static readonly int INTERNAL_SERVER_ERROR = 500;
	public static readonly int NOT_IMPLEMENTED = 501;
	public static readonly int BAD_GATEWAY = 502;
	public static readonly int SERVICE_UNAVAILABLE = 503;
	public static readonly int GATEWAY_TIMEOUT = 504;
	public static readonly int HTTP_VERSION_NOT_SUPPORTED = 505;
	public static readonly int VARIANT_ALSO_NEGOTIATES = 506;
	public static readonly int INSUFFICIENT_STORAGE = 507;
	public static readonly int LOOP_DETECTED = 508;
	public static readonly int NOT_EXTENDED = 510;
	public static readonly int NETWORK_AUTHENTICATION_REQUIRED = 511;
}
}
