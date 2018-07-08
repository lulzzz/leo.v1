using System;

namespace Leo.Core.Commands
{
    public interface IEvent
    {
        DateTime Timestamp { get; }
    }
}