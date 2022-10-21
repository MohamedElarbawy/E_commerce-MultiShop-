using CoreLayer.Entities;
using CoreLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class ProductsRepo : GenericRepository<Product>, IProductsRepo
    {
        private readonly MultiShopContext context;

        public ProductsRepo(MultiShopContext context) : base(context)
        {
            this.context = context;
        }


        public IEnumerable<Product> GetAllProductsWithCategory()
        {
            return context.Products.Include(p=>p.ProductCaregory)
                                   .Include(p=>p.Colors)
                                   .ToList();
        }

        public IEnumerable<Product> GetAllActiveProducts()
        {
            return context.Products.Where(p => p.IsActive).ToList();
        }

        public IEnumerable<Product> GetProductsInCAtegory(int id)
        {
            return context.Products.Where(p => p.ProductCaregoryId == id).ToList();
        }




        public IEnumerable<Product> FilterProductsByPrice(string priceRange)
        {
            int min = 0, max = int.MaxValue;
            if (priceRange != null && priceRange.Length != 0)
            {
                var priceQueryString = priceRange.Split("-");
                if (priceQueryString.Length == 2)
                    try
                    {
                        min = Convert.ToInt32(priceQueryString.FirstOrDefault());
                        max = Convert.ToInt32(priceQueryString.LastOrDefault());
                        var result = context.Products.Where(p => p.ProductPrice > min && p.ProductPrice <= max && p.IsActive).ToList();
                        return result;
                    }

                    catch
                    {
                        return Enumerable.Empty<Product>();
                    }
            }
            return GetAllActiveProducts();
        }

        public IEnumerable<Product> FilterProductsByColor(string colorsIds)
        {
            List<Product> products = new List<Product>();
            if (colorsIds !=null && colorsIds.Length>0)
            {
              string[] ids=  colorsIds.Split(',');
                
                foreach(var stringId in ids) {
                    int id = Convert.ToInt32(stringId);
                 products.AddRange( context.Products.Where(p => p.Colors.Any(c => c.Id == id)).ToList());
                    return products;
            }
            }
            return GetAllActiveProducts();
        } 
        



        public IEnumerable<Product> GetLastAddedProducts(int NumberOfProducts = 10)
        {
            if (NumberOfProducts <= 0)
                NumberOfProducts = 10;
            return context.Products.OrderByDescending(x => x.Id).Take(NumberOfProducts).ToList();
        }

        public void ChangeActiveStateToFalse(int id)
        {
            var SelectedProduct = context.Products.Find(id);
            if (SelectedProduct != null)
            {
                if (SelectedProduct.IsActive)
                    SelectedProduct.IsActive = false;
            }
        }

        public void ChangeActiveStateToTrue(int id)
        {
            var SelectedProduct = context.Products.Find(id);
            if (SelectedProduct != null)
            {
                if (!SelectedProduct.IsActive)
                    SelectedProduct.IsActive = true;
            }
        }


        public IEnumerable<Product> GetAllProductsWithIds(List<int> ids)
        {
            List<Product> productsList = new ();
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    productsList.Add(context.Products.Find(id));

                }
            }
            return productsList;
        }

        public void Edit(Product newProduct, Product oldProduct)
        {
            oldProduct.ProductName = newProduct.ProductName;
            oldProduct.ProductPrice = newProduct.ProductPrice;
            oldProduct.IsActive = newProduct.IsActive;
            oldProduct.ProductCaregoryId = newProduct.ProductCaregoryId;
            oldProduct.ProductDescription = newProduct.ProductDescription;
        }

        public Product GetByIdWithColors(int id)
        {
           return context.Products.Where(p=>p.Id == id).Include(p=>p.Colors).FirstOrDefault();
          
        }
    }
}
