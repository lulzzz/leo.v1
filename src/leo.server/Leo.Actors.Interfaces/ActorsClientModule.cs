using Leo.Actors.Interfaces.Users;
using Leo.Core.Ninject;
using Leo.Core.Orleans.Client;
using Leo.Core.Security;
using Leo.Core.Orleans;
using Microsoft.Orleans.ServiceFabric;
using Ninject;
using Ninject.Modules;
using Orleans;
using Leo.Core.Orleans.Streams;

namespace Leo.Actors.Interfaces
{
    public static class KernelExtensions
    {
        public static IKernel WithActorsClient(this IKernel kernel, string serviceName = ActorsClientModule.ServiceName)
        {
            return kernel.WithModule(new ActorsClientModule(serviceName));
        }
    }

    public class ActorsClientModule : NinjectModule
    {
        public const string ServiceName = "fabric:/leo/actors";
        private readonly string _serviceName;

        public ActorsClientModule(string serviceName = ServiceName)
        {
            _serviceName = serviceName;
        }

        public override void Load()
        {
            Bind<IClusterClient>().ToMethod(ctx => {
                var client = new ClientBuilder()
                .AddServiceFabric(_serviceName)
                .UseConfiguration(
                    new DefaultClientConfiguration()
                    .WithSimpleStream(Streams.Providers.Events)
                        .Build()
                )
                .Build();
                return client;
            }).InSingletonScope();

            Bind<IStreamSubscriptionManager>().To<StreamSubscriptionManager>().InSingletonScope();

            Bind<IUserManager>().To<UserManager>();
        }
    }
}