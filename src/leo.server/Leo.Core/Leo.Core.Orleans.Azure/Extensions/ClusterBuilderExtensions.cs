namespace Leo.Core.Orleans.Azure
{
    public static class ClusterBuilderExtensions
    {
        public static ClusterBuilder WithAzureTableStorage(this ClusterBuilder builder, string providerName, string connectionString, string tableName = "OrleansGrainState", bool deleteOnClear = false, bool useJsonFormat = false, bool useFullAssemblyNames = false, bool indentJson = false)
        {
            return new AzureTableStorageDecorator(builder, providerName, connectionString, tableName, deleteOnClear, useJsonFormat, useFullAssemblyNames, indentJson);
        }
    }
}