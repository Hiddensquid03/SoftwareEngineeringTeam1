using Microsoft.AspNetCore.Mvc;

namespace SEWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: / or /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Looks for Views/Home/Index.cshtml
        }

        // POST: /Home/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Simple validation check
            if ((username == "admin" && password == "password") ||
                (username == "user" && password == "userpass"))
            {
                // Successful login -> Redirects to your Privacy page
                return RedirectToAction("Index", "Staff");
            }

            // Failed login -> Pass error via ViewBag
            ViewBag.Error = "Invalid username or password.";
            return View("Index");
        }
    }
}