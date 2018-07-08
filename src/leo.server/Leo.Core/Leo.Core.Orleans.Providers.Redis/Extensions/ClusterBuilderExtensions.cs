using Leo.Core.Orleans.Providers.Redis;

namespace Leo.Core.Orleans.Providers
{
    public static class ClusterBuilderExtensions
    {
        public static ClusterBuilder WithRedisStorage(this ClusterBuilder builder, string providerName)
        {
            return new ClusterStorageDecorator<RedisStorageProvider>(builder, providerName);
        }
    }
}