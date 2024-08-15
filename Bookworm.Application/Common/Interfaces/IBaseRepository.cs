using Bookworm.Domain.Common;

namespace Bookworm.Application.Common.Interfaces;

public interface IBaseRepository<in T> where T : BaseEntity
{
    Task<bool> ExistsAsync(int id);
    Task AddAsync(T newEntity);
    Task UpdateAsync(T entityToUpdate);
    Task DeleteAsync(T entityToDelete);
}