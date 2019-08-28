﻿using FluentValidation.TestHelper;
using Honoplay.Application.TraineeUsers.Commands.UpdateTraineeUser;
using System.Linq;
using Xunit;

namespace Honoplay.Application.Tests.TraineeUsers.Commands.UpdateTraineeUser
{
    public class UpdateTraineeUserValidatorTest : TestBase
    {
        private readonly UpdateTraineeUserValidator _validator;
        public UpdateTraineeUserValidatorTest()
        {
            _validator = new UpdateTraineeUserValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new UpdateTraineeUserCommand
            {
                Id = 1,
                Name = "asdasd",
                DepartmentId = 1,
                UpdatedBy = 1,
                Surname = "asdasd",
                PhoneNumber = "21412312321",
                Gender = 1,
                NationalIdentityNumber = "214123123123",
                WorkingStatusId = 1
            }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.NationalIdentityNumber, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.PhoneNumber, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.WorkingStatusId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.DepartmentId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.Surname, string.Empty);
        }

        [Fact]
        public void ShouldBeNotValidForLessThan()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Gender, -5);
        }

        [Fact]
        public void ShouldBeNotValidForGreaterThan()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Gender, 3);
        }

        [Fact]
        public void ShouldBeNotValidForMaxLength()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Join("", Enumerable.Repeat("x", 51)));
            _validator.ShouldHaveValidationErrorFor(x => x.Surname, string.Join("", Enumerable.Repeat("x", 51)));
            _validator.ShouldHaveValidationErrorFor(x => x.NationalIdentityNumber, string.Join("", Enumerable.Repeat("x", 31)));
            _validator.ShouldHaveValidationErrorFor(x => x.PhoneNumber, string.Join("", Enumerable.Repeat("x", 21)));
        }
    }
}
