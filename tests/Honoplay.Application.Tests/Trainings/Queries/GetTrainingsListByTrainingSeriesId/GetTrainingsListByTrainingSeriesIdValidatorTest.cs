using FluentValidation.TestHelper;
using Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainingSeriesId;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Trainings.Queries.GetTrainingsListByTrainingSeriesId
{
    public class GetTrainingsListByTrainingSeriesIdValidatorTest
    {
        private readonly GetTrainingsListByTrainingSeriesIdValidator _validator;

        public GetTrainingsListByTrainingSeriesIdValidatorTest()
        {
            _validator = new GetTrainingsListByTrainingSeriesIdValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTrainingsListByTrainingSeriesIdQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }

    }
}
