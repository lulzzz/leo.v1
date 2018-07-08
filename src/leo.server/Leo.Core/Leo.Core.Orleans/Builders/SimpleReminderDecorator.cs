using Orleans.Runtime.Configuration;
using Orleans.Streams;
using System;

namespace Leo.Core.Orleans
{
    public class SimpleReminderDecorator : ClusterDecorator
    {
        public SimpleReminderDecorator(ClusterBuilder builder)
            : base(builder)
        {
        }

        public override ClusterConfiguration Build()
        {
            var rvalue = _builder.Build();

            rvalue.Globals.ReminderServiceType = GlobalConfiguration.ReminderServiceProviderType.ReminderTableGrain;

            return rvalue;
        }
    }
}