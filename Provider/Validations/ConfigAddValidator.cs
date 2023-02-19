using System.Text.RegularExpressions;
using Common;
using FluentValidation;
using Provider.Dto.Configs;

namespace Provider.Validations;

public class ConfigAddValidator : AbstractValidator<ConfigAddDto[]>
{
    private static readonly Regex keyRegex = new(@"[0-9a-zA-Z_\/]+", RegexOptions.Compiled);

    public ConfigAddValidator()
    {
        RuleForEach(configs => configs.Select(config => config.Key))
            .Must(key => key.IsSignificant()).WithMessage("Key must not be null, empty or whitespace")
            .Must(key => keyRegex.IsMatch(key)).WithMessage("Key may consist of digits (0-9) and alphabet (a-z, A-Z) separated by forward slashes (/)")
            .Must(key => key.StartsWith("/") || key.EndsWith("/")).WithMessage("Key must not start or end with slashes");

        RuleFor(config => config)
            .NotEmpty().WithMessage("Must be more than 0 configs")
            .Must(configs => configs.All(x => !configs.Any(y => y.Key.StartsWith(x.Key)))).WithMessage("Keys must not be prefixes of each other");
    }
}