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

        public IActionResult ShopAjax()
        {
            return View();
        }

        public JsonResult getAll()
        {
            var products = unitOfWork.Products.GetAllActiveProducts();
            return Json(products);
        }
        public JsonResult Filter(string colorQuery,string priceQuery)
        {
            var filteredWithColors = unitOfWork.Products.FilterProductsByColor(colorQuery);
            var filterdWithPrice = unitOfWork.Products.FilterProductsByPrice(priceQuery);
             
           var result = filterdWithPrice.Intersect(filteredWithColors).ToList();
       
            return Json(result);


        }
    }
}
