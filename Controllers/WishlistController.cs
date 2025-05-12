using Microsoft.AspNetCore.Mvc;
using MovieProject.Models;
using MovieProject.Services;

namespace MovieProject.Controllers
{
    public class WishlistController : Controller
    {
        private readonly MovieService _movieService;

        public WishlistController(MovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            var wishlist = _movieService.GetAllWishlist();
            return View(wishlist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWishlist(Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                _movieService.AddWishlist(wishlist);
                return RedirectToAction("Index");
            }

            var wishlistItems = _movieService.GetAllWishlist();
            return View("Index", wishlistItems);
        }

        // GET: Wishlist/Edit/{id}
        public IActionResult Edit(int id)
        {
            var item = _movieService.GetWishlistById(id);
            if (item == null)
            {
                return NotFound();  // Return 404 if item not found
            }
            return View(item);  // Show the edit form
        }

        // POST: Wishlist/Edit/{id}
        [HttpPost]
        public IActionResult Edit(Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                _movieService.UpdateWishlist(wishlist);  // Update the wishlist in the database
                return RedirectToAction("Index");  // Redirect to the index page to show the updated list
            }

            return View(wishlist);  // If the model is invalid, return to the edit view with the current data
        }



        public IActionResult Delete(int id)
        {
            var wishlist = _movieService.GetWishlistById(id);
            if (wishlist == null)
            {
                return NotFound();
            }
            return View(wishlist);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _movieService.DeleteWishlist(id);
            return RedirectToAction("Index");
        }

    }

}
