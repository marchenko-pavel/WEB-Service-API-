using Infrastructure.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
public class TestController : ControllerBase
{
    private readonly IRepository _repository;

    public TestController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("test/{body}")]
    public async Task<IActionResult> TestAsync(string body)
    {
        return Ok(body);
    }
}
