using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDependencyInjectionApp
{
    internal static class RegisterServices
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IMyConsoleService, MyConsoleService>();

            // With this, the dependency of class that implements the interface that the delegate FeatureServiceResolver implements
            // (IFeature in this case) will be resolved in real time.
            services.AddTransient<FeatureServiceResolver>(
                serviceProvider => mySecretOption =>
                {
                    // It is checked from an option from appsettings.json, filled with a field from usersecrets.json
                    // The correct way should be read straight from the usersecrets.json the properties for the
                    // class to be resolved.
                    return (mySecretOption != null && mySecretOption.IsSecret) ?
                        serviceProvider.GetService<FeatureB>() :
                        serviceProvider.GetService<FeatureA>();
                });

            // The drawback is we have to add "fixed" references to the classes that will be resolved
            // Otherwise, if we add interface references, we get an error. It does not know how to resolve
            // services.AddTransient<IFeature, FeatureA>(); // <- error, if need to be used/ resolved
            services.AddTransient<FeatureB>();
            services.AddTransient<FeatureA>();
        }
    }
}