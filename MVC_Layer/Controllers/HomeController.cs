using CoreLayer;
using CoreLayer.Interfaces;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using MVC_Layer.Models;
using System.Diagnostics;
using BusinessLogicLayer.Helper;

namespace MVC_Layer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        //[FromQuery]
        //public List<string> color { get; set; }
        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
         
        }
        public IActionResult Index()
        {
            ViewBag.categories = unitOfWork.Categories.GetAllCategoreis();
           var products= unitOfWork.Products.GetAll();
            return View(products);
        }
       public IActionResult ProductDetails(int id)
        {
            var product = unitOfWork.Products.GetById(id);
            return View(product);
        }
        public IActionResult GetProductsInCategory(int id)
        {
            var product = unitOfWork.Products.GetProductsInCAtegory(id);
            return View(product);
        }
        public IActionResult Shop(int pageSize, int pageNumber = 1)
        {
           //ViewBag.x= HttpContext.Request.Query["color"];
            
           
            int totalItems = unitOfWork.Products.NumberOfItems();

            var pager = new Pager(totalItems, pageNumber, pageSize);

            ViewBag.Pager=pager;
            var products = unitOfWork.Products.GetItemsPerPage(pageNumber, pageSize);
            return View(products);
        }















        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}