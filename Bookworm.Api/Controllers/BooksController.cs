using Bookworm.Application.Books.Queries.GetBook;
using Bookworm.Application.Books.Queries.GetBooksByAuthor;
using Bookworm.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookworm.Api.Controllers;

[ApiController]
[Route("api/authors/{authorId:int}/books")]
public class BooksController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBooksAsync(int authorId)
    {
        var booksResult = await mediator.Send(new GetBooksByAuthorQuery(authorId));
        return ActionResult<Result<GetBooksByAuthorResponse>>(booksResult);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookAsync(int id)
    {
        var bookResult = await mediator.Send(new GetBookQuery(id));
        return ActionResult<Result<GetBookResponse>>(bookResult);
    }
}