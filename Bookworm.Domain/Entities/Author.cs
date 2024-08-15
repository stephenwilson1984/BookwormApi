using Bookworm.Domain.Common;

namespace Bookworm.Domain.Entities;

public class Author : BaseAuditableEntity
{
    public required string Forename { get; set; }

    public required string Surname { get; set; }
}