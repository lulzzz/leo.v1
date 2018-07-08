using Ninject;
using System;

namespace Leo.Core.Orleans.ServiceFabric
{
    public class NinjectServiceProvider : IServiceProvider
    {
        protected readonly IKernel _kernel;

        public NinjectServiceProvider(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
    }
}