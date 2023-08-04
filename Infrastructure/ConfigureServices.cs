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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, TeapotImageService>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddCognitoIdentity();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://cognito-idp.{configuration["AWS:Region"]}.amazonaws.com/{configuration["AWS:UserPoolId"]}";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://cognito-idp.{configuration["AWS:Region"]}.amazonaws.com/{configuration["AWS:UserPoolId"]}",
                    ValidateLifetime = true,
                    LifetimeValidator = (_, expires, _, _) => expires > DateTime.UtcNow,
                    ValidateAudience = false,
                    RoleClaimType = "custom:role"
                };
            });

            services.AddDbContext<ApplicationDbContext>(opts => opts.UseLazyLoadingProxies().UseSqlServer(connectionString));
            services.AddAWSService<IAmazonS3>();
            services.AddTransient(a => new AmazonSimpleEmailServiceClient(RegionEndpoint.EUCentral1));
            services.AddServices();
            services.AddRepositories();

            return services;
        }
    }
}
