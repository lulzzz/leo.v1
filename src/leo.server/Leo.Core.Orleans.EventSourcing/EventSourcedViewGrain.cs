using Orleans.EventSourcing;
using Orleans.EventSourcing.CustomStorage;
using Orleans.Streams;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Leo.Core.Orleans.EventSourcing
{
    public abstract class EventSourcedViewGrain<TState> : JournaledGrain<TState, IEvent>, ICustomStorageInterface<TState, IEvent>
        where TState : AggregateRoot, new()
    {
        private IEventsProvider _events;

        protected abstract IEnumerable<EventStream> EventStreams { get; }

        public Task<bool> ApplyUpdatesToStorage(IReadOnlyList<IEvent> updates, int expectedversion)
        {
            return Task.FromResult(Version == expectedversion);
        }

        public override async Task OnActivateAsync()
        {
            _events = (IEventsProvider)ServiceProvider.GetService(typeof(IEventsProvider));

            foreach (var stream in EventStreams)
            {
                await GetStreamProvider(stream.ProviderName)
                    .GetStream<IEvent>(stream.Id, stream.Namespace)
                    .SubscribeAsync(OnNextAsync);
            }
        }

        public async Task<KeyValuePair<int, TState>> ReadStateFromStorage()
        {
            var state = State ?? new TState();
            var version = Version;
            var eventsToReplay = await _events.ReadAsync(EventStreams);
            if (eventsToReplay != null && eventsToReplay.Any())
            {
                version = eventsToReplay.Count();
                state.Replay(eventsToReplay.Select(e => e.Args));
            }

            return new KeyValuePair<int, TState>(version, state);
        }

        protected virtual Task OnNextAsync(IEvent @event, StreamSequenceToken token)
        {
            RaiseEvent(@event);
            return Task.CompletedTask;
        }
    }
}