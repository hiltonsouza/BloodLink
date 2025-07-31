using BloodLink.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BloodLink.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediator()
                .AddValidation();

            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            return services;
        }
        private static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<CreateDonorCommandValidator>();

            return services;
        }
    }
}
