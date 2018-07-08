using Leo.Core.Orleans.Client;

namespace Leo.Core.Orleans
{
    public static class ClientConfigurationBuilderExtensions
    {
        public static ClientConfigurationBuilder WithSimpleStream(this ClientConfigurationBuilder builder, string providerName, bool fireAndForget = false)
        {
            return new SimpleStreamClientDecorator(builder, providerName, fireAndForget);
        }
    }
}