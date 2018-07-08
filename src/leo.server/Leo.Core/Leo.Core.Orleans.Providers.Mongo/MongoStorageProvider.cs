using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Storage;
using System;
using System.Threading.Tasks;

namespace Leo.Core.Orleans.Providers.Mongo
{
    public class MongoStorageProvider : IStorageProvider
    {
        private IMongoDatabase _db;

        public Logger Log { get; set; }

        public string Name { get; set; }

        public async Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            string collectionName = grainType.Replace(".", "").ToLower();            
            var collection = _db.GetCollection<GrainStateDocument>(collectionName);

            await collection.DeleteOneAsync(gs => gs.Id == grainReference.ToString()).ConfigureAwait(false);
        }

        public Task Close()
        {
            return Task.CompletedTask;
        }

        public Task Init(string name, IProviderRuntime providerRuntime, IProviderConfiguration config)
        {
            var client = new MongoClient(config.Properties[Properties.Url]);
            _db = client.GetDatabase(config.Properties[Properties.Database]);

            return Task.CompletedTask;
        }

        public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            string collectionName = grainType.Replace(".", "").ToLower();
            var collection = _db.GetCollection<GrainStateDocument>(collectionName);

            var doc = await collection.Find(gs => gs.Id == grainReference.ToString()).FirstOrDefaultAsync();
            if (doc != null)
            {
                if (doc.State != null)
                    grainState.State = BsonSerializer.Deserialize(doc.State, grainState.State.GetType());
                grainState.ETag = doc.ETag;
            }
        }

        public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            try
            {
                string collectionName = grainType.Replace(".", "").ToLower();
                var collection = _db.GetCollection<GrainStateDocument>(collectionName);

                UpdateDefinition<GrainStateDocument> update = Builders<GrainStateDocument>.Update
                    .Set(gs => gs.State, grainState.State.ToBsonDocument(grainState.State.GetType()))
                    .Set(gs => gs.ETag, grainState.ETag);
                UpdateOptions opts = new UpdateOptions { IsUpsert = true };
                await collection.UpdateOneAsync(gs => gs.Id == grainReference.ToString(), update, opts).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static class Properties
        {
            public const string Database = "database";
            public const string Url = "url";
        }
    }
}