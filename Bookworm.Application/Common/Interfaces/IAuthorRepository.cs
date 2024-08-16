using Bookworm.Domain.Entities;

namespace Bookworm.Application.Common.Interfaces;

public interface IAuthorRepository
{
    public Task<IEnumerable<Author>> GetAllAuthorsAsync(CancellationToken cancellationToken);

    public Task<Author?> GetAuthorAsync(int id, CancellationToken cancellationToken);
}