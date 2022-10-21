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

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            ViewBag.categories = unitOfWork.Categories.GetAllCategoreis();
            ViewBag.recentProducts = unitOfWork.Products.GetLastAddedProducts(12);
            var products = unitOfWork.Products.GetAllActiveProductsIncludecolors();

            return View(products);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = unitOfWork.Products.GetByIdWithColors(id);
            return View(product);
        }

        public IActionResult GetProductsInCategory(int id)
        {
            var product = unitOfWork.Products.GetProductsInCAtegory(id);
            return View(product);
        }

        public IActionResult Shop(int pageSize, int pageNumber)
        {
            

            var priceRange = HttpContext.Request.Query["price"];
            var colors = HttpContext.Request.Query["color"];
            var totalProducts = unitOfWork.Products.FilterProductsByPrice(priceRange);
            if (colors.Count > 0)
                totalProducts = unitOfWork.Products.FilterProductsByColor(colors);

            var totalItems = totalProducts.Count();
            var pager = new Pager(totalItems, pageNumber, pageSize);
            ViewBag.Pager = pager;


            var products = unitOfWork.Products.GetItemsPerPage(pageNumber, pageSize, totalProducts);

            return View(products);
        }








    }
}