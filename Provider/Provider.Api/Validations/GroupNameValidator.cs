using System.Text.RegularExpressions;
using FluentValidation;

namespace Provider.Api.Validations;

public class GroupNameValidator : AbstractValidator<string>
{
    private const string namePattern = @"^[0-9a-zA-z#\.\-]$";
    private static readonly Regex nameRegex = new(namePattern, RegexOptions.Compiled);

    public GroupNameValidator()
    {
        RuleFor(groupName => groupName)
            .NotEmpty()
            .MaximumLength(100)
            .Must(name => nameRegex.IsMatch(name)).WithMessage($"Group name must match: {namePattern}");
    }
}