using Microsoft.AspNetCore.Mvc;

namespace KKDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }public IActionResult Blog1()
        {
            return View();
        }
    }
}
