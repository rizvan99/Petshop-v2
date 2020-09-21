using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop2020.Core.Application_Service;
using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Petshop2020.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET: api/<OwnersController>
        [HttpGet]
        public ActionResult<FilteredList<Owner>> Get([FromQuery] FilterSearch filter)
        {
            return _ownerService.GetAllOwners(filter);
        }

        // GET api/<OwnersController>/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            var owner = _ownerService.FindOwnerByIdIncludePets(id);

            if (id <= 0)
            {
                return BadRequest("Id must be greater than 0");
            }

            if(owner == null)
            {
                return StatusCode(404, $"Owner with id {id} not found");
            }

            return StatusCode(200, owner);

        }

        // POST api/<OwnersController>
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            if (owner.FirstName == null ||
                owner.LastName == null)
            {
                return BadRequest("Please specify firstname and lastname");
            }

            //Check to see if 8 digits long
            if (owner.PhoneNumber == 0)
            {
                return BadRequest("Please specify a phone number for contact purposes");
            }

            return StatusCode(201, _ownerService.CreateOwner(owner));
        }

        // PUT api/<OwnersController>/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner owner)
        {
            if (id < 0 || id != owner.Id)
            {
                return BadRequest($"Owner with id {id} not found");
            }

            return StatusCode(202, _ownerService.UpdateOwner(owner));
        }

        // DELETE api/<OwnersController>/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            var owner = _ownerService.DeleteOwner(id);

            if (owner == null)
            {
                return StatusCode(404, $"Owner with id {id} not found");
            }

            return Ok($"Owner deleted with id {id} ");
        }
    }
}
