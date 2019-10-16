using FluentValidation.TestHelper;
using Honoplay.Application.QuestionTypes.Queries.GetQuestionTypesList;
using Xunit;

namespace Honoplay.Application.Tests.QuestionType.Queries.GetQuestionTypeList
{
    public class GetQuestionTypeListValidatorTest
    {
        private readonly GetQuestionTypesListValidator _getQuestionTypeListValidator;

        public GetQuestionTypeListValidatorTest()
        {
            _getQuestionTypeListValidator = new GetQuestionTypesListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getQuestionTypeListValidator.Validate(new GetQuestionTypesListQueryModel { Take = 5 }).IsValid);
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