using BusinessLogicLayer.Helper;
using CoreLayer;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using MVC_Layer.Models;

namespace MVC_Layer.Controllers.AdminControllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DashBoard()
        {
          var products=  unitOfWork.Products.GetAll();
            return View(products);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
           
            return View();
        }
        
        [HttpPost]
        public IActionResult AddProduct(Product pr)
        {
            unitOfWork.Products.Add(pr);
            pr.ImgName =UploadFile.SaveFile(pr.ImgUrl,"img");
            unitOfWork.Complete();
            return RedirectToAction("DashBoard");
        }
    }
}
