using FluentValidation.TestHelper;
using Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultiesList;
using Xunit;

namespace Honoplay.Application.Tests.QuestionDifficulty.Queries.GetQuestionDifficultyList
{
    public class GetQuestionDifficultyListValidatorTest
    {
        private readonly GetQuestionDifficultiesListValidator _getQuestionDifficultiesListValidator;

        public GetQuestionDifficultyListValidatorTest()
        {
            _getQuestionDifficultiesListValidator = new GetQuestionDifficultiesListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getQuestionDifficultiesListValidator.Validate(new GetQuestionDifficultiesListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _getQuestionDifficultiesListValidator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _getQuestionDifficultiesListValidator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _getQuestionDifficultiesListValidator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}