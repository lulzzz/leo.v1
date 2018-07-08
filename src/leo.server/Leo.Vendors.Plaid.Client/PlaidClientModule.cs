using Leo.Core.Ninject;
using Ninject;
using Ninject.Modules;

namespace Leo.Vendors.Plaid.Client
{
    public static class KernelExtensions
    {
        public static IKernel WithPlaidClient(this IKernel kernel)
        {
            return kernel.WithModule<PlaidClientModule>();
        }
    }

    public class PlaidClientModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlaidClient>().To<PlaidClient>();
        }
    }
}