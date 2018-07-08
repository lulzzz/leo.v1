using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leo.Core.Orleans.Streams
{
    public class StreamSubscriptionManager : IStreamSubscriptionManager
    {
        private readonly IClusterClient _cluster;
        private readonly List<StreamSubscriptionHandle<IEvent>> _subscriptions = new List<StreamSubscriptionHandle<IEvent>>();

        public StreamSubscriptionManager(IClusterClient cluster)
        {
            _cluster = cluster;
        }

        public async Task SubscribeAsync(IAsyncObserver<IEvent> observer, Guid streamId, string streamNamespace, string streamProviderName)
        {
            if (!_cluster.IsInitialized)
                await _cluster.Connect();

            var sub = _subscriptions.FirstOrDefault(s => s.ProviderName == streamProviderName && s.StreamIdentity?.Guid == streamId && s.StreamIdentity?.Namespace == streamNamespace);
            if (sub == null)
            {
                var subscription = await _cluster.GetStreamProvider(streamProviderName)
                    .GetStream<IEvent>(streamId, streamNamespace)
                    .SubscribeAsync(observer);
                _subscriptions.Add(subscription);
            }
        }

        public async Task UnsubscribeAsync(Guid streamId, string streamNamespace = null, string streamProviderName = null)
        {
            if (!_cluster.IsInitialized)
                await _cluster.Connect();

            var subs = _subscriptions.Where(s =>
                s.StreamIdentity?.Guid == streamId &&
                (string.IsNullOrEmpty(streamNamespace) || s.StreamIdentity?.Namespace == streamNamespace) &&
                (string.IsNullOrEmpty(streamProviderName) || s.ProviderName == streamProviderName)
            );

            if (subs.Any())
            {
                await Task.WhenAll(subs.Select(s => s.UnsubscribeAsync()));
                subs.Select(s => _subscriptions.Remove(s));
            }
        }
    }
}