namespace Mission6Movie.Models;

public class Categories
{
        public int CategoryId { get; set; }  // Primary key
        public string CategoryName { get; set; }
    
        // This is a navigation property to link the Category to the Movie
        public ICollection<Movie> Movies { get; set; }  // Collection of related movies
    

}