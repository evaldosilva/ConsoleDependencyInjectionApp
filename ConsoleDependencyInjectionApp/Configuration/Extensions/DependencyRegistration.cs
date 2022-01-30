using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConsoleDependencyInjectionApp
{
    public static class DependencyRegistration
    {
        public static IServiceCollection RegisterMyDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Here we extend 1 method (IServiceCollection RegisterMyDependencies) and call static classes
            // to do the job to register classes and options
            RegisterServices.Register(services);
            RegisterOptions.Register(services, configuration);
            return services;
        }
    }
}