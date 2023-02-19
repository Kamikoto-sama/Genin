using FluentResults;

namespace Common.Results;

public class ResultError : IError
{
    public ResultErrorType Type { get; }
    public Enum Code { get; }
    public string? Message { get; }
    public Dictionary<string, object> Metadata { get; }
    public List<IError> Reasons { get; }

    public ResultError(Enum code, ResultErrorType type, string? details = null)
    {
        Code = code;
        Message = details;
        Type = type;
        Metadata = new Dictionary<string, object>();
        Reasons = new List<IError>();
    }
}