using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using R5T.Magyar;


namespace R5T.Venetia.Extensions
{
    public static class IGUIDedDbSetExtensions
    {
        public static async Task<TEntity> SelectSingleByGuidAsync<TEntity>(this DbSet<TEntity> set, Guid guid)
            where TEntity: class, IGUIDed
        {
            var entity = await set.Where(x => x.GUID == guid).SingleAsync();
            return entity;
        }

        public static async Task<int> GetIDByGuidAsync<TEntity>(this DbSet<TEntity> set, Guid guid)
            where TEntity : class, IGUIDed, IIDed
        {
            var entityID = await set.Where(x => x.GUID == guid).Select(x => x.ID).SingleAsync();
            return entityID;
        }
    }
}
