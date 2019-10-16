using FluentValidation.TestHelper;
using Xunit;

namespace Honoplay.Application.Tests.QuestionType.Queries.GetQuestionTypeList
{
    public class GetQuestionTypeListValidatorTest
    {
        private readonly GetQuestionTypeListValidator _getQuestionTypeListValidator;

        public GetQuestionTypeListValidatorTest()
        {
            _getQuestionTypeListValidator = new GetQuestionTypeListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getQuestionTypeListValidator.Validate(new GetQuestionTypeListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _getQuestionTypeListValidator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _getQuestionTypeListValidator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _getQuestionTypeListValidator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}