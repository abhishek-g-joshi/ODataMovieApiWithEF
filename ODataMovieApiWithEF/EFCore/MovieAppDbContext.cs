using Microsoft.EntityFrameworkCore;
using ODataMovieApiWithEF.Models;

namespace ODataMovieApiWithEF.EFCore
{
    public class MovieAppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options) : base(options)
        {

        }
    }
}
