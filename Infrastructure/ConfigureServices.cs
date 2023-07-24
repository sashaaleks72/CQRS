using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;
using Amazon.S3;
using Infrastructure.Services;
using Application.Interfaces.Services;
using Amazon.SimpleEmail;
using Amazon;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, ProductsRepository>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, TeapotImageService>();
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(opts => opts.UseLazyLoadingProxies().UseSqlServer(connectionString));
            services.AddAWSService<IAmazonS3>();
            services.AddTransient(a => new AmazonSimpleEmailServiceClient(RegionEndpoint.EUCentral1));
            services.AddServices();
            services.AddRepositories();

            return services;
        }
    }
}
