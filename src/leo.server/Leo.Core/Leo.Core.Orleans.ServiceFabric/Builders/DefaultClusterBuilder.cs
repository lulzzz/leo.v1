using Orleans.Runtime.Configuration;
using System;
using System.IO;
using System.Linq;
using static Orleans.Runtime.Configuration.GlobalConfiguration;

namespace Leo.Core.Orleans.ServiceFabric
{
    public class DefaultClusterBuilder : ClusterBuilder
    {
        protected readonly string _clusterId;
        protected readonly string _configurationFileDir;
        protected readonly string _configurationFileName;
        protected readonly string _defaultConnectionString;
        protected readonly Guid _serviceId;
        protected readonly Uri _serviceName;

        public DefaultClusterBuilder(string clusterId, Guid serviceId, Uri serviceName, string defaultConnectionString,
            string configurationFileDir = null, string configurationFileName = "OrleansConfiguration.xml")
        {
            _clusterId = clusterId;
            _serviceId = serviceId;
            _serviceName = serviceName;
            _defaultConnectionString = defaultConnectionString;
            _configurationFileDir = configurationFileDir ?? Environment.CurrentDirectory;
            _configurationFileName = configurationFileName;
        }

        public override ClusterConfiguration Build()
        {
            var rvalue = new ClusterConfiguration();

            DirectoryInfo dir = new DirectoryInfo(_configurationFileDir);
            var file = dir.GetFiles(_configurationFileName)?.FirstOrDefault();
            if (file != null)
                rvalue.LoadFromFile(file.FullName);
            rvalue.UseStartupType<ClusterStartup>();
            rvalue.Globals.ServiceId = _serviceId;
            rvalue.Globals.DeploymentId = _serviceName.ToString().Replace("fabric:/", "").Replace('/', '-').Replace('.', '-');
            rvalue.Globals.ClusterId = _clusterId;
            rvalue.Globals.DataConnectionString = _defaultConnectionString;
            rvalue.Globals.ReminderServiceType = ReminderServiceProviderType.AzureTable;
            rvalue.Globals.DataConnectionStringForReminders = _defaultConnectionString;

            return rvalue;
        }
    }
}