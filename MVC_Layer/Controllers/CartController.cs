using CoreLayer;
using Microsoft.AspNetCore.Mvc;
using MVC_Layer.Models;
using System.Text.Json;
namespace MVC_Layer.Controllers
{
    public class CartController : Controller
    {

        public CartController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        List<int> _items = new List<int>();
        private readonly IUnitOfWork unitOfWork;

        //[HttpPost]
        //public void GetLocalStorage(string items)
        //{

        //    ProductIdViewModel[]? ObjectsFromJason = JsonSerializer.Deserialize<ProductIdViewModel[]>(items);
        //    if(ObjectsFromJason != null)
        //    foreach (var item in ObjectsFromJason)
        //    {
        //        _items.Add(int.Parse(item.productId));
        //    }

            

        //}

        public IActionResult Cart()
        {
            
            var products = unitOfWork.Products.GetAllProductsWithIds(_items);
            return View(products);
        }

    }
}
