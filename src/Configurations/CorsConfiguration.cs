using Microsoft.Extensions.DependencyInjection;

namespace myBURGUERMANIA_API.Configurations
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "https://my-burguermania.vercel.app")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // Adicione esta linha para permitir credenciais
                });
            });

            return services;
        }
    }
}