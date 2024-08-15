using Bookworm.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookworm.Api.Controllers;

public class BaseController : ControllerBase
{
    protected static IActionResult ActionResult<T>(Result<T> booksResult)
    {
        return booksResult switch
        {
            SuccessResult<T> successResult => new OkObjectResult(successResult.Data),
            NotFoundErrorResult<T> => new NotFoundResult(),
            DatabaseErrorResult<T> => new StatusCodeResult(500),
            ErrorResult<T> => new StatusCodeResult(500),
            _ => new StatusCodeResult(500)
        };
    }
}