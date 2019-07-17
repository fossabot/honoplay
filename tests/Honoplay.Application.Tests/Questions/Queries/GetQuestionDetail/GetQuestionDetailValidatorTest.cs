using System;
using Xunit;

namespace Honoplay.Application.Tests.Questions.Queries.GetQuestionDetail
{
    public class GetQuestionDetailValidatorTest : TestBase
    {
        private readonly GetQuestionDetailValidator _validator;

        public GetQuestionDetailValidatorTest()
        {
            _validator = new GetQuestionDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetQuestionDetailQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}