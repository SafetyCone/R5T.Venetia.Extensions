using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;using R5T.T0064;


namespace R5T.Venetia
{[ServiceImplementationMarker]
    public class DoNothingDatabaseContextOptionsBuilderConfigurator : IDatabaseContextOptionsBuilderConfigurator,IServiceImplementation
    {
        public void ConfigureDatabaseContextOptionsBuilder(DbContextOptionsBuilder dbContextOptionsBuilder, SqlServerDbContextOptionsBuilder sqlServerDbContextOptionsBuilder)
        {
            // Do nothing.
        }
    }
}
