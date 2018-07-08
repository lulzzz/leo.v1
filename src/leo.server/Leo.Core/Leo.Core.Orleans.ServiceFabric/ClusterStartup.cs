using Microsoft.Extensions.DependencyInjection;
using Microsoft.Orleans.ServiceFabric;
using Microsoft.ServiceFabric.Services.Runtime;
using Ninject;
using System;

namespace Leo.Core.Orleans.ServiceFabric
{
    public class ClusterStartup
    {
        public static IKernel Kernel { get; set; }

        public static StatelessService Service { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddServiceFabricSupport(Service);
            return services.BuildNinjectServiceProvider(Kernel);
        }
    }
}