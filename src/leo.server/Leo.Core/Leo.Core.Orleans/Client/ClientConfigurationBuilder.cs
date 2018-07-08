using Orleans.Runtime.Configuration;

namespace Leo.Core.Orleans.Client
{
    public abstract class ClientConfigurationBuilder
    {
        public abstract ClientConfiguration Build();
    }
}