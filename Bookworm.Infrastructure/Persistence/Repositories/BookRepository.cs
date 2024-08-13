using Bookworm.Application.Common.Interfaces;
using Bookworm.Application.Common.Options;
using Bookworm.Domain.Entities;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace Bookworm.Infrastructure.Persistence.Repositories;

public class BookRepository(IOptions<ConnectionStrings> options) : BaseRepository(options), IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        await using var connection = new SqliteConnection(ConnectionString);

        const string sql = """
                           SELECT Id, Title
                           FROM Books
                           """;

        var books = await connection.QueryAsync<Book>(sql);

        return books;
    }
}