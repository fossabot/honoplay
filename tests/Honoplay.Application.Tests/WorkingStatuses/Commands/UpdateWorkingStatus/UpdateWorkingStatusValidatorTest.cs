using FluentValidation.TestHelper;
using Honoplay.Application.WorkingStatuses.Commands.UpdateWorkingStatus;
using Xunit;

namespace Honoplay.Application.Tests.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public class UpdateWorkingStatusValidatorTest : TestBase
    {
        private readonly UpdateWorkingStatusValidator _validator;

        public UpdateWorkingStatusValidatorTest()
        {
            _validator = new UpdateWorkingStatusValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new UpdateWorkingStatusCommand
            {
                Id = 1,
                Name = "asdasd",
                HostName = "localhost",
                UpdatedBy = 1
            }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }
    }
}