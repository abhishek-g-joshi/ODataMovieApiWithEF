using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataMovieApiWithEF.Models;
using ODataMovieApiWithEF.Repository;

namespace ODataMovieApiWithEF.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]*/
    public class PersonController : ODataController
    {
        private readonly IPersonRepositroy _pRepo;

        public PersonController(IPersonRepositroy pRepo)
        {
            _pRepo = pRepo;
        }

        /// <summary>
        /// Get list of all Person
        /// </summary>
        /// <return></return>
        [EnableQuery]
        public IQueryable Get()
        {
            return _pRepo.GetPeople();
        }

        /// <summary>
        /// Create new person
        /// </summary>
        /// <return></return>
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if(person == null)
            {
                return BadRequest(ModelState);
            }
            if(_pRepo.PersonExists(person.Id))
            {
                ModelState.AddModelError("", "Person already exist");
                return StatusCode(500,ModelState);
            }
            if (!_pRepo.CreatePerson(person))
            {
                ModelState.AddModelError("", $"Something went wrong while saving person record of {person.FirstName} {person.LastName}");
                return StatusCode(500, ModelState);
            }

            return Created(person);
        }
    }
}
