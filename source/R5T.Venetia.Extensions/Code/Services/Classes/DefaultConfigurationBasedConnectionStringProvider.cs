using System;

using Microsoft.Extensions.Configuration;

using R5T.Lincoln;


namespace R5T.Venetia
{
    /// <summary>
    /// A default implementation of a configuration-based connection string provider service.
    /// The service directly provides the value in the configuration at the <see cref="DefaultConfigurationBasedConnectionStringProvider.ConnectionStringConfigurationPath"/> location.
    /// </summary>
    public class DefaultConfigurationBasedConnectionStringProvider : IConnectionStringProvider
    {
        public const string ConnectionStringConfigurationPath = "DatabaseConfiguration:ConnectionString";


        private IConfiguration Configuration { get; }


        public DefaultConfigurationBasedConnectionStringProvider(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string GetConnectionString()
        {
            var connectionString = this.Configuration[DefaultConfigurationBasedConnectionStringProvider.ConnectionStringConfigurationPath];
            return connectionString;
        }
    }
}
