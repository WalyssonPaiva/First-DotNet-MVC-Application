
using Microsoft.Extensions.DependencyInjection;
using MVCStore.Domain.Interfaces;
using MVCStore.Domain.Notifications;
using MVCStore.Domain.Services;
using MVCStore.Infra.Data.Context;
using MVCStore.Infra.Data.Repository;

namespace MVCStore.Application.Config {
    public static class DependencyInjectionConfiguration {
        public static IServiceCollection ResolveDependencyInjection(this IServiceCollection services) {
            services.AddScoped<PgDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
           
            services.AddScoped<INotificator, Notificator>();
            services.AddScoped<IProductService, ProductService>();
            
            return services;
        }
    }
}