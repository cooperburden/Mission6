using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission6Movie.Models;

namespace Mission6Movie.Controllers;

public class HomeController : Controller
{
    private MovieContext _context;
    
    // Constructor to inject MovieContext
    public HomeController(MovieContext temp)
    {
        _context = temp;
    }

    // GET: Show the Movie Creation Form
    public IActionResult Index()
    {
        return View();
    }

    // GET: The form where users can input movie data
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Handle the form submission and save movie data
    [HttpPost]
    public IActionResult Create(Movie movie)
    {
        if (ModelState.IsValid)  // Check if the model is valid
        {
            Console.WriteLine("Saving movie: " + movie.Title); // Debugging line
            
            _context.Movies.Add(movie);  // Add the movie to the database
            _context.SaveChanges();      // Save the changes

            return View("Confirmation", movie);  // Redirect to the confirmation page with the movie
        }

        return View();  // If the model is invalid, return the Create view to show validation errors
    }

    // Display confirmation after movie is added
    public IActionResult Confirmation(Movie movie)
    {
        return View(movie);  // Display the movie details in the confirmation view
    }
    
    // HomeController.cs

    public IActionResult GetToKnowJoel()
    {
        return View();
    }
}


