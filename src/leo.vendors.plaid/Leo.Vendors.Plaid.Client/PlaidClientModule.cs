using Ninject.Modules;

namespace Leo.Vendors.Plaid.Client
{
    public class PlaidClientModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlaidClient>().To<PlaidClient>();
        }
    }
}