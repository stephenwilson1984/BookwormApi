using Bookworm.Application.Books.Queries.GetAllBooks;
using Bookworm.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookworm.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var booksResult = await mediator.Send(new GetAllBooksQuery());

        return booksResult switch
        {
            SuccessResult<GetAllBooksResponse> successResult => new OkObjectResult(successResult.Data),
            NotFoundErrorResult<GetAllBooksResponse> => new NotFoundResult(),
            DatabaseErrorResult<GetAllBooksResponse> => new StatusCodeResult(500),
            ErrorResult<GetAllBooksResponse> => new StatusCodeResult(500),
            _ => new StatusCodeResult(500)
        };
    }
}