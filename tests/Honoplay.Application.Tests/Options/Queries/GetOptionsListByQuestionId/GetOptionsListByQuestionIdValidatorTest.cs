using FluentValidation.TestHelper;
using Honoplay.Application.Options.Queries.GetOptionsListByQuestionId;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Options.Queries.GetOptionsListByQuestionId
{
    public class GetOptionsListByQuestionIdValidatorTest
    {
        private readonly GetOptionsListByQuestionIdValidator _getOptionsListByQuestionIdValidator;

        public GetOptionsListByQuestionIdValidatorTest()
        {
            _getOptionsListByQuestionIdValidator = new GetOptionsListByQuestionIdValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getOptionsListByQuestionIdValidator.Validate(new GetOptionsListByQuestionIdQuery(questionId: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getOptionsListByQuestionIdValidator.ShouldHaveValidationErrorFor(x => x.QuestionId, 0);
        }
    }
}
