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

        protected void ExecuteInContextSynchronous(Action<TDbContext> action)
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

        /// <summary>
        /// Default method is asynchronous.
        /// </summary>
        protected Task ExecuteInContext(Func<TDbContext, Task> action)
        {
            var execute = this.ExecuteInContextAsync(action);
            return execute;
        }

        protected TOutput ExecuteInContextSynchronous<TOutput>(Func<TDbContext, TOutput> function)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var output = function(dbContext);
                return output;
            }
        }

        protected async Task<TOutput> ExecuteInContextAsync<TOutput>(Func<TDbContext, Task<TOutput>> function)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                var output = await function(dbContext);
                return output;
            }
        }

        /// <summary>
        /// Default method is asynchronous.
        /// </summary>
        protected Task<TOutput> ExecuteInContext<TOutput>(Func<TDbContext, Task<TOutput>> function)
        {
            var execute = this.ExecuteInContextAsync(function);
            return execute;
        }
    }
}
