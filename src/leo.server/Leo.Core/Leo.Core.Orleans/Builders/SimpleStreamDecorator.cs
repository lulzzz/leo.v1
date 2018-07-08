using Orleans.Runtime.Configuration;
using Orleans.Streams;
using System;

namespace Leo.Core.Orleans
{
    public class SimpleStreamDecorator : ClusterDecorator
    {
        private readonly bool _fireAndForget;
        private readonly string _providerName;

        public SimpleStreamDecorator(ClusterBuilder builder, string providerName, bool fireAndForget = false)
            : base(builder)
        {
            _providerName = providerName ?? throw new ArgumentNullException(nameof(providerName));
            _fireAndForget = fireAndForget;
        }

        public override ClusterConfiguration Build()
        {
            var rvalue = _builder.Build();

            rvalue.AddSimpleMessageStreamProvider(_providerName, _fireAndForget, true, StreamPubSubType.ExplicitGrainBasedAndImplicit);

            return rvalue;
        }
    }
}