using Microsoft.AspNetCore.Mvc;
using MovieProject.Services;

public class HomeController : Controller
{
    private readonly MovieService _movieService;

    public HomeController(MovieService movieService)
    {
        _movieService = movieService;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Movies");
    }

    public IActionResult Movies()
    {
        var movies = _movieService.GetAllMovies();
        return View(movies);
    }

    public IActionResult Actors()
    {
        var actors = _movieService.GetAllActors();
        return View(actors);
    }

    public IActionResult Ratings()
    {
        var ratings = _movieService.GetAllRatings();
        return View(ratings);
    }

    public IActionResult Genres()
    {
        var genres = _movieService.GetAllGenres();
        return View(genres);
    }
}

