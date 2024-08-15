using Bookworm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bookworm.Infrastructure.Persistence;

public class BookwormDbContextInitialiser(ILogger<BookwormDbContextInitialiser> logger, BookwormContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            if (context.Database.IsSqlite())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data. Seed, if necessary
        if (!await context.Books.AnyAsync())
        {
            await context.Books.AddRangeAsync(new Book
            {
                Title = "The Old Man and the Sea",
                Author = new Author
                {
                    Forename = "Ernest",
                    Surname = "Hemingway"
                }
            }, new Book
            {
                Title = "The Great Gatsby",
                Author = new Author
                {
                    Forename = "F. Scott",
                    Surname = "Fitzgerald"
                }
            }, new Book
            {
                Title = "To Kill a Mockingbird",
                Author = new Author
                {
                    Forename = "Harper",
                    Surname = "Lee"
                }
            }, new Book
            {
                Title = "Of Mice and Men",
                Author = new Author
                {
                    Forename = "John",
                    Surname = "Steinbeck"
                }
            });

            await context.SaveChangesAsync();
        }
    }
}