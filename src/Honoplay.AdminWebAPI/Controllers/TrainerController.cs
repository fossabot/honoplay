using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Application.Trainers.Commands.CreateTrainer;
using Honoplay.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Honoplay.AdminWebAPI.Controllers
{
    [Authorize]
    public class TrainerController : BaseController
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<ResponseModel<CreateTrainerModel>>> Post([FromBody]CreateTrainerCommand command)
        {
            try
            {
                var userId = Claims[ClaimTypes.Sid].ToInt();
                command.CreatedBy = userId;

                var model = await Mediator.Send(command);
                return Created($"api/trainer/{model.Items.Single().Name}", model);
            }
            catch (ObjectAlreadyExistsException ex)
            {
                return Conflict(new ResponseModel<CreateTrainerModel>(new Error(HttpStatusCode.Conflict, ex)));
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt());
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
