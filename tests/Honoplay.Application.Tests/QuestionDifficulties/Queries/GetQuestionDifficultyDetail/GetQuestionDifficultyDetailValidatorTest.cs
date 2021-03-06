﻿using FluentValidation.TestHelper;
using Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultyDetail;
using Xunit;

namespace Honoplay.Application.Tests.QuestionDifficulties.Queries.GetQuestionDifficultyDetail
{
    public class GetQuestionDifficultyDetailValidatorTest
    {
        private readonly GetQuestionDifficultyDetailValidator _getQuestionDifficultyDetailValidator;

        public GetQuestionDifficultyDetailValidatorTest()
        {
            _getQuestionDifficultyDetailValidator = new GetQuestionDifficultyDetailValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getQuestionDifficultyDetailValidator.Validate(new GetQuestionDifficultyDetailQuery(id: 1)).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getQuestionDifficultyDetailValidator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
