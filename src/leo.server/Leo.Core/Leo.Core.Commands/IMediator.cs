using System.Threading;
using System.Threading.Tasks;

namespace Leo.Core.Commands
{
    public interface IMediator
    {
        Task<TResult> Query<TResult>(IQuery<TResult> query, CancellationToken token = default(CancellationToken));
    }
}