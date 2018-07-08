using Orleans.EventSourcing;
using Orleans.EventSourcing.CustomStorage;
using Orleans.Streams;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Leo.Core.Id;

namespace Leo.Core.Orleans.EventSourcing
{
    public abstract class EventSourcedGrain<TState> : JournaledGrain<TState, IEvent>, ICustomStorageInterface<TState, IEvent>
        where TState : AggregateRoot, new()
    {
        private IEventsProvider _events;
        private IdProvider _ids;

        public override Task OnActivateAsync()
        {
            _events = ServiceProvider.GetService(typeof(IEventsProvider)) as IEventsProvider;
            _ids = ServiceProvider.GetService(typeof(IdProvider)) as IdProvider;
            return base.OnActivateAsync();
        }

        public async Task<KeyValuePair<int, TState>> ReadStateFromStorage()
        {
            var state = State ?? new TState();
            var version = Version;
            var events = await _events.ReadAsync(IdentityString, version + 1);
            if (events != null && events.Any())
            {
                version = events.Max(e => e.Version);                
                state.Replay(events.Select(e => e.Args));
            }

            return new KeyValuePair<int, TState>(version, state);
        }

        public async Task<bool> ApplyUpdatesToStorage(IReadOnlyList<IEvent> updates, int expectedversion)
        {
            List<PersistedEvent> newEvents = new List<PersistedEvent>();
            int version = Version;
            foreach (var update in updates)
            {
                var ts = DateTime.UtcNow;
                version++;
                var newEvent = new PersistedEvent(_ids.Create(update.Timestamp), IdentityString, update, version, update.Timestamp);
                newEvents.Add(newEvent);
            }

            await _events.WriteAsync(IdentityString, newEvents);

            var streams = updates
                    .GroupBy(e => new { e.StreamProviderName, e.StreamId, e.StreamNamespace })
                    .ToDictionary(
                        e => string.Join(":", e.Key.StreamProviderName, e.Key.StreamId, e.Key.StreamNamespace),
                        e => GetStreamProvider(e.Key.StreamProviderName).GetStream<IEvent>(e.Key.StreamId, e.Key.StreamNamespace)
                    );

            foreach (var update in updates)
            {
                var streamKey = string.Join(":", update.StreamProviderName, update.StreamId, update.StreamNamespace);
                await streams[streamKey].OnNextAsync(update);
            }

            return true;
        }
    }
}