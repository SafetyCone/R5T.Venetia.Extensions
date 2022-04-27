using System;

using Microsoft.EntityFrameworkCore;

using R5T.T0064;


namespace R5T.Venetia
{
    [ServiceDefinitionMarker]
    public interface IDbContextProvider<TDbContext> : IServiceDefinition
        where TDbContext: DbContext
    {
        TDbContext GetDbContext(DbContextOptions<TDbContext> dbContextOptions);
    }
}
