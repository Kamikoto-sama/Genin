using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Provider.Validations;

public class ValidateAttribute<TValidator, TDto> : ActionFilterAttribute where TValidator : AbstractValidator<TDto>
{
    private readonly string parameterName;

    public ValidateAttribute(string parameterName)
    {
        this.parameterName = parameterName;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var validator = context.HttpContext.RequestServices.GetRequiredService<TValidator>();
        var parameterValue = (TDto)context.ActionArguments[parameterName];
        var validationResult = validator.Validate(parameterValue);
        if (validationResult.IsValid)
            return;

        foreach (var error in validationResult.Errors)
            context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
    }
}