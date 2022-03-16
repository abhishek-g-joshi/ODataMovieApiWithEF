using System.ComponentModel.DataAnnotations;

namespace ODataMovieApiWithEF.Models
{
    public class Person
    {
        [Key]
        public int Pid { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
