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
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        // GET: api/<PetsController>
        [HttpGet]
        public ActionResult<FilteredList<Pet>> Get([FromQuery] FilterSearch filter)
        {
            return _petService.GetAllPets(filter);
        }

        // GET api/<PetsController>/5
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            var pet = _petService.FindPetByIdIncludeOwnersAndTypes(id);

            if (id <= 0)
            {
                return BadRequest("Id must be greater than 0");
            }

            if (pet == null)
            {
                return StatusCode(404, $"Pet not found with id {id}");
            }

            return StatusCode(200, pet);

        }

        // POST api/<PetsController>
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            try
            {
                if (pet.Name == null)
                {
                    return BadRequest("Must specify a name");
                }

                if (pet.Type == null)
                {
                    return BadRequest("Must specifcy a type");
                }

                if (pet.Price <= 0)
                {
                    return BadRequest("Must specify valid price");
                }

                return StatusCode(201, _petService.CreatePet(pet));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // PUT api/<PetsController>/5
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet pet)
        {
            if (id < 0 || id != pet.Id)
            {
                return BadRequest($"Pet with id {id} not found");
            }

            return _petService.UpdatePet(pet);
        }

        // DELETE api/<PetsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            var pet = _petService.DeletePet(id);

            if (pet == null)
            {
                return StatusCode(404, "Pet not found with id " + id);
            }

            return Ok($"Pet deleted with id {id}");


        }
    }
}
