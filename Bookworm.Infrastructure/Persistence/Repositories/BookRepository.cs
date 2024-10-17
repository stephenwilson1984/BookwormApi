using Bookworm.Application.Common.Interfaces;
using Bookworm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Infrastructure.Persistence.Repositories;

public class BookRepository(IBookwormContext dbContext) : BaseRepository<Book>(dbContext), IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Books
            .Include(book => book.Author)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId, CancellationToken cancellationToken)
    {
        return await DbContext.Books
            .Include(book => book.Author)
            .Where(book => book.AuthorId == authorId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetBookAsync(int id, CancellationToken cancellationToken)
    {
        return await DbContext.Books
            .Include(book => book.Author)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}