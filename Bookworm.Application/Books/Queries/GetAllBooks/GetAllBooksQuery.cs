using System.Data.Common;
using Bookworm.Application.Authors;
using Bookworm.Application.Common.Interfaces;
using Bookworm.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookworm.Application.Books.Queries.GetAllBooks;

public record GetAllBooksQuery : IRequest<Result<GetAllBooksResponse>>;

public record GetAllBooksResponse(List<BookDto> Books);

public class GetAllBooksQueryHandler(IBookRepository bookRepository, ILogger<GetAllBooksQueryHandler> logger) : IRequestHandler<GetAllBooksQuery, Result<GetAllBooksResponse>>
{
    public async Task<Result<GetAllBooksResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(bookRepository);

        try
        {
            var books = await bookRepository.GetAllBooksAsync(cancellationToken);
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
            }).ToList();

            return new SuccessResult<GetAllBooksResponse>(new GetAllBooksResponse(booksDtos));
        }
        catch (DbException e)
        {
            logger.LogError(e, "An exception occurred when retrieving all additives.  Error message: '{ErrorMessage}'.", e.Message);
            return new DatabaseErrorResult<GetAllBooksResponse>(e);
        }
    }
}