using System;

namespace Leo.Core.Orleans
{
    public interface IEvent
    {
        DateTime Timestamp { get; }
        string StreamProviderName { get; }
        Guid StreamId { get; }
        string StreamNamespace { get; }
    }
}