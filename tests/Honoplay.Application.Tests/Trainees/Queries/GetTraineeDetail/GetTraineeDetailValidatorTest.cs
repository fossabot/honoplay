using FluentValidation.TestHelper;
using Honoplay.Application.Trainees.Queries.GetTraineeDetail;
using Xunit;

namespace Honoplay.Application.Tests.Trainees.Queries.GetTraineeDetail
{
    public class GetTraineeDetailValidatorTest : TestBase
    {
        private readonly GetTraineeDetailValidator _validator;
        private readonly string _hostName = "localhost";

        public GetTraineeDetailValidatorTest()
        {
            _validator = new GetTraineeDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTraineeDetailQuery(adminUserId: 1, id: 1, _hostName)).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
