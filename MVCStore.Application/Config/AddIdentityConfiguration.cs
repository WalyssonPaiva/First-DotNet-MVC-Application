using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCStore.Application.Data;

namespace MVCStore.Application.Config {
    public static class AddIdentityConfiguration {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services,
            IConfiguration configuration) {
            
            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}