namespace Leo.Core.Orleans.EventSourcing
{
    public static class ClusterBuilderExtensions
    {
        public static ClusterBuilder WithCustomLog(this ClusterBuilder builder, string providerName, string primaryCluster = null)
        {
            return new CustomLogDecorator(builder, providerName, primaryCluster);
        }
    }
}