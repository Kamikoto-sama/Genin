using System.Diagnostics.CodeAnalysis;
using Common.Results;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Common.Api;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected async Task<ActionResult> HandleAsync(Func<Task<Result>> execute)
    {
        if (!ValidateModel(out var validationResult))
            return validationResult;

        var result = await execute();
        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    protected async Task<ActionResult<T>> HandleAsync<T>(Func<Task<Result<T>>> execute)
    {
        if (!ValidateModel(out var validationResult))
            return validationResult;

        var result = await execute();
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    private bool ValidateModel([NotNullWhen(false)] out ActionResult? actionResult)
    {
        actionResult = default;
        if (ModelState.IsValid)
            return true;

        var details = ModelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage)
            .ToStringJoin("; ");

        var result = ResultHelper.InvalidOperation(ModelError.InvalidModel, details);
        actionResult = ToActionResult(result);
        return false;
    }

    protected async Task<ActionResult<TResult>> HandleAsync<TSource, TResult>(Func<Task<Result<TSource>>> execute, Func<TSource, TResult> mapToDto)
    {
        if (!ValidateModel(out var validationResult))
            return validationResult;

        var result = await execute();
        return result.IsSuccess ? Ok(mapToDto(result.Value)) : ToActionResult(result);
    }

    private ActionResult ToActionResult(IResultBase result)
    {
        var error = (ResultError)result.Errors.Single(x => x is ResultError);
        if (error.Type == ResultErrorType.NotFound)
            return NotFound();

        var problemDetails = new ProblemDetails
        {
            Detail = error.Message,
            Type = error.Code.ToString()
        };

        return BadRequest(problemDetails);
    }
}

public enum ModelError
{
    InvalidModel
}