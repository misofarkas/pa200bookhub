using System.Threading.Tasks;

namespace WebApplication1.Middleware
{
	public class RequestLoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<RequestLoggingMiddleware> _logger;

		public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			_logger.LogInformation($"Received request: {context.Request.Method} {context.Request.Path}");

			await _next(context);

            _logger.LogInformation($"Response status code: {context.Response.StatusCode}");
        }
	}
}
