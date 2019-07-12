using System;
using System.Collections.Generic;
using Xunit;

namespace Honoplay.Application.Tests.Professions.Commands.CreateProfession
{
    public class CreateProfessionValidatorTest
    {
        private readonly CreateProfessionValidator _validator;
        public CreateProfessionCommandValidatorTest()
        {
            _validator = new CreateProfessionValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(
                new CreateProfessionCommand
                {
                    AdminUserId = 1,
                    TenantId = new Guid(),
                    Professions = new List<string> { "yazilim", "tasarim" }
                }
                ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Professions, new List<string>());
        }
    }
}
