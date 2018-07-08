using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Leo.Core.Orleans.Providers.Mongo
{
    internal class GrainStateDocument
    {
        [BsonElement("e")]
        public string ETag { get; set; }

        [BsonId]
        public string Id { get; set; }

        [BsonElement("s")]
        public BsonDocument State { get; set; }
    }
}