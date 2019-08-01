using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Trainings.Queries.GetTrainingsList
{
    public class GetTrainingsListQuery : IRequest<ResponseModel<TrainingsListModel>>
    {
        public GetTrainingsListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetTrainingsListQuery() { }

        public Guid TenantId { get; }
        public int? Skip { get; }
        public int? Take { get; }

    }
    public class GetTrainingsListQueryModel : IRequest<ResponseModel<TrainingsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
