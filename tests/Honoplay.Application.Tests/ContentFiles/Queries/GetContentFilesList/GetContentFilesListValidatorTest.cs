using FluentValidation.TestHelper;
using Honoplay.Application.ContentFiles.Queries.GetContentFilesList;
using Xunit;

namespace Honoplay.Application.Tests.ContentFiles.Queries.GetContentFilesList
{
    public class GetContentFilesListValidatorTest
    {
        private readonly GetContentFilesListValidator _getContentFilesListValidator;

        public GetContentFilesListValidatorTest()
        {
            _getContentFilesListValidator = new GetContentFilesListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getContentFilesListValidator.Validate(new GetContentFilesListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _getContentFilesListValidator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _getContentFilesListValidator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _getContentFilesListValidator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}
