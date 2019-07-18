using FluentValidation.TestHelper;
using Honoplay.Application.Questions.Queries.GetQuestionsList;
using Xunit;

namespace Honoplay.Application.Tests.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListValidatorTest : TestBase
    {
        private readonly GetQuestionsListValidator _validator;

        public GetQuestionsListValidatorTest()
        {
            _validator = new GetQuestionsListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetQuestionsListQueryModel { Take = 5 }).IsValid);
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
