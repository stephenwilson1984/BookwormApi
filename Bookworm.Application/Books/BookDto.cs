using Bookworm.Application.Authors;

namespace Bookworm.Application.Books;

public class BookDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public AuthorDto Author { get; set; } = null!;
}