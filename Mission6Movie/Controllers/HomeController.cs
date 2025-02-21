using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Mission6Movie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mission6Movie.Controllers;

public class HomeController : Controller
{
    private MovieContext _context;

    // Constructor to inject MovieContext
    public HomeController(MovieContext temp)
    {
        _context = temp;
    }

    // GET: Show the Movie List
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies
            .Include(m => m.Categories)
            .Select(m => new Movie
            {
                MovieId = m.MovieId,
                CategoryId = m.CategoryId,
                Title = m.Title ?? "Unknown Title",  
                Year = m.Year,  
                Director = m.Director ?? "Unknown Director",  
                Rating = m.Rating ?? "Unrated",  
                Edited = m.Edited,  
                LentTo = m.LentTo ?? "Not Lent",  
                CopiedToPlex = m.CopiedToPlex,  
                Notes = m.Notes ?? ""  // Empty string for Notes instead of NULL
            })
            .ToListAsync();

        return View(movies);
    }

    
    
    
    
    
    
    // GET: Create or Edit Movie
    [HttpGet]
    public IActionResult Create(int? id)
    {
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");  // Pass categories to the view

        if (id == null)
        {
            return View(new Movie());  // If there's no id, this is for creating a new movie
        }

        var movieToEdit = _context.Movies.SingleOrDefault(m => m.MovieId == id);

        if (movieToEdit == null)
        {
            return NotFound();  // If the movie isn't found, return an error
        }

        return View(movieToEdit);  // If it's an edit, pass the movie to the view
    }

    
    

    [HttpPost]
    public IActionResult Create(Movie movie)
    {
        // Re-populate categories so they appear in the dropdown after form submission
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");

        // Check if the model is valid
        if (ModelState.IsValid)
        {
            // Add the movie to the database
            _context.Movies.Add(movie);
     
            // Save the changes to the database
            _context.SaveChanges();
     
            // Return the Confirmation view with the created movie
            return View("Confirmation", movie);
        }

        // If the model is not valid, return to the Create view with the existing model
        return View(movie);
    }



    
    
    
    
    
    // GET: Edit Movie
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var movieToEdit = _context.Movies
            .SingleOrDefault(x => x.MovieId == id);  // Find the movie by ID

        if (movieToEdit == null)
        {
            return NotFound();  // If the movie is not found, return 404
        }

        return View("Create", movieToEdit);  // Return the Create view with the movie data for editing
    }

// POST: Edit Movie
    [HttpPost]
    public IActionResult Edit(Movie updatedMovie)
    {
        if (ModelState.IsValid)  // Ensure the model is valid before saving
        {
            // Handle nullable values here, if needed
            updatedMovie.Rating = updatedMovie.Rating ?? "Unrated";  // Default to "Unrated" if Rating is null
            updatedMovie.LentTo = updatedMovie.LentTo ?? "Not Lent";  // Default to "Not Lent" if LentTo is null
            updatedMovie.Notes = updatedMovie.Notes ?? "";  // Default to empty string if Notes is null

            // Update the movie in the database
            _context.Update(updatedMovie);
            _context.SaveChanges(); // Save the changes

            return RedirectToAction("Index"); // Redirect to the Index page after saving
        }

        return View(updatedMovie); // If model is invalid, return the view with the updated movie object
    }





    
    
    
    
    
    
    
    
    
    
    

    
    
    
    // Display confirmation after movie is added
    public IActionResult Confirmation(Movie movie)
    {
        return View(movie);  // Display the movie details in the confirmation view
    }
    
    
    // GET: Confirm Deletion
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var movie = _context.Movies.SingleOrDefault(m => m.MovieId == id);
    
        if (movie == null)
        {
            return NotFound(); // Movie not found
        }

        return View(movie); // Show the movie details in a confirmation view
    }

// POST: Delete the movie
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var movie = _context.Movies.SingleOrDefault(m => m.MovieId == id);

        if (movie == null)
        {
            return NotFound(); // Movie not found
        }

        _context.Movies.Remove(movie);  // Remove the movie from the database
        _context.SaveChanges();         // Save the changes to the database

        return RedirectToAction("Index"); // Redirect to the movie list page
    }

    
    
    
    
    
    
    

    // Example of another controller action (if needed)
    public IActionResult GetToKnowJoel()
    {
        return View();
    }
}
