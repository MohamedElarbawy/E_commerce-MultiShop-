using BusinessLogicLayer.Helper;
using CoreLayer;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        public IActionResult Index(int pageSize, int pageNumber)
        {
            var totalProducts = unitOfWork.Products.GetProductsIncludeCategoryNColors();

            var totalItems = totalProducts.Count();
            ViewBag.TotalProducts = totalItems;
            var pager = new Pager(totalItems, pageNumber, pageSize);
            ViewBag.Pager = pager;

            var products = unitOfWork.Products.GetItemsPerPage(pageNumber, pageSize, totalProducts);

            return View(products);
        }


        public IActionResult Details(int id)
        {
            var product = unitOfWork.Products.GetById(id);
            return View(product);
        }










        [HttpGet]
        public IActionResult AddProduct()
        {
            List<SelectListItem> categoryList = unitOfWork.Categories.GetAll().Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()

            }).ToList();

            List<SelectListItem> colorList = unitOfWork.Colors.GetAll().Select(c => new SelectListItem
            {
                Text = c.ColorName,
                Value = c.Id.ToString()

            }).ToList();

            ViewBag.categories = categoryList;
            ViewBag.colors = colorList;
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product entity, List<int> colorIds)
        {
            if (!ModelState.IsValid)
                return View(entity);

            var colors = unitOfWork.Colors.GetColors(colorIds);
            entity.Colors = (ICollection<Color>)colors;
            unitOfWork.Products.Add(entity);
            entity.ImgName = UploadFile.SaveFile(entity.ImgUrl, "img");
            unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            List<SelectListItem> categoryList = unitOfWork.Categories.GetAll().Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()

            }).ToList();
            ViewBag.categories = categoryList;
            List<SelectListItem> colorList = unitOfWork.Colors.GetAll().Select(c => new SelectListItem
            {
                Text = c.ColorName,
                Value = c.Id.ToString()

            }).ToList();
            ViewBag.colors = colorList;

            var product = unitOfWork.Products.GetById(id);
            return View(product);

        }


        [HttpPost]
        public IActionResult ConfirmEdit(Product entity, List<int> colorIds)
        {
            if (!ModelState.IsValid)
                return View(entity);
             var product = unitOfWork.Products.GetProductWithItsRelatedColors(entity.Id);
            if (colorIds.Any())
            {
                var colors = unitOfWork.Colors.GetColors(colorIds);
               
                product.Colors = (ICollection<Color>)colors;
            }
            if(entity.ImgUrl!=null)
            product.ImgName = UploadFile.SaveFile(entity.ImgUrl, "img");

            unitOfWork.Products.Edit(entity, product);
            unitOfWork.Complete();
            return RedirectToAction("Index");
        }



      
        public IActionResult Delete(int id)
        {
          var deletedProduct=  unitOfWork.Products.GetById(id);
            if (deletedProduct.ImgName != null)
                UploadFile.RemoveFile("img", deletedProduct.ImgName);
            unitOfWork.Products.Delete(id);
            unitOfWork.Complete();
            return RedirectToAction("Index");
        }


        public IActionResult SoftDelete(int id)
        {
           
            unitOfWork.Products.ChangeActiveState(id);
            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public IActionResult ShowProduct(int id)
        {
            unitOfWork.Products.ChangeActiveState(id);
            unitOfWork.Complete();

            return RedirectToAction("Index");
        }

    }
}
