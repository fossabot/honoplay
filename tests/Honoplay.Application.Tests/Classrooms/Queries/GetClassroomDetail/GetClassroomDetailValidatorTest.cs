using FluentValidation.TestHelper;
using Honoplay.Application.Classrooms.Queries.GetClassroomDetail;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Classrooms.Queries.GetClassroomDetail
{
    public class GetClassroomDetailValidatorTest
    {
        private readonly GetClassroomDetailValidator _validator;

        public GetClassroomDetailValidatorTest()
        {
            _validator = new GetClassroomDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetClassroomDetailQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
