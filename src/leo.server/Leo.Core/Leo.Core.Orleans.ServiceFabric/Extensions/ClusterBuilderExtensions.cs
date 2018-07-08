using static Orleans.Runtime.Configuration.GlobalConfiguration;

namespace Leo.Core.Orleans.ServiceFabric
{
    public static class ClusterBuilderExtensions
    {
        public static ClusterBuilder WithMultiCluster(this ClusterBuilder builder, params string[] clusters)
        {
            return new MultiClusterDecorator(builder, clusters);
        }

        public static ClusterBuilder WithGossipChannel(this ClusterBuilder builder, GossipChannelType channelType, string connectionString)
        {
            return new GossipChannelDecorator(builder, channelType, connectionString);
        }
    }
}