using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication1.Middleware
{
    public class FormatTransformMiddleware
    {
        private readonly RequestDelegate _next;

        public FormatTransformMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Buffer the response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Call the next middleware
            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var query = context.Request.Query;
            var format = query["format"].ToString().ToLower();

            if (string.IsNullOrWhiteSpace(format) || format == "json")
            {
                // Default to JSON, no action needed
            }
            else if (format == "xml")
            {
                // Convert JSON to XML
                var token = JToken.Parse(text);
                string xmlData;
                if (token is JArray)
                {
                    xmlData = JsonConvert.DeserializeXmlNode("{\"Item\":" + text + "}", "Root").OuterXml;
                }
                else
                {
                    xmlData = JsonConvert.DeserializeXmlNode(text, "Root").OuterXml;
                }

                // Replace the response with XML
                context.Response.ContentType = "application/xml";
                await context.Response.WriteAsync(xmlData);
            }
            else
            {
                // Invalid format requested, return an error
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Invalid response format requested: '{format}'. Only 'json' and 'xml' are supported.");
                return;
            }

            // Copy the contents of the new response to the original response body
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
