using Leo.Core.Orleans.EventSourcing;
using MongoDB.Driver;
using Ninject.Modules;

namespace Leo.Core.Orleans.Providers.Mongo
{
    public class MongoEventsProviderModule : NinjectModule
    {
        private readonly string _collection;
        private readonly string _database;
        private readonly string _url;

        public MongoEventsProviderModule(string url, string database, string collection)
        {
            _url = url;
            _database = database;
            _collection = collection;
        }

        public override void Load()
        {
            Bind<IMongoCollection<EventDocument>>().ToMethod(ctx =>
            {
                var client = new MongoClient(_url);
                var db = client.GetDatabase(_database);
                return db.GetCollection<EventDocument>(_collection);
            });
            Bind<IEventsProvider>().To<MongoEventsProvider>();
        }
    }
}