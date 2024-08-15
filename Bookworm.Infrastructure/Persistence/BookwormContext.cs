using Bookworm.Application.Common.Interfaces;
using Bookworm.Domain.Entities;
using Bookworm.Infrastructure.Common;
using Bookworm.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Infrastructure.Persistence;

public class BookwormContext(DbContextOptions<BookwormContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor, IMediator mediator)
    : DbContext(options), IBookwormContext
{
    public DbSet<Book> Books => Set<Book>();

    public DbSet<Author> Authors => Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}