using Orleans.Runtime.Configuration;

namespace Leo.Core.Orleans.EventSourcing
{
    public class CustomLogDecorator : ClusterDecorator
    {
        private readonly string _primaryCluster;
        private readonly string _providerName;

        public CustomLogDecorator(ClusterBuilder builder, string providerName, string primaryCluster = null)
            : base(builder)
        {
            _providerName = providerName;
            _primaryCluster = primaryCluster;
        }

        public override ClusterConfiguration Build()
        {
            var rvalue = _builder.Build();
            rvalue.AddCustomStorageInterfaceBasedLogConsistencyProvider(_providerName, _primaryCluster);
            return rvalue;
        }
    }
}