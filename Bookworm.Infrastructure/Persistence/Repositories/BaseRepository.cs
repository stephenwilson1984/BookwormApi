using Bookworm.Application.Common.Options;
using Microsoft.Extensions.Options;

namespace Bookworm.Infrastructure.Persistence.Repositories;

public class BaseRepository(IOptions<ConnectionStrings> options)
{
    protected readonly string ConnectionString = options.Value.DefaultConnection;
}