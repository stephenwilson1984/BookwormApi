using Bookworm.Application.Books.Queries.GetAllBooks;
using Bookworm.Application.Books.Queries.GetBook;
using Bookworm.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookworm.Api.Controllers;

[ApiController]
[Route("api/authors/{authorId}/books")]
public class BooksController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBooksAsync()
    {
        var booksResult = await mediator.Send(new GetAllBooksQuery());
        return ActionResult<Result<GetAllBooksResponse>>(booksResult);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookAsync(int id)
    {
        var bookResult = await mediator.Send(new GetBookQuery(id));
        return ActionResult<Result<GetBookResponse>>(bookResult);
    }
}