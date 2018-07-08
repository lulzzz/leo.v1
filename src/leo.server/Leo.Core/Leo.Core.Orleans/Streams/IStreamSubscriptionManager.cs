using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace Leo.Core.Orleans.Streams
{
    public interface IStreamSubscriptionManager
    {
        Task SubscribeAsync(IAsyncObserver<IEvent> observer, Guid streamId, string streamNamespace, string streamProviderName);

        Task UnsubscribeAsync(Guid streamId, string streamNamespace = null, string streamProviderName = null);
    }
}