using Orleans.Runtime.Configuration;

namespace Leo.Core.Orleans
{
    public abstract class ClusterBuilder
    {
        public abstract ClusterConfiguration Build();
    }
}