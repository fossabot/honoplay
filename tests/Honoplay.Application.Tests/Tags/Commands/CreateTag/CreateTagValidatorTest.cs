using FluentValidation;
using Honoplay.Application.Tags.Commands.CreateTag;
using System.Collections.Generic;
using Xunit;

namespace Honoplay.Application.Tests.Tags.Commands.CreateTag
{
    public class CreateTagValidatorTest : AbstractValidator<CreateTagCommand>
    {
        private readonly CreateTagValidator _createTagValidator;

        public CreateTagValidatorTest()
        {
            _createTagValidator = new CreateTagValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            var newTag = new List<CreateTagCommandModel>
            {
                new CreateTagCommandModel
                {
                    ToQuestion = true,
                    Name = "sample"
                }
            };
            Assert.True(_createTagValidator.Validate(new CreateTagCommand { CreateTagModels = newTag }).IsValid);
        }

    }
}
