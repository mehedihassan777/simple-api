using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace UserManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            Debug.WriteLine($"[Request] {context.Request.Method} {context.Request.Path}");
            
            // Call the next middleware in the pipeline
            await _next(context);
            
            Debug.WriteLine($"[Response] Completed processing the request.");
        }
    }
}
