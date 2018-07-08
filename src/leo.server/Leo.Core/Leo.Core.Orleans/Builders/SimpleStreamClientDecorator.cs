using Leo.Core.Orleans.Client;
using Orleans.Runtime.Configuration;
using System;
using Orleans.Streams;

namespace Leo.Core.Orleans
{
    public class SimpleStreamClientDecorator : ClientConfigurationDecorator
    {
        private readonly bool _fireAndForget;
        private readonly string _providerName;

        public SimpleStreamClientDecorator(ClientConfigurationBuilder builder, string providerName, bool fireAndForget = false)
            : base(builder)
        {
            _providerName = providerName ?? throw new ArgumentNullException(nameof(providerName));
            _fireAndForget = fireAndForget;
        }

        public override ClientConfiguration Build()
        {
            var rvalue = _builder.Build();

            rvalue.AddSimpleMessageStreamProvider(_providerName, _fireAndForget, true, StreamPubSubType.ExplicitGrainBasedAndImplicit);
            
            return rvalue;
        }
    }
}