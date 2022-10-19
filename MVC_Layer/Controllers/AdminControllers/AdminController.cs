﻿using BusinessLogicLayer;
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
        private readonly MultiShopContext context;

        public AdminController(IUnitOfWork unitOfWork,MultiShopContext context )
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
        }


        public IActionResult Index(int pageSize, int pageNumber)
        {
            var totalProducts = unitOfWork.Products.GetAllProductsWithCategory();

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
            List<SelectListItem> categoryListItems = unitOfWork.Categories.GetAll().Select(a => new SelectListItem
            {
                Text = a.CategoryName,
                Value = a.Id.ToString()
            }).ToList();
            List<SelectListItem> colorListItems = unitOfWork.Colors.GetAll().Select(a => new SelectListItem
            {
                Text = a.ColorName,
                Value = a.Id.ToString()
            }).ToList();
            ViewBag.categories = categoryListItems;
            ViewBag.colors = colorListItems;
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product entity,List<int> ColorsIds)
        {

           var newProduct= unitOfWork.Products.Add(entity);
            entity.ImgName = UploadFile.SaveFile(entity.ImgUrl, "img");
            unitOfWork.Complete();

         

            unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = unitOfWork.Products.GetById(id);
            return View(product);

        }


        [HttpPost]
        public IActionResult ConfirmEdit(Product entity)
        {
            unitOfWork.Products.Update(entity);
            unitOfWork.Complete();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            unitOfWork.Products.Delete(id);
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
