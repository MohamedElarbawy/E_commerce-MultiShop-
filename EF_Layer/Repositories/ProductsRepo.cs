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
            return context.Products.Include(p=>p.ProductCaregory).ToList();
        }

        public IEnumerable<Product> GetAllActiveProducts()
        {
            return context.Products.Where(p => p.IsActive)/*Include(x => x.ProductColor)*/.ToList();
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

        public IEnumerable<Product> FilterProductsByColor(string colors)
        {
            List<Product> products = new();
            if (colors != null && colors.Length != 0)
            {
                foreach (var color in colors)
                    products.AddRange(context.Products.Where(p => p.ProductColorId == color && p.IsActive).ToList());
                return products;
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
    }
}
