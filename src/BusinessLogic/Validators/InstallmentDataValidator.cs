using FluentValidation;
using AlifTask.DataAccess;
namespace AlifTask.BusinessLogic;

public sealed class InstallmentDataValidator : AbstractValidator<InstallmentDataDto>
{
    public InstallmentDataValidator()
    {
        RuleFor(i => i.PhoneNumberClient)
            .MaximumLength(maximumLength: 20)
            .NotEmpty();
        RuleFor(i => i.Price)
            .GreaterThan(valueToCompare: 1)
            .NotEmpty();
        RuleFor(i => i.ClientInstallmentRange)
            .GreaterThanOrEqualTo(valueToCompare: 3)
            .NotEmpty();
        RuleFor(i => i.ProductType)
            .NotEmpty();
    }
}