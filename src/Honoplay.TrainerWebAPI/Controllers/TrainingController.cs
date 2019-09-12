using Honoplay.Application._Infrastructure;
using Honoplay.Application.Trainings.Queries.GetTrainingsList;
using Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainerUserId;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Honoplay.TrainerWebAPI.Controllers
{
    [Authorize]
    public class TrainingController : BaseController
    {
        [HttpGet]
        //[Route("/Training")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainingsListModel>>> Get()
        {
            try
            {
                var trainerUserId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var trainingsListModel = await Mediator.Send(new GetTrainingsListByTrainerUserIdQuery(trainerUserId, tenantId));

                return Ok(trainingsListModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }
    }
}
