using System.Data.Common;
using Bookworm.Application.Authors;
using Bookworm.Application.Common.Interfaces;
using Bookworm.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookworm.Application.Books.Queries.GetBook;

public record GetBookQuery(int BookId) : IRequest<Result<GetBookResponse>>;

public record GetBookResponse(BookDto Book);

public class GetBookQueryHandler(IBookRepository bookRepository, ILogger<GetBookQueryHandler> logger) : IRequestHandler<GetBookQuery, Result<GetBookResponse>>
{
    public async Task<Result<GetBookResponse>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(bookRepository);

        try
        {
            var book = await bookRepository.GetBookAsync(request.BookId, cancellationToken);

            if (book is null)
            {
                return new NotFoundErrorResult<GetBookResponse>($"Book with id {request.BookId} was not found.");
            }

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = new AuthorDto
                {
                    Id = book.Author.Id,
                    Forename = book.Author.Forename,
                    Surname = book.Author.Surname
                }
            };

            return new SuccessResult<GetBookResponse>(new GetBookResponse(bookDto));
        }
        catch (DbException e)
        {
            logger.LogError(e, "An exception occurred when retrieving a book.  Error message: '{ErrorMessage}'.", e.Message);
            return new DatabaseErrorResult<GetBookResponse>(e);
        }
    }
}