﻿using FluentValidation;
using FluentValidation.TestHelper;
using Honoplay.Application.Tags.Commands.UpdateTag;
using Xunit;

namespace Honoplay.Application.Tests.Tags.Commands.UpdateTag
{
    public class UpdateTagValidatorTest : AbstractValidator<UpdateTagCommand>
    {
        private readonly UpdateTagValidator _updateTagValidator;

        public UpdateTagValidatorTest()
        {
            _updateTagValidator = new UpdateTagValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_updateTagValidator.Validate(
                new UpdateTagCommand
                {
                    ToQuestion = true,
                    Name = "tag1"
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _updateTagValidator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            _updateTagValidator.ShouldHaveValidationErrorFor(x => x.ToQuestion, null);
        }
    }
}
