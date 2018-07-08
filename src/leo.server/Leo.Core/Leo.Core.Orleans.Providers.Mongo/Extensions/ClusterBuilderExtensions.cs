using Leo.Core.Orleans.Providers.Mongo;
using System;
using System.Collections.Generic;

namespace Leo.Core.Orleans.Providers
{
    public static class ClusterBuilderExtensions
    {
        public static ClusterBuilder WithMongoStorage(this ClusterBuilder builder, string providerName, string url, string databaseName)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(databaseName)) throw new ArgumentNullException(nameof(databaseName));

            IDictionary<string, string> config = new Dictionary<string, string>
            {
                { MongoStorageProvider.Properties.Url, url },
                { MongoStorageProvider.Properties.Database, databaseName }
            };

            return new ClusterStorageDecorator<MongoStorageProvider>(builder, providerName, config);
        }
    }
}