using System.Data.Common;
using Bookworm.Application.Authors;
using Bookworm.Application.Common.Interfaces;
using Bookworm.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookworm.Application.Books.Queries.GetBooksByAuthor;

public record GetBooksByAuthorQuery(int AuthorId) : IRequest<Result<GetBooksByAuthorResponse>>;

public record GetBooksByAuthorResponse(IEnumerable<BookDto> Books);

public class GetBooksByAuthorQueryHandler(IBookRepository bookRepository, ILogger<GetBooksByAuthorQueryHandler> logger) : IRequestHandler<GetBooksByAuthorQuery, Result<GetBooksByAuthorResponse>>
{
    public async Task<Result<GetBooksByAuthorResponse>> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(bookRepository);

        try
        {
            var books = await bookRepository.GetBooksByAuthorAsync(request.AuthorId, cancellationToken);
            var booksDtos = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = new AuthorDto
                {
                    Id = book.Author.Id,
                    Forename = book.Author.Forename,
                    Surname = book.Author.Surname
                }
            });

            return new SuccessResult<GetBooksByAuthorResponse>(new GetBooksByAuthorResponse(booksDtos));
        }
        catch (DbException e)
        {
            logger.LogError(e, "An exception occurred when retrieving all books for author {AuthorId}.  Error message: '{ErrorMessage}'.", request.AuthorId, e.Message);
            return new DatabaseErrorResult<GetBooksByAuthorResponse>(e);
        }
    }
}