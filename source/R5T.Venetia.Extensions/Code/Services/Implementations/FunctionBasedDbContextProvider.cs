using System;

using Microsoft.EntityFrameworkCore;

using R5T.T0064;


namespace R5T.Venetia
{
    [ServiceImplementationMarker]
    public class FunctionBasedDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>, IServiceImplementation
        where TDbContext : DbContext
    {
        private Func<DbContextOptions<TDbContext>, TDbContext> DbContextConstructor { get; }


        public FunctionBasedDbContextProvider(
            [NotServiceComponent] Func<DbContextOptions<TDbContext>, TDbContext> dbContextConstructor)
        {
            this.DbContextConstructor = dbContextConstructor;
        }

        public TDbContext GetDbContext(DbContextOptions<TDbContext> dbContextOptions)
        {
            var dbContext = this.DbContextConstructor(dbContextOptions);
            return dbContext;
        }
    }
}
