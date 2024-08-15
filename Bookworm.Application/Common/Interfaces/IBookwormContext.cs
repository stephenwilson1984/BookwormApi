using Bookworm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Application.Common.Interfaces;

public interface IBookwormContext
{
    public DbSet<Book> Books { get; }

    public DbSet<Author> Authors { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}