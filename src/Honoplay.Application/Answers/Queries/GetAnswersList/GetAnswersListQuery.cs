using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Answers.Queries.GetAnswersList
{
    public class GetAnswersListQuery : IRequest<ResponseModel<AnswersListModel>>
    {
        public GetAnswersListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetAnswersListQuery() { }

        public Guid TenantId { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetAnswersListQueryModel : IRequest<ResponseModel<AnswersListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
