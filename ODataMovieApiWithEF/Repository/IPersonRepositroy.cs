using ODataMovieApiWithEF.Models;

namespace ODataMovieApiWithEF.Repository
{
    public interface IPersonRepositroy
    {
        IQueryable<Person> GetPeople();

        Person GetPerson(int id);
        bool PersonExists(int id);

        bool PersonExists(string name);

        bool CreatePerson(Person person);

        bool UpdatePerson(Person person);

        bool DeletePerson(Person person);

        bool Save();

    }
}
