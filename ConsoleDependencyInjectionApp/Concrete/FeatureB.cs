using Microsoft.Extensions.Options;

namespace ConsoleDependencyInjectionApp
{
    public class FeatureB : IFeature
    {
        private readonly MySecretOption MySecretOption;
        private readonly MyLocalSecret MyLocalSecret;

        public FeatureB(IOptions<MySecretOption> mySecretOption, IOptions<MyLocalSecret> myLocalSecret)
        {
            MySecretOption = mySecretOption.Value;
            MyLocalSecret = myLocalSecret.Value;
        }
        public string GetSecretFromOption()
        {
            return $"Shhh... this is secret. It is {MySecretOption.Secret}, but could be {MyLocalSecret.LocalSecret}";
        }
    }
}