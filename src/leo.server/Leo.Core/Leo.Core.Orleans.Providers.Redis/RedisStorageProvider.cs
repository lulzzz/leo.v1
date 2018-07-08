using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Storage;
using ServiceStack.Redis;
using System.Threading.Tasks;

namespace Leo.Core.Orleans.Providers.Redis
{
    public class RedisStorageProvider : IStorageProvider
    {
        private BasicRedisClientManager _clients;

        public Logger Log { get; set; }

        public string Name { get; set; }

        public Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var key = grainReference.ToKeyString();

            using (var client = _clients.GetClient())
            {
                var value = client.Remove(key);
            }

            return Task.CompletedTask;
        }

        public Task Close()
        {
            _clients.Dispose();

            return Task.CompletedTask;
        }

        public Task Init(string name, IProviderRuntime providerRuntime, IProviderConfiguration config)
        {
            _clients = new BasicRedisClientManager();

            return Task.CompletedTask;
        }

        public Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var key = grainReference.ToKeyString();

            using (var client = _clients.GetClient())
            {
                var value = client.Get<IGrainState>(key);
                if (value != null)
                    grainState = value;
            }

            return Task.CompletedTask;
        }

        public Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var key = grainReference.ToKeyString();

            using (var client = _clients.GetClient())
            {
                client.Set(key, grainState);
            }

            return Task.CompletedTask;
        }
    }
}