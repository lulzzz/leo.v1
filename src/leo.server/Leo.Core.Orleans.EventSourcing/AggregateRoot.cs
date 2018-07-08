using System;
using System.Collections.Generic;
using System.Reflection;

namespace Leo.Core.Orleans.EventSourcing
{
    public abstract class AggregateRoot
    {
        public void Replay(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                bool hasApply = this.GetType().GetMethod("Apply",
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    new Type[] { @event.GetType() },
                    null) != null;

                if (hasApply)
                {
                    dynamic a = this;
                    dynamic e = @event;
                    a.Apply(e);
                }
            }
        }
    }
}