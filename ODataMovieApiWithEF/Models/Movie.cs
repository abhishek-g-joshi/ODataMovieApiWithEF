using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODataMovieApiWithEF.Models
{
    public class Movie
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Required]
        public int PId { get; set; }

        [ForeignKey("PId")]
        public Person? Director { get; set; }




    }
}
