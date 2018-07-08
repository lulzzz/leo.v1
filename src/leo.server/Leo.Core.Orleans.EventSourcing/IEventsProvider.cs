using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leo.Core.Orleans.EventSourcing
{
    public interface IEventsProvider
    {
        Task<IEnumerable<PersistedEvent>> ReadAsync(IEnumerable<EventStream> streams);

        Task<IEnumerable<PersistedEvent>> ReadAsync(string aggregateId, int fromVersion = -1);

        Task WriteAsync(string aggregateId, IEnumerable<PersistedEvent> events);
    }
}