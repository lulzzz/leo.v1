using System;

namespace Leo.Core.Orleans.EventSourcing
{
    public class PersistedEvent
    {
        public PersistedEvent(string id, string aggregateId, IEvent args, int version, DateTime timestamp)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrEmpty(aggregateId))
                throw new ArgumentNullException(nameof(aggregateId));

            Id = id;
            AggregateId = aggregateId;
            Args = args ?? throw new ArgumentNullException(nameof(args));
            Version = version;
            Timestamp = timestamp;
        }

        public string AggregateId { get; }
        public IEvent Args { get; }
        public string Id { get; }
        public DateTime Timestamp { get; }
        public int Version { get; }
    }
}