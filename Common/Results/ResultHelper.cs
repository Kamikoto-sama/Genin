using FluentResults;

namespace Common.Results;

public static class ResultHelper
{
    public static Result InvalidOperation(Enum code, string? details = null) =>
        Result.Fail(new ResultError(code, ResultErrorType.InvalidOperation, details));

    public static Result NotFound(Enum code, string? details = null) =>
        Result.Fail(new ResultError(code, ResultErrorType.NotFound, details));
}