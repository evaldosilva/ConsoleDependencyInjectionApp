using Microsoft.Extensions.Options;
using System;

namespace ConsoleDependencyInjectionApp
{
    class MyConsoleService : IMyConsoleService
    {
        private readonly FeatureServiceResolver serviceResolver;
        private readonly MySecretOption mySecretOption;
        private readonly OptionB optionB;

        public MyConsoleService(FeatureServiceResolver serviceResolver, IOptions<MySecretOption> mySecretOption, IOptions<OptionB> optionB)
        {
            this.serviceResolver = serviceResolver;
            this.mySecretOption = mySecretOption.Value;
            this.optionB = optionB.Value;
        }

        public void WriteToConsoleSomething()
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("This is my registered service.");

            Console.WriteLine($"The [{nameof(optionB.ConfigB)}] value is [{optionB.ConfigB}]");

            // Here we call our service resolver to load, depending on our custom code, the right class to be injected
            IFeature feature = serviceResolver(mySecretOption); 
            Console.WriteLine($"My secret and variable feature is [{feature.GetSecretFromOption()}]");

            Console.WriteLine("This is the end.");
        }
    }
}