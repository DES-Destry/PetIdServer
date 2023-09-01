using Microsoft.AspNetCore.Mvc;

namespace PetIdServer.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet(Name = "Test")]
    public IActionResult Test() => Ok("Test");
}