using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.TestHelper;
using Honoplay.Application.Options.Commands.CreateOption;
using Xunit;

namespace Honoplay.Application.Tests.Options.Commands.CreateOption
{
    public class CreateOptionValidatorTest : AbstractValidator<CreateOptionCommand>
    {
        private readonly CreateOptionValidator _createOptionValidator;

        public CreateOptionValidatorTest()
        {
            _createOptionValidator = new CreateOptionValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            var newOption = new List<CreateOptionCommandModel>
            {
                new CreateOptionCommandModel
                {
                    Text = "sample",
                    QuestionId = 1,
                    IsCorrect = true,
                    VisibilityOrder = 0
                }
            };
            Assert.True(_createOptionValidator.Validate(new CreateOptionCommand { CreateOptionModels = newOption }).IsValid);
        }

    }
}
