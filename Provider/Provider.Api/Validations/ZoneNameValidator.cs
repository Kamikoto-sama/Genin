using System.Text.RegularExpressions;
using FluentValidation;

namespace Provider.Api.Validations;

public class ZoneNameValidator : AbstractValidator<string>
{
    private const string namePattern = @"^[0-9a-zA-z#\.\-]$";
    private static readonly Regex nameRegex = new(namePattern, RegexOptions.Compiled);

    public ZoneNameValidator()
    {
        RuleFor(zoneName => zoneName)
            .NotEmpty()
            .MaximumLength(100)
            .Must(name => nameRegex.IsMatch(name)).WithMessage($"Zone name must match: {namePattern}");
    }
}