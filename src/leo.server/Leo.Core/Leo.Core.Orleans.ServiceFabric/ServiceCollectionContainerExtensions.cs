using Microsoft.Extensions.DependencyInjection;
using Ninject;
using Ninject.Syntax;
using System;

namespace Leo.Core.Orleans.ServiceFabric
{
    public static class ServiceCollectionContainerExtensions
    {
        public static IServiceProvider BuildNinjectServiceProvider(this IServiceCollection services, IKernel kernel)
        {
            var rvalue = new NinjectServiceProvider(kernel);
            kernel.Bind<IServiceProvider>().ToConstant(rvalue);
            foreach (var service in services)
            {
                IBindingToSyntax<object> bind = kernel.Rebind(service.ServiceType);
                IBindingWhenInNamedWithOrOnSyntax<object> binding = null;
                if (service.ImplementationInstance != null)
                    binding = bind.ToConstant(service.ImplementationInstance);
                else if (service.ImplementationType != null)
                    binding = bind.To(service.ImplementationType);
                else
                    binding = bind.ToMethod(ctx => service.ImplementationFactory(ctx.Kernel.Get<IServiceProvider>()));

                switch (service.Lifetime)
                {
                    case ServiceLifetime.Scoped:
                        binding.InThreadScope();
                        break;

                    case ServiceLifetime.Singleton:
                        binding.InSingletonScope();
                        break;

                    case ServiceLifetime.Transient:
                        binding.InTransientScope();
                        break;
                }
            }

            return new NinjectServiceProvider(kernel);
        }
    }
}