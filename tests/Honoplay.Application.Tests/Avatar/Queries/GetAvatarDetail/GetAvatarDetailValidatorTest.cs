﻿using FluentValidation.TestHelper;
using Xunit;

namespace Honoplay.Application.Tests.Avatars.Queries.GetAvatarDetail
{
    public class GetAvatarDetailValidatorTest
    {
        private readonly GetAvatarDetailValidator _getAvatarDetailValidator;

        public GetAvatarDetailValidatorTest()
        {
            _getAvatarDetailValidator = new GetAvatarDetailValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getAvatarDetailValidator.Validate(new GetAvatarDetailQuery(1)).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getAvatarDetailValidator.ShouldHaveValidationErrorFor(x => x.Id, null);
        }
    }
}