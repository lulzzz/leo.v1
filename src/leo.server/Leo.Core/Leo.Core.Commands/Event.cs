using System;
using System.Runtime.Serialization;

namespace Leo.Core.Commands
{
    [KnownType(typeof(Event))]
    [KnownType(typeof(IEvent))]
    [Serializable]
    public class Event : IEvent
    {
        public Event(DateTime? timestamp = null)
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; private set; }
    }
}