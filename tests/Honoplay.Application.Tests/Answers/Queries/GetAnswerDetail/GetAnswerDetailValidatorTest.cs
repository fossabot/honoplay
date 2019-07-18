using Xunit;

namespace Honoplay.Application.Tests.Answers.Queries.GetAnswerDetail
{
    public class GetAnswerDetailValidatorTest
    {
        private readonly GetAnswerDetailValidator _getAnswerDetailValidator;

        public GetAnswerDetailValidatorTest()
        {
            _getAnswerDetailValidator = new GetAnswerDetailValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getAnswerDetailValidator.Validate(new GetAnswerDetailQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _getAnswerDetailValidator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _getAnswerDetailValidator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _getAnswerDetailValidator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}
