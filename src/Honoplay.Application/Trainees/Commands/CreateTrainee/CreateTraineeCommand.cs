using System;
using System.Collections.Generic;
using System.Text;
using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Trainees.Commands.CreateTrainee
{
    public class CreateTraineeCommand : IRequest<ResponseModel<CreateTraineeModel>>
    {
    }
}
