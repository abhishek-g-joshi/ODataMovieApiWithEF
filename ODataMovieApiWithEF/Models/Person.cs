using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODataMovieApiWithEF.Models
{
    public class Person
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int PId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
