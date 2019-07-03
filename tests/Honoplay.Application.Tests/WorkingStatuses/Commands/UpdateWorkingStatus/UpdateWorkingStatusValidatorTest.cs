using Xunit;

namespace Honoplay.Application.Tests.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public class UpdateWorkingStatusValidatorTest : TestBase
    {
        private readonly UpdateWorkingStatusValidator _validator;

        public UpdateWorkingStatusValidatorTest(UpdateWorkingStatusValidator validator)
        {
            _validator = validator;
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new UpdateWorkingStatusCommand
            {
                Id = 1,
                Name = "asdasd",
                TenantId = 1,
                CreatedBy = 1,
                UpdatedBy = 1,
            }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.TenantId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.UpdatedBy, 0);
        }
    }
}