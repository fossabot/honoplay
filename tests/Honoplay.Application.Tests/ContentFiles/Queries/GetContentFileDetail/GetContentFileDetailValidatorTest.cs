using System;
using FluentValidation.TestHelper;
using Honoplay.Application.ContentFiles.Queries.GetContentFileDetail;
using Xunit;

namespace Honoplay.Application.Tests.ContentFiles.Queries.GetContentFileDetail
{
    public class GetContentFileDetailValidatorTest
    {
        private readonly GetContentFileDetailValidator _getContentFileDetailValidator;

        public GetContentFileDetailValidatorTest()
        {
            _getContentFileDetailValidator = new GetContentFileDetailValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getContentFileDetailValidator.Validate(new GetContentFileDetailQuery(adminUserId: 1, id: Guid.NewGuid(), tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getContentFileDetailValidator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }
    }
}
