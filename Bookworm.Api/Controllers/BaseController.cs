using System.Net;
using System.Reflection.Metadata.Ecma335;
using Bookworm.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookworm.Api.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult ActionResult<T>(Result booksResult)
    {
        return booksResult switch
        {
            SuccessResult<T> successResult => Ok(successResult.Data),
            NotFoundErrorResult => NotFound(),
            DatabaseErrorResult => StatusCode((int)HttpStatusCode.InternalServerError),
            ValidationErrorResult validationError => BadRequest(validationError),
            ErrorResult<T> => StatusCode((int)HttpStatusCode.InternalServerError),
            _ => StatusCode((int)HttpStatusCode.InternalServerError)
        };
    }
}