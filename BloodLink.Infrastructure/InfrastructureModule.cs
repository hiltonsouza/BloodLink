using BloodLink.Core.Repositories;
using BloodLink.Core.Services;
using BloodLink.Infrastructure.Persistence;
using BloodLink.Infrastructure.Persistence.Repositories;
using BloodLink.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BloodLink.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRepositories()
                .AddDbContext(configuration)
                .AddUnitOfWork()
                .AddExternalServices();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IDonorRepository, DonorRepository>();
            services.AddScoped<IBloodStockRepository, BloodStockRepository>();

            return services;
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BloodLinkDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>(); // Ensure NotificationService implements INotificationService
            services.AddTransient<IViaCepService,ViaCepService>();
            return services;
        }
    }
}
