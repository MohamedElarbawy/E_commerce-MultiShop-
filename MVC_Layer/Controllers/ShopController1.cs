using Microsoft.AspNetCore.Mvc;

namespace MVC_Layer.Controllers
{
    public class ShopController1 : Controller
    {
        public IActionResult Filter()
        {
            return View();
        }
    }
}
