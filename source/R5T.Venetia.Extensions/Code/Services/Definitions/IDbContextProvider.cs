using System;

using Microsoft.EntityFrameworkCore;


namespace R5T.Venetia
{
    public interface IDbContextProvider<TDbContext>
        where TDbContext: DbContext
    {
        TDbContext GetDbContext(DbContextOptions<TDbContext> dbContextOptions);
    }
}
