using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leo.Core.Commands
{
    public interface IHandleQuery<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Execute(TQuery query, CancellationToken cancellationToken = default(CancellationToken));
    }
}