using Orleans.Streams;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Leo.Core.Orleans.Streams
{
    public abstract class StreamObserver : IAsyncObserver<IEvent>
    {
        public virtual Task OnCompletedAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnErrorAsync(Exception ex)
        {
            return Task.CompletedTask;
        }

        public Task OnNextAsync(IEvent @event, StreamSequenceToken token = null)
        {
            bool hasApply = this.GetType().GetMethod("OnNextAsync",
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    new Type[] { @event.GetType() },
                    null) != null;

            if (hasApply)
            {
                dynamic a = this;
                dynamic e = @event;
                dynamic t = token;
                return a.OnNextAsync(e);
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}
