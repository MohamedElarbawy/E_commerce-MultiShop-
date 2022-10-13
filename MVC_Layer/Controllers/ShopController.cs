using CoreLayer;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Layer.Controllers
{
    public class ShopController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ShopController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public JsonResult Filter(int min,int max)
        {
            var result= unitOfWork.Products.FilterProductsByPrice(min,max);
            return Json(new { result });
        }
    }
}
