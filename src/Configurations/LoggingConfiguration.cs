
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace myBURGUERMANIA_API.Configurations
{
    public static class LoggingConfiguration
    {
        public static void AddLoggingConfiguration(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
        }

        public static void UseRequestLogging(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
                await next.Invoke();
                Console.WriteLine($"Response: {context.Response.StatusCode}");
            });
        }
    }
}