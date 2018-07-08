using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leo.Core.Commands
{
    public abstract class QueryInvoker<TResult>
    {
        public abstract Task<TResult> Invoke(IQuery<TResult> query, HandlerFactory factory, CancellationToken token = default(CancellationToken));

        protected TQuery GetQueryHandler<TQuery>(HandlerFactory factory)
        {
            return (TQuery)factory(typeof(TQuery));
        }
    }

    public class QueryInvoker<TQuery, TResult> : QueryInvoker<TResult>
        where TQuery : IQuery<TResult>
    {
        public override async Task<TResult> Invoke(IQuery<TResult> query, HandlerFactory factory, CancellationToken token = default(CancellationToken))
        {
            TResult rvalue = default(TResult);
            var handler = GetQueryHandler<IHandleQuery<TQuery, TResult>>(factory);
            try
            {
                //log start
                
                rvalue = await handler.Execute((TQuery)query, token);
            }
            catch
            {
                //log fail
                if (query.AttemptsRemaining > 1)
                    query.AttemptsRemaining--;
                else
                {
                    throw;
                }                    
            }
            finally
            {
                //log stop
            }
            return rvalue;
        }
    }
}