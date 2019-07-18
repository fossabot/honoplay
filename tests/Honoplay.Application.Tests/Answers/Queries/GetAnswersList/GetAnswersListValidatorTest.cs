using Xunit;

namespace Honoplay.Application.Tests.Answers.Queries.GetAnswersList
{
    public class GetAnswersListValidatorTest
    {
        private readonly GetAnswersListValidator _getAnswersListValidator;

        public GetAnswersListValidatorTest()
        {
            _getAnswersListValidator = new GetAnswersListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getAnswersListValidator.Validate(new GetAnswersListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _getAnswersListValidator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _getAnswersListValidator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _getAnswersListValidator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}
