using Honoplay.Application._Infrastructure;
using Honoplay.Application.Classrooms.Queries.GetClassroomsListByTrainingId;
using Honoplay.Application.Classrooms.Queries.GetClassroomsListByTrainingIdWithTrainerUserId;
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
    public class ClassroomController : BaseController
    {
        /// <summary>
        /// This service retrieve classroom list by trainingId & trainerId
        /// </summary>
        /// <param name="trainingId">Get classrooms</param>
        /// <returns>Get classrooms by tenantId & trainingId & trainerId  with status code.</returns>
        [HttpGet]
        [Route("/Training/{trainingId}/Classroom")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseModel<ClassroomsListByTrainingIdModel>>> GetByTrainingId(int trainingId)
        {
            try
            {
                var tenantId = Guid.Parse(Claims[ClaimTypes.UserData]);
                var trainerUserId = Claims[ClaimTypes.Sid].ToInt();

                var classroomsListModel = await Mediator.Send(new GetClassroomsListByTrainingIdWithTrainerUserIdQuery(tenantId, trainingId, trainerUserId));

                return Ok(classroomsListModel);
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
