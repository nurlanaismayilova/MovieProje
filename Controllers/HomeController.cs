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
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            return RedirectToAction("SignIn", "Account");

        return RedirectToAction("Movies");
    }

    public IActionResult Movies()
    {
        if (!IsLoggedIn()) return RedirectToAction("SignIn", "Account");
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

    public IActionResult Search(string query, string entity)
    {
        if (string.IsNullOrWhiteSpace(entity))
            return RedirectToAction("Movies");

        // If query is empty, redirect back to the original list page for that entity
        if (string.IsNullOrWhiteSpace(query))
        {
            switch (entity.ToLower())
            {
                case "movies":
                    return RedirectToAction("Movies");
                case "actors":
                    return RedirectToAction("Actors");
                case "ratings":
                    return RedirectToAction("Ratings");
                case "genres":
                    return RedirectToAction("Genres");
                case "movieactors":
                    return RedirectToAction("MovieActors");
                default:
                    return RedirectToAction("Movies");
            }
        }

        switch (entity.ToLower())
        {
            case "movies":
                var movies = _movieService.SearchMovies(query);
                return View("Movies", movies);

            case "actors":
                var actors = _movieService.SearchActors(query);
                return View("Actors", actors);

            case "ratings":
                var ratings = _movieService.SearchRatings(query);
                return View("Ratings", ratings);

            case "genres":
                var genres = _movieService.SearchGenres(query);
                return View("Genres", genres);

            case "movieactors":
                var movieActors = _movieService.SearchMovieActors(query);
                return View("MovieActors", movieActors);

            default:
                return RedirectToAction("Movies");
        }
    }



    private bool IsLoggedIn()
    {
        return !string.IsNullOrEmpty(HttpContext.Session.GetString("Username"));
    }


}

