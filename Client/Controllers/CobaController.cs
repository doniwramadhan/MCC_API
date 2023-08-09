using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class CobaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }

        public IActionResult AgeChart()
        {
            return View();
        }
    }
}
