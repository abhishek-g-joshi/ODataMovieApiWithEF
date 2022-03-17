using ODataMovieApiWithEF.EFCore;
using ODataMovieApiWithEF.Models;

namespace ODataMovieApiWithEF.Repository
{
    public class PersonRepositroy : IPersonRepositroy
    {
        private readonly MovieAppDbContext _db;

        public PersonRepositroy(MovieAppDbContext db)
        {
            _db = db;
        }
        public bool CreatePerson(Person person)
        {
            _db.Person.Add(person);
            return Save();
        }

        public bool DeletePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Person> GetPeople()
        {
            return _db.Person.AsQueryable();
        }

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public bool PersonExists(int id)
        {
            return _db.Person.Any(x => x.PId == id);
        }

        public bool PersonExists(string name)
        {
            var value = _db.Person.Any(y => y.FirstName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool Save()
        {
           return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
