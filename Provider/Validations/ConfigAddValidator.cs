using System.Text.RegularExpressions;
using FluentValidation;
using Provider.Dto.Configs;

namespace Provider.Validations;

public class ConfigAddValidator : AbstractValidator<ConfigAddDto[]>
{
    private static readonly Regex keyRegex = new(@"[0-9a-zA-Z_\/]+", RegexOptions.Compiled);

    public ConfigAddValidator()
    {
        RuleForEach(configs => configs)
            .Must(config => keyRegex.IsMatch(config.Key)).WithMessage("Key may consist of digits (0-9) and alphabet (a-z, A-Z) separated by forward slashes (/)")
            .Must(config => !config.Key.StartsWith("/") && !config.Key.EndsWith("/")).WithMessage("Key must not start or end with slashes");

        RuleFor(configs => configs)
            .NotEmpty().WithMessage("Must be more than 0 configs")
            .Must(configs => configs.Length == configs.Select(config => config.Key).ToHashSet().Count).WithMessage("Keys must be unique")
            .DependentRules(() => RuleFor(configs => configs)
                .Must(configs => configs.All(config => !configs.Where(other => other != config).Any(other => other.Key.StartsWith(config.Key))))
                .WithMessage("Keys must not be prefixes of each other"));
    }
}