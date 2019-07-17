using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommand : IRequest<ResponseModel<UpdateQuestionModel>>
    {
        public int Id { get; set; }
        public int UpdatedBy { get; set; }
        public Guid TenantId { get; set; }
        public string Text { get; set; }
        public int Duration { get; set; }
    }
}
