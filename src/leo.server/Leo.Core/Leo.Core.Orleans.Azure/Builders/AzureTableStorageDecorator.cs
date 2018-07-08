using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Runtime.Configuration;
using Orleans.AzureUtils;

namespace Leo.Core.Orleans.Azure
{
    public class AzureTableStorageDecorator : ClusterDecorator
    {
        private readonly string _providerName;
        private readonly string _connectionString;
        private readonly string _tableName;
        private readonly bool _deleteOnClear;
        private readonly bool _useJsonFormat;
        private readonly bool _useFullAssemblyNames;
        private readonly bool _indentJson;


        public AzureTableStorageDecorator(ClusterBuilder builder, string providerName, string connectionString, string tableName = "OrleansGrainState", bool deleteOnClear = false, bool useJsonFormat = false, bool useFullAssemblyNames = false, bool indentJson = false) 
            : base(builder)
        {
            _providerName = providerName;
            _connectionString = connectionString;
            _tableName = tableName;
            _deleteOnClear = deleteOnClear;
            _useJsonFormat = useJsonFormat;
            _useFullAssemblyNames = useFullAssemblyNames;
            _indentJson = indentJson;
        }

        public override ClusterConfiguration Build()
        {
            var rvalue = _builder.Build();

            rvalue.AddAzureTableStorageProvider(_providerName, _connectionString, _tableName, _deleteOnClear, _useJsonFormat, _useFullAssemblyNames, _indentJson);

            return rvalue;
        }
    }
}
