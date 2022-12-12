using Microsoft.AspNetCore.Mvc;

namespace JH.CodeAssignment.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}