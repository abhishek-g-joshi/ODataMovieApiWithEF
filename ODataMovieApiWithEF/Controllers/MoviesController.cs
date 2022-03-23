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
   private readonly IMovieRepository _movieRepo;
        public MoviesController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        /// <summary>
        /// Get list of all Movies
        /// </summary>
        [EnableQuery]
        public IQueryable Get()
        {
            return _movieRepo.GetMovies();
        }

        /// <summary>
        /// Get individual Movie
        /// </summary>
        /// <param name="movieId">The Id of the movie</param>
        /// <returns></returns>
      
        [HttpGet(nameof(GetById))]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [EnableQuery]
        //[ODataRoute("{movieId}")]
        public IActionResult GetById(int movieId)
        {
            var obj = _movieRepo.GetMovie(movieId);

            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        /// <summary>
        /// Create a new movie
        /// </summary>
        [ProducesResponseType(201, Type = typeof(Movie))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableQuery]
        public IActionResult Post([FromBody] Movie movie)
        {
            if (movie == null)
                return BadRequest(ModelState);
            if (_movieRepo.MovieExists(movie.Title))
            {
                ModelState.AddModelError("", "Movie already Exist");
                return StatusCode(500, ModelState);
            }

            if (!_movieRepo.CreateMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong while saving movie record of {movie.Title}");
                return StatusCode(500, ModelState);
            }

            return Created(movie);

        }

        /// <summary>
        /// Update a movie
        /// </summary>
        /// <return></return>
        [EnableQuery]
        [ODataRoute]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Patch([FromODataUri] int movieId, [FromBody] Movie movie)
        {
            if (movie == null || movieId != movie.Id)
                return BadRequest(ModelState);

            if (!_movieRepo.UpdateMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong while updating movie : {movie.Title}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        /// <summary>
        /// Update a movie
        /// </summary>
        /// <return></return> 
        [HttpDelete("{movieId:int}")]
        [EnableQuery]
       // [ODataRoute()]
       // [ODataRoute("v1/Movies({movieId:int})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Delete(int movieId)
        {
            if (!_movieRepo.MovieExists(movieId))
            {
                return NotFound();
            }

            var movieobj = _movieRepo.GetMovie(movieId);

            if (!_movieRepo.DeleteMovie(movieobj))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting movie : {movieobj.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
