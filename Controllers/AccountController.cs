using Microsoft.AspNetCore.Mvc;
using MovieProject.Models;
using MovieProject.Services;
using Microsoft.AspNetCore.Http;

public class AccountController : Controller
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult SignUp() => View();

    [HttpPost]
    public IActionResult SignUp(User user)
    {
        if (ModelState.IsValid)
        {
            _userService.Register(user);
            return RedirectToAction("SignIn");
        }
        return View(user);
    }

    public IActionResult SignIn() => View();

    [HttpPost]
    public IActionResult SignIn(string username, string password)
    {
        var user = _userService.Authenticate(username, password);
        if (user != null)
        {
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Invalid credentials";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("SignIn");
    }
}
