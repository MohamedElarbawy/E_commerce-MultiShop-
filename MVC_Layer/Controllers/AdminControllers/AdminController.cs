using Microsoft.AspNetCore.Mvc;

namespace MVC_Layer.Controllers.AdminControllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
