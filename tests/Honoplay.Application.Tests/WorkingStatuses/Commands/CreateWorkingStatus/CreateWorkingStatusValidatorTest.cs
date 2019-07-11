using FluentValidation.TestHelper;
using Honoplay.Application.WorkingStatuses.Commands.CreateWorkingStatus;
using System;
using Xunit;

namespace Honoplay.Application.Tests.WorkingStatuses.Commands.CreateWorkingStatus
{
    public class CreateWorkingStatusValidatorTest
    {
        private readonly CreateWorkingStatusValidator _validator;

        public CreateWorkingStatusValidatorTest()
        {
            _validator = new CreateWorkingStatusValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new CreateWorkingStatusCommand
            {
                Name = "asdasd",
                TenantId = Guid.NewGuid(),
                CreatedBy = 1,
            }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }
    }
}