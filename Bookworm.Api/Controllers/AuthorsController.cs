using Bookworm.Application.Authors.Queries.GetAllAuthors;
using Bookworm.Application.Authors.Queries.GetAuthor;
using Bookworm.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookworm.Api.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAuthorsAsync()
    {
        var authorsResult = await mediator.Send(new GetAllAuthorsQuery());
        return ActionResult<Result<GetAllAuthorsResponse>>(authorsResult);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAuthorAsync(int id)
    {
        var authorResult = await mediator.Send(new GetAuthorQuery(id));
        return ActionResult<Result<GetAuthorResponse>>(authorResult);
    }
}