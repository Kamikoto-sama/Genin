using System.Text.RegularExpressions;
using Common;
using FluentValidation;
using Provider.Dto.Configs;

namespace Provider.Validations;

public class ConfigAddValidator : AbstractValidator<ConfigAddDto[]>
{
    private const string keyPattern = @"^[0-9a-zA-Z_\/]+$";
    private static readonly Regex keyRegex = new(keyPattern, RegexOptions.Compiled);

    public ConfigAddValidator()
    {
        RuleForEach(configs => configs).ChildRules(rules => rules
            .RuleFor(config => config.Key)
            .MaximumLength(256)
            .Must(key => keyRegex.IsMatch(key)).WithMessage($"Key must match: {keyPattern}")
            .Must(key => !key.StartsWith("/") && !key.EndsWith("/")).WithMessage("Key must not start or end with slashes")
            .Must(key => key.Split("/").All(part => part.IsSignificant())).WithMessage("Key must not contain empty parts")
        );

        RuleFor(configs => configs)
            .NotEmpty().WithMessage("Must be more than 0 configs")
            .Must(configs => configs.Length == configs.Select(config => config.Key).ToHashSet().Count).WithMessage("Keys must be unique")
            .DependentRules(() => RuleFor(configs => configs)
                .Must(configs => configs.All(config => !configs.Where(other => other != config).Any(other => other.Key.StartsWith(config.Key))))
                .WithMessage("Keys must not be prefixes of each other"));
    }
}