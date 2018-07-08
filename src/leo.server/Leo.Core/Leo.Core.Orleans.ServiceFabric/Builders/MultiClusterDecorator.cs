using Orleans.Runtime.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Leo.Core.Orleans.ServiceFabric
{
    public class MultiClusterDecorator : ClusterDecorator
    {
        private readonly List<string> _clusters;

        public MultiClusterDecorator(ClusterBuilder builder, params string[] clusters)
            : base(builder)
        {
            _clusters = clusters?.ToList() ?? new List<string>();
        }

        public override ClusterConfiguration Build()
        {
            var rvalue = _builder.Build();

            if (!_clusters.Contains(rvalue.Globals.ClusterId))
                _clusters.Add(rvalue.Globals.ClusterId);

            rvalue.Globals.DefaultMultiCluster = _clusters;

            return rvalue;
        }
    }
}