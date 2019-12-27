using System;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;

using R5T.Dacia.Extensions;
using R5T.Lincoln;


namespace R5T.Venetia.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <typeparamref name="TConnectionStringProvider"/> and <typeparamref name="TDatabaseContextOptionsBuilderConfigurator"/> types to the service collection, then uses them add the <typeparamref name="TDbContext"/> database context. 
        /// </summary>
        public static IServiceCollection AddDatabaseContextAddServices<TDbContext, TConnectionStringProvider, TDatabaseContextOptionsBuilderConfigurator>(this IServiceCollection services)
            where TDbContext: DbContext
            where TConnectionStringProvider: class, IConnectionStringProvider
            where TDatabaseContextOptionsBuilderConfigurator: class, IDatabaseContextOptionsBuilderConfigurator
        {
            services
                .TryAddSingletonFluent<TConnectionStringProvider>()
                .TryAddSingletonFluent<TDatabaseContextOptionsBuilderConfigurator>()
                ;

            services.AddDatabaseContextUseServices<TDbContext, TConnectionStringProvider, TDatabaseContextOptionsBuilderConfigurator>();

            return services;
        }

        /// <summary>
        /// Uses the <typeparamref name="TConnectionStringProvider"/> and <typeparamref name="TDatabaseContextOptionsBuilderConfigurator"/> types to add the <typeparamref name="TDbContext"/> database context. 
        /// </summary>
        public static IServiceCollection AddDatabaseContextUseServices<TDbContext, TConnectionStringProvider, TDatabaseContextOptionsBuilderConfigurator>(this IServiceCollection services)
            where TDbContext : DbContext
            where TConnectionStringProvider : class, IConnectionStringProvider
            where TDatabaseContextOptionsBuilderConfigurator : class, IDatabaseContextOptionsBuilderConfigurator
        {
            var intermediateServiceProvider = services.BuildServiceProvider();

            var connectionStringProvider = intermediateServiceProvider.GetRequiredService<TConnectionStringProvider>();

            var connectionString = connectionStringProvider.GetConnectionString();

            var databaseContextOptionsBuilderConfigurator = intermediateServiceProvider.GetRequiredService<TDatabaseContextOptionsBuilderConfigurator>();

            services.AddDbContext<TDbContext>(dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.UseSqlServer(connectionString, sqlServerDbContextOptionsBuilder =>
                {
                    databaseContextOptionsBuilderConfigurator.ConfigureDatabaseContextOptionsBuilder(dbContextOptionsBuilder, sqlServerDbContextOptionsBuilder);
                });
            });

            return services;
        }

        public static IServiceCollection AddDatabaseContext<TDbContext, TConnectionStringProvider, TDatabaseContextOptionsBuilderConfigurator>(this IServiceCollection services)
            where TDbContext : DbContext
            where TConnectionStringProvider : class, IConnectionStringProvider
            where TDatabaseContextOptionsBuilderConfigurator : class, IDatabaseContextOptionsBuilderConfigurator
        {
            services.AddDatabaseContextAddServices<TDbContext, TConnectionStringProvider, TDatabaseContextOptionsBuilderConfigurator>();

            return services;
        }

        public static IServiceCollection AddDatabaseContext<TDbContext, TConnectionStringProvider>(this IServiceCollection services)
            where TDbContext : DbContext
            where TConnectionStringProvider : class, IConnectionStringProvider
        {
            services.AddDatabaseContext<TDbContext, TConnectionStringProvider, DoNothingDatabaseContextOptionsBuilderConfigurator>();

            return services;
        }
    }
}
