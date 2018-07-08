using Leo.Core.Ninject;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Ninject;

namespace Leo.Core.Orleans.Providers.Mongo
{
    public static class KernelExtensions
    {
        public static IKernel WithMongoEventsProvider(this IKernel kernel, string url, string database, string collection)
        {
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            return kernel.WithModule(new MongoEventsProviderModule(url, database, collection));
        }
    }
}