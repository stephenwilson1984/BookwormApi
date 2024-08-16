using System.Data.Common;
using Bookworm.Application.Common.Interfaces;
using Bookworm.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookworm.Application.Authors.Queries.GetAllAuthors;

public record GetAllAuthorsQuery : IRequest<Result<GetAllAuthorsResponse>>;

public record GetAllAuthorsResponse(IEnumerable<AuthorDto> Authors);

public class GetAllAuthorsHandler(IAuthorRepository authorsRepository, ILogger<GetAllAuthorsHandler> logger) : IRequestHandler<GetAllAuthorsQuery, Result<GetAllAuthorsResponse>>
{
    public async Task<Result<GetAllAuthorsResponse>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(authorsRepository);

        try
        {
            var authors = await authorsRepository.GetAllAuthorsAsync(cancellationToken);

            var authorsDtos = authors.Select(author => new AuthorDto
            {
                Id = author.Id,
                Forename = author.Forename,
                Surname = author.Surname
            });

            return new SuccessResult<GetAllAuthorsResponse>(new GetAllAuthorsResponse(authorsDtos));
        }
        catch (DbException e)
        {
            logger.LogError(e, "An exception occurred when retrieving all authors.  Error message: '{ErrorMessage}'.", e.Message);
            return new DatabaseErrorResult<GetAllAuthorsResponse>(e);
        }
    }
}