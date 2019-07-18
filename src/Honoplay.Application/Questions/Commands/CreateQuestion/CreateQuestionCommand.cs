using System;
using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<ResponseModel<CreateQuestionModel>>
    {
        public int CreatedBy { get; set; }
        public Guid TenantId { get; set; }
        public string Text { get; set; }
        public int Duration { get; set; }
    }
}
