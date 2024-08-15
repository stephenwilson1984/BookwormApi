using Bookworm.Domain.Common;

namespace Bookworm.Domain.Entities;

public class Book : BaseAuditableEntity
{
    public required string Title { get; set; }

    public required Author Author { get; set; }
}