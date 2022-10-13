using BusinessLogicLayer.Helper;
using CoreLayer;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Layer.Models;

namespace MVC_Layer.Controllers.AdminControllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
      

        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
           
        }
       
        
        public IActionResult Index()
        {
            var products = unitOfWork.Products.GetAll();
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
            pr.ImgName = UploadFile.SaveFile(pr.ImgUrl, "img");
            unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public IActionResult SoftDelete(int id)
        {
            unitOfWork.Products.ChangeActiveStateToFalse(id);
            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public IActionResult ShowProduct(int id)
        {
            unitOfWork.Products.ChangeActiveStateToTrue(id);
            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

    }
}
