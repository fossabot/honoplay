using FluentValidation.TestHelper;
using Honoplay.Application.Professions.Queries.GetProfessionsList;
using Xunit;

namespace Honoplay.Application.Tests.Professions.Queries.GetProfessionsList
{
    public class GetProfessionsListValidatorTest
    {
        private readonly GetProfessionsListValidator _validator;

        public GetProfessionsListValidatorTest()
        {
            _validator = new GetProfessionsListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetProfessionsListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}
