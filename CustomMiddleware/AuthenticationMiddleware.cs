using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InsuranceProviderManagementSystem.CustomMiddleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            if (path.StartsWithSegments("/Admin"))
            {
                // Check if the user is authenticated
                if (!context.User.Identity.IsAuthenticated)
                {
                    context.Response.Redirect("/Home/Login");
                    return;
                }

                // Check if the user has the required role
                if (!context.User.IsInRole("Admin"))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("You do not have permission to access this resource.");
                    return;
                }
            }

            // Proceed to the next middleware if authenticated and authorized
            await _next(context);
        }
    }
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}