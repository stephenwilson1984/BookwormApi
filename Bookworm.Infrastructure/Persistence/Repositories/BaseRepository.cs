using Bookworm.Application.Common.Interfaces;
using Bookworm.Application.Common.Options;
using Bookworm.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bookworm.Infrastructure.Persistence.Repositories;

public abstract class BaseRepository<T>(IBookwormContext dbContext) : IBaseRepository<T> where T : BaseEntity
{
    protected readonly IBookwormContext DbContext = dbContext;

    public async Task<bool> ExistsAsync(int id)
    {
        return await DbContext.Set<T>().AnyAsync(x => x.Id == id);
    }

    public async Task AddAsync(T newEntity)
    {
        await DbContext.Set<T>().AddAsync(newEntity);
        await DbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entityToUpdate)
    {
        DbContext.Set<T>().Update(entityToUpdate);
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entityToDelete)
    {
        DbContext.Set<T>().Remove(entityToDelete);
        await DbContext.SaveChangesAsync();
    }
}