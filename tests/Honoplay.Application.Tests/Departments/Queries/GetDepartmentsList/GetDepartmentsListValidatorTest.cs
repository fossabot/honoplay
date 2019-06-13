using FluentValidation.TestHelper;
using Honoplay.Application.Departments.Queries.GetDepartmentsList;
using Xunit;

namespace Honoplay.Application.Tests.Departments.Queries.GetDepartmentsList
{
    public class GetDepartmentsListValidatorTest : TestBase
    {
        private readonly GetDepartmentsListValidator _validator;

        public GetDepartmentsListValidatorTest()
        {
            _validator = new GetDepartmentsListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetDepartmentsListQuery(adminUserId: 1, hostName: "localhost", skip: 0, take: 10)).IsValid);
            Assert.True(_validator.Validate(new GetDepartmentsListQuery(adminUserId: 1, hostName: "localhost")).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 4);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 101);
        }
    }
}
