using Orleans.Storage;
using System.Collections.Generic;

namespace Leo.Core.Orleans
{
    public static class ClusterBuilderExtensions
    {
        public static ClusterBuilder WithSimpleStorage(this ClusterBuilder builder, string providerName, int numberStorageGrains = MemoryStorage.NumStorageGrainsDefaultValue)
        {
            var config = new Dictionary<string, string> { { MemoryStorage.NumStorageGrainsPropertyName, numberStorageGrains.ToString() } };
            return new ClusterStorageDecorator<MemoryStorage>(builder, providerName, config);
        }

        public static ClusterBuilder WithSimpleStream(this ClusterBuilder builder, string providerName, bool fireAndForget = false)
        {
            return new SimpleStreamDecorator(builder, providerName, fireAndForget);
        }

        public static ClusterBuilder WithSimpleReminders(this ClusterBuilder builder)
        {
            return new SimpleReminderDecorator(builder);
        }
    }
}