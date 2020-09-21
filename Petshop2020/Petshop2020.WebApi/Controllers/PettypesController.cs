using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop2020.Core.Application_Service;
using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Petshop2020.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PettypesController : ControllerBase
    {
        private readonly IPetTypeService _typeService;

        public PettypesController(IPetTypeService typeService)
        {
            _typeService = typeService;
        }

        // GET: api/<PettypeController>
        [HttpGet]
        public ActionResult<FilteredList<PetType>> Get([FromQuery] FilterSearch filter)
        {
            return _typeService.GetAllTypes(filter);
        }

        // GET api/<PettypeController>/5
        [HttpGet("{id}")]
        public ActionResult<PetType> Get(int id)
        {
            var type = _typeService.FindTypeByIdIncludePets(id);

            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            if (type == null)
            {
                return StatusCode(404, $"Pet type with id {id} not found");
            }

            return StatusCode(200, type);
        }

        // POST api/<PettypeController>
        [HttpPost]
        public ActionResult<PetType> Post([FromBody] PetType type)
        {
            if (type.Type == null)
            {
                return BadRequest("Please specify type");
            }

            return _typeService.CreateType(type);
        }

        // PUT api/<PettypeController>/5
        [HttpPut("{id}")]
        public ActionResult<PetType> Put(int id, [FromBody] PetType type)
        {
            if (id < 0 || id != type.Id)
            {
                return BadRequest($"Owner with id {id} not found");
            }

            return StatusCode(202, _typeService.UpdateType(type));
        }

        // DELETE api/<PettypeController>/5
        [HttpDelete("{id}")]
        public ActionResult<PetType> Delete(int id)
        {
            var type = _typeService.DeleteType(id);

            if (type == null)
            {
                return StatusCode(404, $"Type with id {id} not found");
            }

            return StatusCode(202,type);
        }
    }
}
