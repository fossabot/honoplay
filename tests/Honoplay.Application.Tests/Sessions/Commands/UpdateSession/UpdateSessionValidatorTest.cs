using Xunit;

namespace Honoplay.Application.Tests.Sessions.Commands.UpdateSession
{
    public class UpdateSessionValidatorTest
    {
        private readonly UpdateSessionValidator _validator;

        public UpdateSessionValidatorTest()
        {
            _validator = new UpdateSessionValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new UpdateSessionCommand
            {
                Id = 1,
                GameId = 1,
                ClassroomId = 1,
                Name = "test"
            }).IsValid);
        }
    }
}
