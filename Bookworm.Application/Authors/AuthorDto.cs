using System.Globalization;

namespace Bookworm.Application.Authors;

public class AuthorDto
{
    public int Id { get; set; }

    public string Forename { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;
}