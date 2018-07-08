using Orleans.Runtime.Configuration;

namespace Leo.Core.Orleans.Client
{
    public class DefaultClientConfiguration : ClientConfigurationBuilder
    {
        public override ClientConfiguration Build()
        {
            return new ClientConfiguration();
        }
    }
}