using Bookworm.Application.Common.Interfaces;
using Bookworm.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookworm.Application.Books.Queries.GetAllBooks;

public record GetAllBooksQuery : IRequest<GetAllBooksResponse>;

public record GetAllBooksResponse(Result<List<BookDto>> Books);

public class GetAllBooksQueryHandler(IBookwormContext dbContext) : IRequestHandler<GetAllBooksQuery, GetAllBooksResponse>
{
    public async Task<GetAllBooksResponse> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        var books = await dbContext.Books.Select(book => new BookDto
        {
            Id = book.Id,
            Name = book.Name
        }).ToListAsync(cancellationToken);

        return new GetAllBooksResponse(Result<List<BookDto>>.Success(books));
    }
}