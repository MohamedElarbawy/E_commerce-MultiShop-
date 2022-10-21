using BusinessLogicLayer;
using CoreLayer;
using Microsoft.AspNetCore.Mvc;
using MVC_Layer.Models;
using System.Text.Json;
namespace MVC_Layer.Controllers
{
    public class CartController : Controller
    {

        public CartController(IUnitOfWork unitOfWork, MultiShopContext context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        private readonly IUnitOfWork unitOfWork;
        private readonly MultiShopContext context;

        public IActionResult Cart()
        {
            return View();
        }


        public JsonResult getTotalPrice(string localStorageItems)
        {
            List<int> productsIds = new();
            Dictionary<int, int> productIdAndCount = new();
            ProductIdViewModel[]? ObjectsFromJason = JsonSerializer.Deserialize<ProductIdViewModel[]>(localStorageItems);
            if (ObjectsFromJason != null && ObjectsFromJason.Length > 0)
                foreach (var item in ObjectsFromJason)
                {
                    if (item.id != null && item.count != 0)
                        productIdAndCount.Add(int.Parse(item.id), item.count);

                }
            productsIds = productIdAndCount.Keys.ToList();
            var products = unitOfWork.Products.GetAllProductsWithIds(productsIds);
            double? totalPrice = 0;
            foreach (var item in products)
            {
                if (item.ProductPrice != null)
                    totalPrice += productIdAndCount[item.Id] * item.ProductPrice;
            }

            return Json(totalPrice);
        }



        public JsonResult getDiscount(string code)
        {
            double dicount = 0;
            if (code != null)
                dicount = context.Discounts.Where(d => d.CouponCode == code).Select(d => d.DiscountPercent).FirstOrDefault();
            if (dicount > 0 && dicount < 100)
                return Json(dicount);

            return Json(0.0);

        }


    }
}
