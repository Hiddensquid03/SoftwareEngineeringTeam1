using Microsoft.AspNetCore.Mvc;

namespace SEWebApp.Controllers
    {
        public class HomeController : Controller
        {
            [HttpGet]
            public IActionResult Index()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Login(string username, string password)
            {
                // Admin login
                if (username == "admin" && password == "password")
                {
                    return RedirectToAction("Index", "Staff");
                }

                // Customer login
                else if (username == "customer" && password == "password")
                {
                    return RedirectToAction("Index", "Customer");
                }

                // Failed login
                ViewBag.Error = "Invalid username or password.";
                return View("Index");
            }
        }

    }
    
