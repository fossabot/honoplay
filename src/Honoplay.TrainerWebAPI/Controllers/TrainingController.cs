using Honoplay.AdminWebAPI.Controllers;
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
        /// <summary>
        /// This service retrieve trainings by trainerUserId
        /// </summary>
        /// <param name="trainerUserId">Get trainings list </param>
        /// <returns>Get trainings by tenant id and trainerUser id with status code.</returns>
        [HttpGet]
        [Route("/api/TrainerUser/{trainerUserId}/Training")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<TrainingsListModel>>> GetByTrainerId(int trainerUserId)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);

                var trainingsListModel = await Mediator.Send(new GetTrainingsListByTrainerUserIdQuery(userId, trainerUserId, tenantId));

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
