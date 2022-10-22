using BusinessLogicLayer;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Layer.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly MultiShopContext context;

        public CheckOutController(MultiShopContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            Order o1 = new();
            o1.Status = "waiting";
            o1.TotalPrice = 1254;
            o1.UserId = 1;

            context.Orders.Add(o1);
            context.SaveChanges();
            return View();
        }
    }
}
