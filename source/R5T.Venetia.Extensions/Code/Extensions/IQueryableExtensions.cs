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
        public static async Task<WasFound<TResult>> WasFoundSingleOrDefault<TResult>(this IQueryable<TResult> queryable)
        {
            var singleOrDefault = await queryable.SingleOrDefaultAsync();

            var wasFound = Magyar.WasFound.From(singleOrDefault); // Needs qualification due to static method name.
            return wasFound;
        }

        /// <summary>
        /// Selects <see cref="WasFoundSingleOrDefault{TEntity}(IQueryable{TEntity})"/> as the default was found.
        /// </summary>
        public static Task<WasFound<TResult>> WasFound<TResult>(this IQueryable<TResult> queryable)
        {
            var gettingWasFound = queryable.WasFoundSingleOrDefault();
            return gettingWasFound;
        }

        /// <summary>
        /// Selects <see cref="WasFoundSingleOrDefault{TEntity}(IQueryable{TEntity})"/> as the default was found.
        /// </summary>
        public static Task<WasFound<TResult>> WasFoundForQueryable<TResult>(this IQueryable<TResult> queryable)
        {
            var gettingWasFound = queryable.WasFoundSingleOrDefault();
            return gettingWasFound;
        }

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

        public static Task<TProperty> GetPropertySingle<TEntity, TProperty>(this IQueryable<TEntity> entities,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TProperty>> propertySelector)
        {
            var gettingProperty = entities
                .Where(predicate)
                .Select(propertySelector)
                .SingleAsync();

            return gettingProperty;
        }
    }
}
