using Bookworm.Domain.Entities;

namespace Bookworm.Application.Common.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken);
    
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId, CancellationToken cancellationToken);

    Task<Book?> GetBookAsync(int id, CancellationToken cancellationToken);
}