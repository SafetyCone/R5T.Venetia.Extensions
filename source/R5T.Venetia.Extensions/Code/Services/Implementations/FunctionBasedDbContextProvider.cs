using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Venetia
{
    public class FunctionBasedDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        private Func<DbContextOptions<TDbContext>, TDbContext> DbContextConstructor { get; }


        public FunctionBasedDbContextProvider(Func<DbContextOptions<TDbContext>, TDbContext> dbContextConstructor)
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
