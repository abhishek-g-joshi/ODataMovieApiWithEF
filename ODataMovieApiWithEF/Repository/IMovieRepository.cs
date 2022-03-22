using ODataMovieApiWithEF.Models;

namespace ODataMovieApiWithEF.Repository
{
    public interface IMovieRepository
    {
        IQueryable<Movie> GetMovies();

        Movie GetMovie(int id);

        bool MovieExists(int id);

        bool MovieExists(string title);

        bool CreateMovie(Movie movie);

        bool UpdateMovie(Movie movie);

        bool  DeleteMovie(Movie movie);

        bool Save();
    }
}
