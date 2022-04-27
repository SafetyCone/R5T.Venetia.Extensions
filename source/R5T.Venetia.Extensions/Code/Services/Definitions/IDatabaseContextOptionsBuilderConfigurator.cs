using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using R5T.T0064;


namespace R5T.Venetia
{
    [ServiceDefinitionMarker]
    public interface IDatabaseContextOptionsBuilderConfigurator : IServiceDefinition
    {
        void ConfigureDatabaseContextOptionsBuilder(DbContextOptionsBuilder dbContextOptionsBuilder, SqlServerDbContextOptionsBuilder sqlServerDbContextOptionsBuilder);
    }
}
