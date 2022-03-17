﻿using ODataMovieApiWithEF.EFCore;
using ODataMovieApiWithEF.Models;

namespace ODataMovieApiWithEF.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieAppDbContext _db;

        public MovieRepository(MovieAppDbContext db)
        {
            _db = db;
        }
        public bool CreateMovie(Movie movie)
        {
            _db.Movies.Add(movie);
            return Save();
        }

        public bool DeleteMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Movie> GetMovies()
        {
            return _db.Movies.AsQueryable();
            
        }

        public bool Save()
        {
           return _db.SaveChanges() >=0 ? true : false;
        }

        public bool UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
        
        public bool MovieExists(int id)
        {
            return _db.Movies.Any(x => x.Id == id);
        }

        public Movie GetMovie(int id)
        {
            throw new NotImplementedException();
        }

        public bool MovieExists(string title)
        {
            bool value  = _db.Movies.Any(y => y.Title.ToLower().Trim() == title.ToLower().Trim());
            return value;
        }
    }
}
