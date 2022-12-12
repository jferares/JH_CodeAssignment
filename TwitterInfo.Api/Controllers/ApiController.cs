
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JH.CodeAssignment.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    protected readonly IConfiguration _config;

    public ApiController(IConfiguration config)
    {
        _config = config;
    }

    protected IActionResult Problem(List<Error> errors)
    {
        // look to see if all errors are validation errors.  If so group them up and return
        if(errors.All(e => e.Type == ErrorType.Validation))
        {
            ModelStateDictionary modelStateDictionary = new();
            foreach(Error error in errors)
                modelStateDictionary.AddModelError(error.Code, error.Description);
            return ValidationProblem(modelStateDictionary);
        }

        // handle the unexpected case (other errors are likely not reliable)
        if(errors.Any(e => e.Type == ErrorType.Unexpected))
            return Problem();

        // otherwise we'll use the first error
        Error firstError = errors[0];

        int statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}