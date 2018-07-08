using Leo.Core.Orleans.EventSourcing;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leo.Core.Orleans.Providers.Mongo
{
    public class MongoEventsProvider : IEventsProvider
    {
        private readonly IMongoCollection<EventDocument> _events;
        private readonly ProjectionDefinition<EventDocument, PersistedEvent> _projection;

        public MongoEventsProvider(IMongoCollection<EventDocument> events)
        {
            _events = events ?? throw new ArgumentNullException(nameof(events));
            _projection = new ProjectionDefinitionBuilder<EventDocument>().Expression(doc =>
                new PersistedEvent(doc.Id, doc.AggregateId, (IEvent)BsonSerializer.Deserialize(doc.Args, Type.GetType(doc.ArgsType), null), doc.Version, doc.Timestamp)
            );
        }

        public async Task<IEnumerable<PersistedEvent>> ReadAsync(string aggregateId, int fromVersion = -1)
        {
            var filter = Builders<EventDocument>.Filter.And(
                Builders<EventDocument>.Filter.Eq(e => e.AggregateId, aggregateId),
                Builders<EventDocument>.Filter.Gte(e => e.Version, fromVersion)
                );

            var opts = new FindOptions<EventDocument, PersistedEvent>();
            opts.Projection = _projection;
            opts.Sort = Builders<EventDocument>.Sort.Ascending(e => e.Version);

            var docs = await _events.FindAsync(filter, opts);
            return await docs.ToListAsync();
        }

        public async Task<IEnumerable<PersistedEvent>> ReadAsync(IEnumerable<EventStream> streams)
        {
            var filter = Builders<EventDocument>.Filter.Or(
                streams.Select(s =>
                    Builders<EventDocument>.Filter.And(
                        Builders<EventDocument>.Filter.Eq(e => e.StreamProviderName, s.ProviderName),
                        Builders<EventDocument>.Filter.Eq(e => e.StreamId, s.Id),
                        Builders<EventDocument>.Filter.Eq(e => e.StreamNamespace, s.Namespace)
                    )
                )
            );
            var opts = new FindOptions<EventDocument, PersistedEvent>();
            opts.Projection = _projection;
            opts.Sort = Builders<EventDocument>.Sort.Ascending(e => e.Timestamp);

            var docs = await _events.FindAsync(filter, opts);
            return await docs.ToListAsync();
        }

        public async Task WriteAsync(string aggregateId, IEnumerable<PersistedEvent> events)
        {
            var docs = events.Select(e => new EventDocument
            {
                AggregateId = e.AggregateId,
                Args = e.Args.ToBsonDocument(e.Args.GetType(), args: new BsonSerializationArgs { }),
                ArgsType = e.Args.GetType().AssemblyQualifiedName,
                Id = e.Id,
                Timestamp = e.Timestamp,
                Version = e.Version,
                StreamId = e.Args.StreamId,
                StreamNamespace = e.Args.StreamNamespace,
                StreamProviderName = e.Args.StreamProviderName,
                EventName = e.Args.GetType().Name.ToLower()
            })
            .OrderBy(e => e.Version)
            .ToList();
            await _events.InsertManyAsync(docs, new InsertManyOptions { IsOrdered = true });
        }
    }
}