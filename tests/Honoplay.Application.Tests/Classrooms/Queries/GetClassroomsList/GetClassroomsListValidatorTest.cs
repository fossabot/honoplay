using Xunit;

namespace Honoplay.Application.Tests.Classrooms.Queries.GetClassroomsList
{
    public class GetClassroomsListValidatorTest
    {
        private readonly GetClassroomsListValidator _validator;

        public GetClassroomsListValidatorTest()
        {
            _validator = new GetClassroomsListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetClassroomsListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}
