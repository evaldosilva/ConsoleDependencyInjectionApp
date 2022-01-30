using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConsoleDependencyInjectionApp
{
    public static class RegisterOptions
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            // *** Loading Options ***

            // Just have to match the parameters of the json with the properties of the class
            // Less interaction and manimupation
            // auto added to IOption DI
            services.Configure<MyParameterY>(configuration.GetSection("a"));

            // Loaded with string name
            var paramBOption = configuration.GetSection("OptionB").Get<OptionB>();
            // Loaded with nameof (better)
            var paramCOption = configuration.GetSection(nameof(MySecretOption)).Get<MySecretOption>();

            // Load my user secrets, no names related.
            // Both (B and C) are flexible, the value can be changed
            // As it is flexible, it can fill values on other options (not good, but flexible)
            var UserSecret = configuration.GetSection("Banana").Get<MyLocalSecret>();
            if (UserSecret != null)
                paramCOption.IsSecret = UserSecret.DoTheMagic;

            // And (B and C) have to be added manually
            services.AddOptions()
                .AddSingleton(Options.Create(paramBOption))
                .AddSingleton(Options.Create(paramCOption))
                .AddSingleton(Options.Create(UserSecret));
        }
    }
}