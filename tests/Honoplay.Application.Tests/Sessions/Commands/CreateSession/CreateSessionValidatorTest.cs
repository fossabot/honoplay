using System;
using System.Collections.Generic;
using Honoplay.Application.Sessions.Commands.CreateSession;
using Xunit;

namespace Honoplay.Application.Tests.Sessions.Commands.CreateSession
{
    public class CreateSessionValidatorTest
    {
        private readonly CreateSessionValidator _validator;

        public CreateSessionValidatorTest()
        {
            _validator = new CreateSessionValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new CreateSessionCommand
            {
                CreatedBy = 1,
                TenantId = Guid.NewGuid(),
                CreateSessionModels = new List<CreateSessionCommandModel>
                {
                    new CreateSessionCommandModel
                    {
                        ClassroomId = 1,
                        GameId = 1,
                        Name = "test"
                    }
                }
            }).IsValid);
        }
    }
}
