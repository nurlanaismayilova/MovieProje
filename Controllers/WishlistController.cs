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
            var wishlist = new Wishlist();
            return View(wishlist);
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

        public IActionResult Edit(int id)
        {
            var item = _movieService.GetWishlistById(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                _movieService.UpdateWishlist(wishlist);
                return RedirectToAction("Index");
            }

            return View(wishlist);
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
