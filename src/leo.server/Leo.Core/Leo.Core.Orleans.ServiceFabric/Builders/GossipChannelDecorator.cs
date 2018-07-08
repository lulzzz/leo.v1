using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Runtime.Configuration;
using static Orleans.Runtime.Configuration.GlobalConfiguration;

namespace Leo.Core.Orleans.ServiceFabric
{
    public class GossipChannelDecorator : ClusterDecorator
    {
        private readonly GossipChannelType _channelType;
        private readonly string _connectionString;

        public GossipChannelDecorator(ClusterBuilder builder, GossipChannelType channelType, string connectionString) 
            : base(builder)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            _channelType = channelType;
            _connectionString = connectionString;
        }

        public override ClusterConfiguration Build()
        {
            var rvalue = _builder.Build();

            List<GossipChannelConfiguration> channels = new List<GossipChannelConfiguration>();
            if (rvalue.Globals.GossipChannels != null && rvalue.Globals.GossipChannels.Any())
                channels.AddRange(rvalue.Globals.GossipChannels);

            channels.Add(new GossipChannelConfiguration { ChannelType = _channelType, ConnectionString = _connectionString });

            rvalue.Globals.GossipChannels = channels;

            return rvalue;
        }
    }
}
