using Microsoft.Orleans.ServiceFabric;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Ninject;
using Orleans.Runtime.Configuration;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

namespace Leo.Core.Orleans.ServiceFabric
{
    public class OrleansStatelessService : StatelessService
    {
        protected readonly Func<ClusterConfiguration> _configBuilder;
        protected readonly IKernel _kernel;

        public OrleansStatelessService(StatelessServiceContext serviceContext, IKernel kernel, Func<ClusterConfiguration> configBuilder)
            : base(serviceContext)
        {
            _kernel = kernel;
            _configBuilder = configBuilder;
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            ClusterStartup.Service = this;
            ClusterStartup.Kernel = _kernel;
            var clusterConfig = _configBuilder();
            return new[] { new ServiceInstanceListener(context => new OrleansCommunicationListener(context, clusterConfig), "Orleans") };
        }

        protected override Task OnCloseAsync(CancellationToken cancellationToken)
        {
            if (ClusterStartup.Kernel != null)
                ClusterStartup.Kernel.Dispose();
            return base.OnCloseAsync(cancellationToken);
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}