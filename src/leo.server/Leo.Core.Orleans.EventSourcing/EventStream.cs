using Leo.Core.Id;
using System;

namespace Leo.Core.Orleans.EventSourcing
{
    public class EventStream
    {
        public EventStream(string providerName, string id, string @namespace)
            :this(providerName, id.ToGuid(), @namespace)
        {
        }

        public EventStream(string providerName, Guid id, string @namespace)
        {
            ProviderName = providerName;
            Id = id;
            Namespace = @namespace;
        }

        public string ProviderName { get; }
        public Guid Id { get; }
        public string Namespace { get; }
    }
}