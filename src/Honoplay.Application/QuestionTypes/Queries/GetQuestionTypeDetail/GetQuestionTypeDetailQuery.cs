﻿using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.QuestionTypes.Queries.GetQuestionTypeDetail
{
    public class GetQuestionTypeDetailQuery : IRequest<ResponseModel<QuestionTypeDetailModel>>
    {
        public GetQuestionTypeDetailQuery(int id)
        {
            Id = id;
        }

        public GetQuestionTypeDetailQuery() { }

        public int Id { get; private set; }
    }
}