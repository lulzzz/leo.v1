using AutoMapper;
using Leo.Actors.Interfaces;
using Leo.Api.Boards;
using Leo.Core.Id.Bson;
using Leo.Core.Owin;
using Leo.Core.Owin.Cors;
using Leo.Core.Owin.OAuth.Google;
using Leo.Core.Owin.ServiceFabric;
using Leo.Core.Owin.WebApi;
using Leo.Core.Security;
using Leo.Core.Security.WebApi;
using Microsoft.ServiceFabric.Services.Runtime;
using Ninject;
using System.Fabric;
using System.Threading;

namespace Leo.Api
{
    internal static class Program
    {
        private static void Main()
        {
            ServiceRuntime.RegisterServiceAsync("api", ServiceFactory)
                .GetAwaiter()
                .GetResult();

            Thread.Sleep(Timeout.Infinite);
        }

        private static StatelessService ServiceFactory(StatelessServiceContext context)
        {
            var kernel = new StandardKernel()
                .WithBsonIds()
                .WithActorsClient();

            kernel.Bind<IMapper>().ToMethod(ctx =>
                new MapperConfiguration(cfg =>
                {
                })
                .CreateMapper()
            );

            AppBuilder appBuilder = new DefaultAppBuilder();
            appBuilder = new GoogleOAuth2AppDecorator(appBuilder, kernel.Get<IUserManager>());
            appBuilder = new CorsAppDecorator(appBuilder);
            appBuilder = new WebApiAppDecorator(appBuilder, kernel, new AuthorizedRequestFilter(), new ClusterInitializationFilter(), kernel.Get<BoardAuthorizationFilter>());

            return new OwinStatelessService(context, appBuilder);
        }
    }
}