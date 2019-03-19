using FluentValidation;

namespace FluentValidator.Tests.Seed
{
    public class SeedDataValidator : AbstractValidator<SeedData>
    {
        public SeedDataValidator()
        {
            RuleFor(rf => rf.CreditCardValidatorProp).CreditCard();
            RuleFor(rf => rf.EmailValidatorProp).EmailAddress();
            RuleFor(rf => rf.InclusiveBetweenValidatorProp).InclusiveBetween(18,60);
            RuleFor(rf => rf.LengthValidatorProp).Length(1, 6);
            RuleFor(rf => rf.MaximumLengthValidatorProp).MaximumLength(6);
            RuleFor(rf => rf.MinimumLengthValidatorProp).MinimumLength(4);
            RuleFor(rf => rf.NotNullValidatorProp).NotNull();
            RuleFor(rf => rf.NotEmptyValidatorProp).NotEmpty();
            RuleFor(rf => rf.NotNullValidatorProp).NotNull();
            RuleFor(rf => rf.CompositeValidatorProp).NotNull().EmailAddress();
        }
    }
}
