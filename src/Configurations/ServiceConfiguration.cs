
using Microsoft.Extensions.DependencyInjection;
using myBURGUERMANIA_API.Services;

namespace myBURGUERMANIA_API.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<ProductService>(); // Alterado para Scoped
            services.AddScoped<OrderService>(); 
            services.AddScoped<CategoryService>();
            services.AddScoped<StatusService>();
            services.AddScoped<LoginService>(); 
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}