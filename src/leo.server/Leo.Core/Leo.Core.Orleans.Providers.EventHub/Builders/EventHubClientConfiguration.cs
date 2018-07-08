using Leo.Core.Orleans.Client;
using Orleans.Runtime.Configuration;
using Orleans.ServiceBus.Providers;
using System;
using System.Collections.Generic;

namespace Leo.Core.Orleans.Providers.EventHub
{
    public class EventHubClientConfiguration : ClientConfigurationDecorator
    {
        private readonly string _checkpointConnectionString;
        private readonly TimeSpan _checkpointInterval;
        private readonly string _checkpointNamespace;
        private readonly string _checkpointTableName;
        private readonly string _consumerGroup;
        private readonly string _hubConnectionString;
        private readonly string _path;
        private readonly string _providerName;

        public EventHubClientConfiguration(ClientConfigurationBuilder builder, string providerName, string path, string hubConnectionString, string checkpointConnectionString, string checkpointNamespace, string checkpointTableName = "OrleansEventHubCheckpoint", string consumerGroup = "$Default", int checkpointIntervalSeconds = 5)
            : base(builder)
        {
            _providerName = providerName;
            _path = path;
            _hubConnectionString = hubConnectionString;
            _checkpointConnectionString = checkpointConnectionString;
            _checkpointTableName = checkpointTableName;
            _checkpointNamespace = checkpointNamespace;
            _consumerGroup = consumerGroup;
            _checkpointInterval = TimeSpan.FromSeconds(checkpointIntervalSeconds);
        }

        public override ClientConfiguration Build()
        {
            var rvalue = _builder.Build();

            var settings = new Dictionary<string, string>();

            EventHubStreamProviderSettings providerSettings = new EventHubStreamProviderSettings(_providerName);
            providerSettings.WriteProperties(settings);

            EventHubCheckpointerSettings checkpoint = new EventHubCheckpointerSettings(_checkpointConnectionString, _checkpointTableName, _checkpointNamespace, _checkpointInterval);
            checkpoint.WriteProperties(settings);

            EventHubSettings eventHub = new EventHubSettings(_hubConnectionString, _consumerGroup, _path);
            eventHub.WriteProperties(settings);

            rvalue.RegisterStreamProvider<EventHubStreamProvider>(_providerName, settings);

            return rvalue;
        }
    }
}