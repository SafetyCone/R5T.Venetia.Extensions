using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace R5T.Venetia
{
    public class DoNothingDatabaseContextOptionsBuilderConfigurator : IDatabaseContextOptionsBuilderConfigurator
    {
        public void ConfigureDatabaseContextOptionsBuilder(DbContextOptionsBuilder dbContextOptionsBuilder, SqlServerDbContextOptionsBuilder sqlServerDbContextOptionsBuilder)
        {
            // Do nothing.
        }
    }
}
