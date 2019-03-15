namespace FluentValidator.Tests.Seed
{
    public class SeedData
    {
        public string NotNullValidatorProp { get; set; } = "lorem";
        public string MinimumLengthValidatorProp { get; set; } = "lorem";
        public string MaximumLengthValidatorProp { get; set; } = "lorem";
        public string LengthValidatorProp { get; set; } = "lorem";
        public string EmailValidatorProp { get; set; } = "lorem@gmail.com";
        public string CreditCardValidatorProp { get; set; } = "5105105105105100";
        public string NotEmptyValidatorProp { get; set; } = "lorem";
        public int? InclusiveBetweenValidatorProp { get; set; } = 19;
    }
}