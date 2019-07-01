using System.Collections.Generic;
using FluentValidation.TestHelper;
using Honoplay.Application.Departments.Commands.CreateDepartment;
using Xunit;

namespace Honoplay.Application.Tests.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandValidatorTest
    {
        private readonly CreateDepartmentValidator _validator;
        public CreateDepartmentCommandValidatorTest()
        {
            _validator = new CreateDepartmentValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(
                new CreateDepartmentCommand
                {
                    AdminUserId = 1,
                    HostName = "localhost",
                    Departments = new List<string> { "yazilim", "tasarim" }
                }
                ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Departments, new List<string>());
        }

    }
}
