using Xunit;

namespace Honoplay.Application.Tests.WorkingStatuses.Commands.CreateWorkingStatus
{
    public class CreateWorkingStatusValidatorTest : TestBase
    {
        private readonly CreateWorkingStatusValidator _validator;

        public CreateWorkingStatusValidatorTest(CreateWorkingStatusValidator validator)
        {
            _validator = validator;

        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new CreateWorkingStatusCommand
            {
                Name = "asdasd",
                HostName = "localhost",
                CreatedBy = 1
            }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.CreatedBy, 0);
        }
    }
}