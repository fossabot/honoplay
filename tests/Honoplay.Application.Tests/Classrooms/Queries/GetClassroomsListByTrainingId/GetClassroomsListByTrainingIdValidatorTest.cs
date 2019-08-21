using FluentValidation.TestHelper;
using Honoplay.Application.Classrooms.Queries.GetClassroomsListByTrainingId;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Classrooms.Queries.GetClassroomsList
{
    public class GetClassroomsListByTrainingIdValidatorTest
    {
        private readonly GetClassroomsListByTrainingIdValidator _validator;

        public GetClassroomsListByTrainingIdValidatorTest()
        {
            _validator = new GetClassroomsListByTrainingIdValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetClassroomsListByTrainingIdQuery(trainingId: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.TrainingId, 0);
        }
    }
}
