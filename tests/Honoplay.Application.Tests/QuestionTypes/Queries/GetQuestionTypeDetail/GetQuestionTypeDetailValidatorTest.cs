﻿using System;
using FluentValidation.TestHelper;
using Honoplay.Application.QuestionTypes.Queries.GetQuestionTypeDetail;
using Xunit;

namespace Honoplay.Application.Tests.QuestionTypes.Queries.GetQuestionTypeDetail
{
    public class GetQuestionTypeDetailValidatorTest
    {
        private readonly GetQuestionTypeDetailValidator _getQuestionTypeDetailValidator;

        public GetQuestionTypeDetailValidatorTest()
        {
            _getQuestionTypeDetailValidator = new GetQuestionTypeDetailValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getQuestionTypeDetailValidator.Validate(new GetQuestionTypeDetailQuery(adminUserId: 1, id: Guid.NewGuid(), tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getQuestionTypeDetailValidator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
