using Leo.Core.Id;
using System;

namespace Leo.Core.Orleans
{
    public abstract class Event : IEvent
    {
        public Event(string streamProviderName, string streamId, string streamNamespace, DateTime? timestamp = null)
            :this(streamProviderName, streamId.ToGuid(), streamNamespace, timestamp)
        {
        }

        public Event(string streamProviderName, Guid streamId, string streamNamespace, DateTime? timestamp = null)
        {
            StreamProviderName = streamProviderName;
            StreamId = streamId;
            StreamNamespace = streamNamespace;
            Timestamp = timestamp ?? DateTime.UtcNow;
        }

        public string StreamProviderName { get; private set; }
        public Guid StreamId { get; private set; }
        public string StreamNamespace { get; private set; }
        public DateTime Timestamp { get; private set; }
    }
}