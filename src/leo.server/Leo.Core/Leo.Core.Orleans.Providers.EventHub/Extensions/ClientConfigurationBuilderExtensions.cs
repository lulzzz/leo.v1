using Leo.Core.Orleans.Client;

namespace Leo.Core.Orleans.Providers.EventHub
{
    public static class ClientConfigurationBuilderExtensions
    {
        public static ClientConfigurationBuilder WithEventHubStream(this ClientConfigurationBuilder builder, string providerName, string path, string hubConnectionString, string checkpointConnectionString, string checkpointNamespace, string checkpointTableName = "OrleansEventHubCheckpoint", string consumerGroup = "$Default", int checkpointIntervalSeconds = 5)
        {
            return new EventHubClientConfiguration(builder, providerName, path, hubConnectionString, checkpointConnectionString, checkpointNamespace, checkpointTableName, consumerGroup, checkpointIntervalSeconds);
        }
    }
}