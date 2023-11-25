using Microsoft.AspNetCore.Mvc;

namespace Travel_Agency_Management_System.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
