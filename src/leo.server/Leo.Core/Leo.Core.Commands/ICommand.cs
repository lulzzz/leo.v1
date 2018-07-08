using System;

namespace Leo.Core.Commands
{
    public interface ICommand
    {
        string CommandId { get; }

        string ParentCommandId { get; }

        short Retries { get; }

        TimeSpan RetryDelay { get; }
    }
}