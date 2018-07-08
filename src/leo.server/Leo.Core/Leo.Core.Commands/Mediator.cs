using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leo.Core.Commands
{
    public class Mediator : IMediator
    {
        protected readonly HandlerFactory _factory;

        public Mediator(HandlerFactory factory)
        {
            _factory = factory;
        }

        public Task<TResult> Query<TResult>(IQuery<TResult> query, CancellationToken token = default(CancellationToken))
        {
            var invoker = (QueryInvoker<TResult>)Activator.CreateInstance(typeof(QueryInvoker<,>).MakeGenericType(query.GetType(), typeof(TResult)));
            return invoker.Invoke(query, _factory, token);
        }
    }
}