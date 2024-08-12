using Bookworm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Application.Common.Interfaces;

public interface IBookwormContext
{
    DbSet<Book> Books { get; }
}