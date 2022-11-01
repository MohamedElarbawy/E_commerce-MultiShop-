using CoreLayer;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Layer.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public SearchController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public JsonResult Search(string s)
        {
            var products = unitOfWork.Products.Search(p=>p.IsActive &&( p
            .ProductName.Contains(s)||(p.ProductDescription!=null?p
            .ProductDescription.Contains(s):false)));

            if (products.Count() == 0)
            {
            List<Product> productsSearchedByCategory = new List<Product>();
          var categories= unitOfWork.Categories.Search(x=>x.CategoryName.Contains(s));
                foreach (var category in categories)
                {
                    productsSearchedByCategory.AddRange ( unitOfWork.Products.GetAllThatMatchesACriteria(p => p.ProductCaregoryId == category.Id && p.IsActive));

                }
                return Json(productsSearchedByCategory);
            }
                                              
          
            return Json(products);
        }


    }
}
