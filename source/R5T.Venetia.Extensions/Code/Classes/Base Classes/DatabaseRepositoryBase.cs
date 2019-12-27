using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Venetia
{
    /// <summary>
    /// Base class for repositories that can create their own database context instance from DI-container provided <see cref="DbContextOptions{TContext}"/>.
    /// </summary>
    public abstract class DatabaseRepositoryBase<TDbContext>
        where TDbContext : DbContext
    {
        protected DbContextOptions<TDbContext> DbContextOptions { get; }


        public DatabaseRepositoryBase(DbContextOptions<TDbContext> dbContextOptions)
        {
            this.DbContextOptions = dbContextOptions;
        }

        /// <summary>
        /// Basically just a wrapper for callings the <typeparamref name="TDbContext"/> constructor that takes the <see cref="DbContextOptions{TContext}"/>.
        /// </summary>
        public abstract TDbContext GetNewDbContext();

        protected void ExecuteInContext(Action<TDbContext> action)
        {
            using (var dbContext = this.GetNewDbContext())
            {
                action(dbContext);
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
