using Bookworm.Application.Common.Interfaces;
using Bookworm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Infrastructure.Persistence.Repositories;

public class AuthorRepository(IBookwormContext dbContext) : BaseRepository<Author>(dbContext), IAuthorRepository
{
    public async Task<IEnumerable<Author>> GetAllAuthorsAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Authors
            .Include(author => author.Books)
            .ToListAsync(cancellationToken);
    }

    public async Task<Author?> GetAuthorAsync(int id, CancellationToken cancellationToken)
    {
        return await DbContext.Authors
            .Include(author => author.Books)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}