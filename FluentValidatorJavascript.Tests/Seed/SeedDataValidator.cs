using FluentValidation;
using FluentValidatorJavascript;

namespace FluentValidator.Tests.Seed
{
    public class SeedDataValidator : AbstractValidator<SeedData>
    {
        public SeedDataValidator()
        {

            RuleFor(rf => rf.CreditCardValidatorProp)
                .CreditCard()
                .WithMessage("CreditCard");

            RuleFor(rf => rf.EmailValidatorProp)
                .EmailAddress()
                .WithMessage("Email");

            RuleFor(rf => rf.InclusiveBetweenValidatorProp)
                .InclusiveBetween(18, 60)
                .WithMessage("InclusiveBetween");

            RuleFor(rf => rf.LengthValidatorProp)
                .Length(1, 6)
                .WithMessage("Length");

            RuleFor(rf => rf.MaximumLengthValidatorProp)
                .MaximumLength(6)
                .WithMessage("MaximumLength");

            RuleFor(rf => rf.MinimumLengthValidatorProp)
                .MinimumLength(4)
                .WithMessage("MinimumLength");

            RuleFor(rf => rf.NotNullValidatorProp)
                .NotNull()
                .WithMessage("NotNull");

            RuleFor(rf => rf.NotEmptyValidatorProp)
                .NotEmpty()
                .WithMessage("NotEmpty");

            RuleFor(rf => rf.CustomNullValidatorProp)
                .NotNull()
                .WithMessage("CustomNotNull");

            RuleFor(rf => rf.CompositeValidatorProp)
                .NotNull()
                .WithMessage("CompositeNotNull")
                .EmailAddress()
                .WithMessage("CompositeEmail");
        }
    }
}
