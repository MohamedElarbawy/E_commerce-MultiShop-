using BusinessLogicLayer;
using CoreLayer;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using MVC_Layer.Models;
using MVC_Layer.Services;
using System.Text.Json;
namespace MVC_Layer.Controllers
{
    public class CartController : Controller
    {

        public CartController(IUnitOfWork unitOfWork, MultiShopContext context)
        {
            this.unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork unitOfWork;

        public IActionResult Cart()
        {
            HangFireActions action = new(unitOfWork);
            RecurringJob.AddOrUpdate(() => action.DeleteDiscountCodeAfterExpired(), Cron.Daily(0, 1));

            return View();
        }


        public JsonResult getTotalPrice(string sessionStorageItems)
        {
            ProductIdViewModel[]? ObjectsFromJason = JsonSerializer.Deserialize<ProductIdViewModel[]>(sessionStorageItems);
          
            Dictionary<int, int> productIdAndCount = new();
            if (ObjectsFromJason != null && ObjectsFromJason.Length > 0)
                foreach (var item in ObjectsFromJason)
                    if (item.id != null && item.count != 0)
                        productIdAndCount.Add(int.Parse(item.id), item.count);

            List<int> productsIds = new();
            productsIds = productIdAndCount.Keys.ToList();
            var products = unitOfWork.Products.GetAllProductsWithIds(productsIds);
            double? totalPrice = 0;
            foreach (var item in products)
                if (item.ProductPrice != null)
                    totalPrice += productIdAndCount[item.Id] * item.ProductPrice;

            return Json(totalPrice);
        }



        public JsonResult getDiscount(string code)
        {
            double dicount = 0;
            if (code != null)
                dicount = unitOfWork.Discounts.GetAllThatMatchesACriteria(d => d.CouponCode == code).Select(d => d.DiscountPercent).FirstOrDefault();
            if (dicount > 0 && dicount < 100)
                return Json(dicount);

            return Json(0);

        }


    }
}
