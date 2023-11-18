using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApplication1.Middleware
{
    public class StaticTokenAuthenticationMiddleware
    {
        private const string ExpectedToken = "YourHardcodedToken";
        private readonly RequestDelegate _next;

        public StaticTokenAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Missing authorization header.");
                return;
            }

            var token = authorizationHeader.ToString().Replace("Bearer ", "");
            if (token != ExpectedToken)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid token.");
                return;
            }

            await _next(context);
        }
    }
}
