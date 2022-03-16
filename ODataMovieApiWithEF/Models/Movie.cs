using System.ComponentModel.DataAnnotations;

namespace ODataMovieApiWithEF.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public Person? Director { get; set; }

        
    }
}
