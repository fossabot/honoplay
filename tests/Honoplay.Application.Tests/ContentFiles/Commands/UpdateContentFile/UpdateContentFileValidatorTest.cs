using FluentValidation;
using FluentValidation.TestHelper;
using Honoplay.Application.ContentFiles.Commands.UpdateContentFile;
using System;
using Xunit;

namespace Honoplay.Application.Tests.ContentFiles.Commands.UpdateContentFile
{
    public class UpdateContentFileValidatorTest : AbstractValidator<UpdateContentFileCommand>
    {
        private readonly UpdateContentFileValidator _updateContentFileValidator;

        public UpdateContentFileValidatorTest()
        {
            _updateContentFileValidator = new UpdateContentFileValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_updateContentFileValidator.Validate(
                new UpdateContentFileCommand
                {
                    Id = Guid.NewGuid(),
                    Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    Name = "contentFile1",
                    ContentType = "image/jpeg"
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _updateContentFileValidator.ShouldHaveValidationErrorFor(x => x.Data, new byte[] { });
            _updateContentFileValidator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }
    }
}
