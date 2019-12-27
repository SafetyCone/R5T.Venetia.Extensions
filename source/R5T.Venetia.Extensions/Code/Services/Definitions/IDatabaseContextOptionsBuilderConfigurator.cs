using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace R5T.Venetia
{
    public interface IDatabaseContextOptionsBuilderConfigurator
    {
        void ConfigureDatabaseContextOptionsBuilder(DbContextOptionsBuilder dbContextOptionsBuilder, SqlServerDbContextOptionsBuilder sqlServerDbContextOptionsBuilder);
    }
}
