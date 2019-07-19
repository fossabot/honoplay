using System;
using FluentValidation.TestHelper;
using Honoplay.Application.Answers.Queries.GetAnswerDetail;
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
            Assert.True(_getAnswerDetailValidator.Validate(new GetAnswerDetailQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getAnswerDetailValidator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
