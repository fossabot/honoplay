﻿using FluentValidation.TestHelper;
using Honoplay.Application.TrainerUsers.Queries.GetTrainerUserDetail;
using System;
using Xunit;

namespace Honoplay.Application.Tests.TrainerUsers.Queries.GetTrainerUserDetail
{
    public class GetTrainerUserDetailValidatorTest
    {
        private readonly GetTrainerUserDetailValidator _validator;

        public GetTrainerUserDetailValidatorTest()
        {
            _validator = new GetTrainerUserDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTrainerUserDetailQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
