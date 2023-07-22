using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, ProductsRepository>();
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(opts => opts.UseLazyLoadingProxies().UseSqlServer(connectionString));
            services.AddRepositories();

            return services;
        }
    }
}
