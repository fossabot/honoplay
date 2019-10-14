using FluentValidation.TestHelper;
using Honoplay.Application.Tags.Queries.GetTagsListByQuestionId;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Tags.Queries.GetTagsListByQuestionId
{
    public class GetTagsListByQuestionIdValidatorTest
    {
        private readonly GetTagsListByQuestionIdValidator _getTagsListByQuestionIdValidator;

        public GetTagsListByQuestionIdValidatorTest()
        {
            _getTagsListByQuestionIdValidator = new GetTagsListByQuestionIdValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getTagsListByQuestionIdValidator.Validate(new GetTagsListByQuestionIdQuery(questionId: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getTagsListByQuestionIdValidator.ShouldHaveValidationErrorFor(x => x.QuestionId, 0);
        }
    }
}
