using Leo.Actors.Interfaces;
using Leo.Core.Id.Bson;
using Leo.Core.Orleans;
using Leo.Core.Orleans.Azure;
using Leo.Core.Orleans.EventSourcing;
using Leo.Core.Orleans.Providers.Mongo;
using Leo.Core.Orleans.ServiceFabric;
using Leo.Core.Settings.ServiceFabric;
using Leo.Vendors.Plaid.Client;
using Microsoft.ServiceFabric.Services.Runtime;
using Ninject;
using Orleans.Runtime.Configuration;
using System;
using System.Fabric;
using System.Threading;

namespace Leo.Actors
{
    internal static class Program
    {
        public const string ClusterId = "local";
        public const string DefaultConnectionString = "UseDevelopmentStorage=true";
        public static Guid ServiceId = new Guid("3691dcd9-7cac-4b51-8042-7138836a4d66");

        private static void Main()
        {
            ServiceRuntime.RegisterServiceAsync("actors", ServiceFactory).GetAwaiter().GetResult();

            Thread.Sleep(Timeout.Infinite);
        }

        private static StatelessService ServiceFactory(StatelessServiceContext context)
        {
            var kernel = new StandardKernel()
                .WithBsonIds()
                .WithMongoEventsProvider("mongodb://localhost:27017", "leo", "events")
                .WithServiceFabricSettings(context.CodePackageActivationContext)
                .WithPlaidClient()
                .WithActorsClient(context.ServiceName.ToString());

            var builder = new DefaultClusterBuilder(ClusterId, ServiceId, context.ServiceName, DefaultConnectionString)
                //.WithMongoStorage("PubSubStore", "mongodb://localhost:27017", "leo")
                .WithSimpleStorage("Default")
                .WithCustomLog("Default")
                .WithAzureTableStorage("PubSubStore", DefaultConnectionString)
                .WithGossipChannel(GlobalConfiguration.GossipChannelType.AzureTable, DefaultConnectionString)
                .WithMultiCluster(ClusterId)
                .WithSimpleStream(Streams.Providers.Events)
                .WithSimpleReminders();

            return new OrleansStatelessService(context, kernel, builder.Build);
        }
    }
}