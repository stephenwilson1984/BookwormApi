using Bookworm.Application.Common.Interfaces;
using Bookworm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Infrastructure.Data;

public class BookwormContext : DbContext, IBookwormContext
{
    public DbSet<Book> Books => Set<Book>();
}