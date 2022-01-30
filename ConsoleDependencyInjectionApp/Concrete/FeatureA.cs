using Microsoft.Extensions.Options;

namespace ConsoleDependencyInjectionApp
{
    public class FeatureA : IFeature
    {
        private readonly MyParameterY MyParameterYOption;

        public FeatureA(IOptions<MyParameterY> myParameterYOption)
        {
            MyParameterYOption = myParameterYOption.Value;
        }

        public string GetSecretFromOption()
        {
            return MyParameterYOption.a + MyParameterYOption.b;
        }
    }
}