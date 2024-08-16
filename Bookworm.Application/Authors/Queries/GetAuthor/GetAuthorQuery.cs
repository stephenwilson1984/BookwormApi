using System.Data.Common;
using Bookworm.Application.Common.Interfaces;
using Bookworm.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookworm.Application.Authors.Queries.GetAuthor;

public record GetAuthorQuery(int Id) : IRequest<Result<GetAuthorResponse>>;

public record GetAuthorResponse(AuthorDto Author);

public class GetAuthorQueryHandler(IAuthorRepository authorRepository, ILogger<GetAuthorQueryHandler> logger) : IRequestHandler<GetAuthorQuery, Result<GetAuthorResponse>>
{
    public async Task<Result<GetAuthorResponse>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(authorRepository);

        try
        {
            var author = await authorRepository.GetAuthorAsync(request.Id, cancellationToken);

            if (author is null)
            {
                return new NotFoundErrorResult<GetAuthorResponse>($"Author with id {request.Id} was not found.");
            }

            var authorDto = new AuthorDto
            {
                Id = author.Id,
                Forename = author.Forename,
                Surname = author.Surname
            };

            return new SuccessResult<GetAuthorResponse>(new GetAuthorResponse(authorDto));
        }
        catch (DbException e)
        {
            logger.LogError(e, "An exception occurred when retrieving an author.  Error message: '{ErrorMessage}'.", e.Message);
            return new DatabaseErrorResult<GetAuthorResponse>(e);
        }
    }
}