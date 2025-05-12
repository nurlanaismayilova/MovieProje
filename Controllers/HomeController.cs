using Microsoft.AspNetCore.Mvc;
using MovieProject.Models;
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
    public IActionResult MovieActors()
    {
        var data = _movieService.GetAllMovieActors();
        return View(data);
    }

    [HttpPost]
    public IActionResult AddWishlist(Wishlist wishlist)
    {
        if (ModelState.IsValid)
        {
            _movieService.AddWishlist(wishlist);
            return RedirectToAction("Wishlist");
        }
        return View("Wishlist", _movieService.GetAllWishlist());
    }

    public IActionResult Search(string query)
    {
        var results = _movieService.Search(query);
        return View("SearchResults", results);
    }



}

