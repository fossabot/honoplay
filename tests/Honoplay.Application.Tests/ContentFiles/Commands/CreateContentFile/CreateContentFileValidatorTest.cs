using FluentValidation;
using Honoplay.Application.ContentFiles.Commands.CreateContentFile;
using System.Collections.Generic;
using Xunit;

namespace Honoplay.Application.Tests.ContentFiles.Commands.CreateContentFile
{
    public class CreateContentFileValidatorTest : AbstractValidator<CreateContentFileCommand>
    {
        private readonly CreateContentFileValidator _createContentFileValidator;

        public CreateContentFileValidatorTest()
        {
            _createContentFileValidator = new CreateContentFileValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            var newContentFile = new List<CreateContentFileCommandModel>
            {
                new CreateContentFileCommandModel
                {
                    Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    Name = "contentFile1",
                    ContentType = "image/jpeg"
                }
            };
            Assert.True(_createContentFileValidator.Validate(new CreateContentFileCommand { CreateContentFileModels = newContentFile }).IsValid);
        }

    }
}