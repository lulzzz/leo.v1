using Leo.Actors.Interfaces;
using Leo.Core.Id.Bson;
using Leo.Core.Owin;
using Leo.Core.Owin.Cors;
using Leo.Core.Owin.OAuth.Google;
using Leo.Core.Owin.ServiceFabric;
using Leo.Core.Owin.SignalR;
using Leo.Core.Security;
using Leo.Core.Security.SignalR;
using Leo.Hubs.Users;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.ServiceFabric.Services.Runtime;
using Ninject;
using System.Fabric;
using System.Threading;

namespace Leo.Hubs
{
    internal static class Program
    {
        private static void Main()
        {
            ServiceRuntime.RegisterServiceAsync("hubs", ServiceFactory)
                .GetAwaiter()
                .GetResult();

            Thread.Sleep(Timeout.Infinite);
        }

        private static StatelessService ServiceFactory(StatelessServiceContext context)
        {
            var kernel = new StandardKernel()
                .WithBsonIds()
                .WithActorsClient();

            IDependencyResolver resolver = new NinjectDependencyResolver(kernel);
            kernel.Bind<UsersStreamObserver>().ToSelf().InSingletonScope();
            kernel.Bind<IHubConnectionContext<IUserClient>>()
                .ToMethod(ctx =>
                    resolver.Resolve<IConnectionManager>()
                        .GetHubContext<UserHub, IUserClient>().Clients
                )
                .WhenInjectedInto<UsersStreamObserver>();

            AppBuilder appBuilder = new DefaultAppBuilder();
            appBuilder = new GoogleOAuth2AppDecorator(appBuilder, kernel.Get<IUserManager>());
            appBuilder = new CorsAppDecorator(appBuilder);
            appBuilder = new SignalRAppDecorator(appBuilder, resolver, kernel, new AuthorizedRequestModule());

            return new OwinStatelessService(context, appBuilder, "public");
        }
    }
}