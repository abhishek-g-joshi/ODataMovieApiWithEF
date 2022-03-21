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
    public class MoviesController : ODataController
    {
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
        /// Create a new movie
        /// </summary>
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Movie movie)
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
        [HttpPut("{movieId:int}")]
        public IActionResult Update(int movieId, [FromBody] Movie movie)
        {
            if (movie == null || movieId != movie.Id)
                return BadRequest(ModelState);

            if (!_movieRepo.UpdateMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong while updating movie : {movie.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Update a movie
        /// </summary>
        /// <return></return>
        [EnableQuery]
        [HttpDelete("{movieId:int}")]
        public IActionResult Delete(int movieId, [FromBody] Movie movie)
        {
            if (movie == null || movieId != movie.Id)
                return BadRequest(ModelState);

            if (!_movieRepo.DeleteMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting movie : {movie.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
