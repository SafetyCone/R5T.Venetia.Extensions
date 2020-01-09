using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace R5T.Venetia.Extensions
{
    public static class DbSetExtensions
    {
        public static async Task<TEntity> AcquireSingleAsync<TEntity>(this DbSet<TEntity> set, Expression<Func<TEntity, bool>> predictateForSingle, Func<TEntity> constructor)
            where TEntity: class
        {
            var queryable = set.Where(predictateForSingle);

            var output = await queryable.HasSingleAsync();
            if(output.Exists)
            {
                return output.Result;
            }
            else
            {
                var newEntity = constructor();

                set.Add(newEntity);

                return newEntity;
            }
        }

        public static async Task<TEntity> AcquireSingleAsync<TEntity>(this DbSet<TEntity> set, Expression<Func<TEntity, bool>> predictateForSingle, Func<Task<TEntity>> constructorAsync)
            where TEntity : class
        {
            var queryable = set.Where(predictateForSingle);

            var output = await queryable.HasSingleAsync();
            if (output.Exists)
            {
                return output.Result;
            }
            else
            {
                var newEntity = await constructorAsync();

                set.Add(newEntity);

                return newEntity;
            }
        }
    }
}
