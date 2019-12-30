using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace R5T.Venetia
{
    /// <summary>
    /// Base class for repositories that can create their own database context instance from DI-container provided <see cref="DbContextOptions{TContext}"/>.
    /// </summary>
    public abstract class ProvidedDatabaseRepositoryBase<TDbContext>
        where TDbContext : DbContext
    {
        protected DbContextOptions<TDbContext> DbContextOptions { get; }
        protected IDbContextProvider<TDbContext> DbContextProvider { get; }


        public ProvidedDatabaseRepositoryBase(DbContextOptions<TDbContext> dbContextOptions, IDbContextProvider<TDbContext> dbContextProvider)
        {
            this.DbContextOptions = dbContextOptions;
            this.DbContextProvider = dbContextProvider;
        }

        /// <summary>
        /// Basically just a wrapper for callings the <see cref="IDbContextProvider{TDbContext}"/>.
        /// </summary>
        public TDbContext GetNewDbContext()
        {
            var dbContext = this.DbContextProvider.GetDbContext(this.DbContextOptions);
            return dbContext;
        }

        protected void ExecuteInContext(Action<TDbContext> action)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                action(dbContext);
            }
        }

        protected async Task ExecuteInContextAsync(Func<TDbContext, Task> action)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                await action(dbContext);
            }
        }

        protected TOutput ExecuteInContext<TOutput>(Func<TDbContext, TOutput> function)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var output = function(dbContext);
                return output;
            }
        }
    }
}
