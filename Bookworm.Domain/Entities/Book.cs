using Bookworm.Domain.Common;

namespace Bookworm.Domain.Entities;

public class Book : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
}