using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace R5T.Venetia
{
    public static class DbContextExtensions
    {
        public static async Task ModifyEntitySingle<TDbContext, TEntity>(this TDbContext dbContext,
            Func<TDbContext, IQueryable<TEntity>> entitySelector,
            Expression<Func<TEntity, bool>> predicate,
            Action<TEntity> entityModifer)
            where TDbContext: DbContext
        {
            var entities = entitySelector(dbContext);

            var entity = await entities.Where(predicate).SingleAsync();

            entityModifer(entity);

            await dbContext.SaveChangesAsync();
        }
    }
}
