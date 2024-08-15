using Bookworm.Application.Books.Queries.GetAllBooks;
using Bookworm.Application.Books.Queries.GetBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookworm.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBooksAsync()
    {
        var booksResult = await mediator.Send(new GetAllBooksQuery());
        return ActionResult(booksResult);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookAsync(int id)
    {
        var bookResult = await mediator.Send(new GetBookQuery(id));
        return ActionResult(bookResult);
    }
}