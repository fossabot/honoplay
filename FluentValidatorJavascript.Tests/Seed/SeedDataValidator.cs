using FluentValidation;

namespace FluentValidator.Tests.Seed
{
    public class SeedDataValidator : AbstractValidator<SeedData>
    {
        public SeedDataValidator()
        {
            RuleFor(rf => rf.NotNullValidatorProp)
                .NotNull()
                .WithMessage("NotNullValidator");

            RuleFor(rf => rf.NotEmptyValidatorProp)
                .NotEmpty()
                .WithMessage("NotEmptyValidator");

            RuleFor(rf => rf.CreditCardValidatorProp)
                .CreditCard()
                .WithMessage("CreditCardValidator");

            RuleFor(rf => rf.EmailValidatorProp)
                .EmailAddress()
                .WithMessage("EmailValidator");

            RuleFor(rf => rf.InclusiveBetweenValidatorProp)
                .InclusiveBetween(18, 60)
                .WithMessage("InclusiveBetweenValidator");

            RuleFor(rf => rf.MaximumLengthValidatorProp)
                .MaximumLength(6)
                .WithMessage("MaximumLengthValidator");

            RuleFor(rf => rf.MinimumLengthValidatorProp)
                .MinimumLength(4)
                .WithMessage("MinimumLengthValidator");




        }
    }
}
