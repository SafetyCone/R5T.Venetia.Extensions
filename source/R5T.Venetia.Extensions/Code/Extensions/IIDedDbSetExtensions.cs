using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using R5T.Magyar;


namespace R5T.Venetia.Extensions
{
    public static class IIDedDbSetExtensions
    {
        public static async Task<int> GetIDByPredicateForSingleAsync<TEntity>(this DbSet<TEntity> set, Expression<Func<TEntity, bool>> predicateForSingle)
            where TEntity: class, IIDed
        {
            var ID = await set.Where(predicateForSingle).Select(x => x.ID).SingleAsync();
            return ID;
        }
    }
}
