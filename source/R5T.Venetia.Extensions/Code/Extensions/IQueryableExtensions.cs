using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using R5T.Magyar;


namespace R5T.Venetia.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<HasOutput<TEntity>> HasSingleAsync<TEntity>(this IQueryable<TEntity> queryable)
            where TEntity: class
        {
            var singleOrDefault = await queryable.SingleOrDefaultAsync();

            var hasOutput = HasOutput.From(singleOrDefault);
            return hasOutput;
        }

        public static async Task<HasOutput<TEntity>> HasSingleAsync<TEntity>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, bool>> predicateForSingle)
            where TEntity: class
        {
            var singleOrDefault = await queryable.Where(predicateForSingle).SingleOrDefaultAsync();

            var hasOutput = HasOutput.From(singleOrDefault);
            return hasOutput;
        }
    }
}
