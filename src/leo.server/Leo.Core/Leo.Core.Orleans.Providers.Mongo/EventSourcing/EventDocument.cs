using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Leo.Core.Orleans.Providers.Mongo
{
    public class EventDocument
    {
        [BsonElement("n", Order = 10)]
        public string EventName { get; set; }

        [BsonElement("aid", Order = 20)]
        public string AggregateId { get; set; }

        [BsonElement("args", Order = 60)]
        public BsonDocument Args { get; set; }

        [BsonElement("argst", Order = 61)]
        public string ArgsType { get; set; }

        [BsonId]
        public string Id { get; set; }

        [BsonElement("sid", Order = 30)]
        public Guid StreamId { get; set; }

        [BsonElement("sns", Order = 31)]
        public string StreamNamespace { get; set; }

        [BsonElement("spn", Order = 32)]
        public string StreamProviderName { get; set; }

        [BsonElement("ts", Order = 11)]
        public DateTime Timestamp { get; set; }

        [BsonElement("v", Order = 12)]
        public int Version { get; set; }
    }
}