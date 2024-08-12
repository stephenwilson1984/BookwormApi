using Microsoft.AspNetCore.Mvc;

namespace Bookworm.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    [HttpGet]
    public JsonResult GetBooks()
    {
        return new JsonResult(new List<object>
        {
            new { Id = 1, Name = "The Old Man and the Sea" },
            new { Id = 2, Name = "Of Mice and Men" }
        });
    }
}