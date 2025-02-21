using System.ComponentModel.DataAnnotations;

namespace Mission6Movie.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public int CategoryId { get; set; }  // This links to the Category table
        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }  // Rating can be null
        public int? Edited { get; set; }  // Nullable int for Edited
        public string LentTo { get; set; }
        public int? CopiedToPlex { get; set; }  // Nullable int for CopiedToPlex
        public string Notes { get; set; }

        public Categories Categories { get; set; }  // Navigation property for Category table
    }
}