using Orleans.Runtime.Configuration;
using Orleans.Storage;
using System;
using System.Collections.Generic;

namespace Leo.Core.Orleans
{
    public class ClusterStorageDecorator<T> : ClusterDecorator
        where T : IStorageProvider
    {
        protected readonly IDictionary<string, string> _config;
        protected readonly string _providerName;

        public ClusterStorageDecorator(ClusterBuilder builder, string providerName, IDictionary<string, string> config = null)
            : base(builder)
        {
            _providerName = providerName ?? throw new ArgumentNullException(nameof(providerName));
            _config = config ?? new Dictionary<string, string>();
        }

        public override ClusterConfiguration Build()
        {
            var rvalue = _builder.Build();

            rvalue.Globals.RegisterStorageProvider<T>(_providerName, _config);

            return rvalue;
        }
    }
}