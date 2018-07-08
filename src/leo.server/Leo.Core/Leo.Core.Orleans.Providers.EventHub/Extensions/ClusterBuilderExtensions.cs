namespace Leo.Core.Orleans.Providers.EventHub
{
    public static class ClusterBuilderExtensions
    {
        public static ClusterBuilder WithEventHubStream(this ClusterBuilder builder, string providerName, string path, string hubConnectionString, string checkpointConnectionString, string checkpointNamespace, string checkpointTableName = "OrleansEventHubCheckpoint", string consumerGroup = "$Default", int checkpointIntervalSeconds = 5)
        {
            return new EventHubStreamDecorator(builder, providerName, path, hubConnectionString, checkpointConnectionString, checkpointNamespace, checkpointTableName, consumerGroup, checkpointIntervalSeconds);
        }
    }
}