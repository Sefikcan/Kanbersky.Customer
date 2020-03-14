using Kanbersky.Customer.Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Kanbersky.Customer.Core.Extensions
{
    public static class RegistrationExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
