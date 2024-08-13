using Bookworm.Domain.Entities;

namespace Bookworm.Application.Common.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooks();
}