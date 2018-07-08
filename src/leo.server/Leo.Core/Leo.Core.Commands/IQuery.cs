using System;

namespace Leo.Core.Commands
{
    public interface IQuery<TResult>
    {
        int AttemptsRemaining { get; set; }

        Guid? TransactionId { get; }
    }
}