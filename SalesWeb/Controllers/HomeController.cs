using Microsoft.AspNetCore.Mvc;

namespace SalesWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}