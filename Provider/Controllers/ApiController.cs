using Common.Results;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Provider.Controllers;

public abstract class ApiController : Controller
{
    [NonAction]
    public async Task<ActionResult> HandleAsync(Func<Task<Result>> execute)
    {
        var result = await execute();
        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    [NonAction]
    public async Task<ActionResult<T>> HandleAsync<T>(Func<Task<Result<T>>> execute)
    {
        var result = await execute();
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    public async Task<ActionResult<TResult>> HandleAsync<TSource, TResult>(Func<Task<Result<TSource>>> execute, Func<TSource, TResult> mapToDto)
    {
        var result = await execute();
        return result.IsSuccess ? Ok(mapToDto(result.Value)) : ToActionResult(result);
    }

    private ActionResult ToActionResult<T>(Result<T> result) => ToActionResult(result.ToResult());

    private ActionResult ToActionResult(Result result)
    {
        var error = (ResultError)result.Errors.Single(x => x is ResultError);
        if (error.Type == ResultErrorType.NotFound)
            return NotFound();

        var problemDetails = new ProblemDetails
        {
            Detail = error.Message,
            Title = error.Code.ToString(),
            Type = error.Code.ToString()
        };

        return BadRequest(problemDetails);
    }
}