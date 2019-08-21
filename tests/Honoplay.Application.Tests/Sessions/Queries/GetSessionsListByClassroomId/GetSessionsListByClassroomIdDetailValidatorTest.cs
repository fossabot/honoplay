using FluentValidation.TestHelper;
using Honoplay.Application.Sessions.Queries.GetSessionsListByClassroomId;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Options.Queries.GetSessionsListByClassroomId
{
    public class GetSessionsListByClassroomIdValidatorTest
    {
        private readonly GetSessionsListByClassroomIdValidator _getSessionsListByClassroomIdValidator;

        public GetSessionsListByClassroomIdValidatorTest()
        {
            _getSessionsListByClassroomIdValidator = new GetSessionsListByClassroomIdValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getSessionsListByClassroomIdValidator.Validate(new GetSessionsListByClassroomIdQuery(classroomId: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getSessionsListByClassroomIdValidator.ShouldHaveValidationErrorFor(x => x.ClassroomId, 0);
        }
    }
}
