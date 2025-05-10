using Microsoft.AspNetCore.Mvc;
using MovieProject.Models;
using MovieProject.Services;

namespace MovieProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieService _movieService;

        public HomeController(MovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            List<Movie> movies = _movieService.GetAllMovies();
            return View(movies);
        }
    }
}
