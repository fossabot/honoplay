using System;
using FluentValidation.TestHelper;
using Honoplay.Application.QuestionCategories.Queries.GetQuestionCategoryDetail;
using Xunit;

namespace Honoplay.Application.Tests.QuestionCategorys.Queries.GetQuestionCategoryDetail
{
    public class GetQuestionCategoryDetailValidatorTest
    {
        private readonly GetQuestionCategoryDetailValidator _getQuestionCategoryDetailValidator;

        public GetQuestionCategoryDetailValidatorTest()
        {
            _getQuestionCategoryDetailValidator = new GetQuestionCategoryDetailValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getQuestionCategoryDetailValidator.Validate(new GetQuestionCategoryDetailQuery(adminUserId: 1, id: Guid.NewGuid(), tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getQuestionCategoryDetailValidator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }
    }
}
