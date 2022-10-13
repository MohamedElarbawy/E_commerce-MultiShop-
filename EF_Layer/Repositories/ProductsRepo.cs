using CoreLayer.Entities;
using CoreLayer.Interfaces;
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

        public IEnumerable<Product> GetAllActiveProducts()
        {
            return context.Products.Where(p => p.IsActive).ToList();
        }

        public IEnumerable<Product> GetProductsInCAtegory(int id)
        {
            return context.Products.Where(p => p.ProductCaregoryId == id).ToList();
        }

        


        public IEnumerable<Product> FilterProductsByPrice(int min, int max)
        {
            return context.Products.Where(p => p.ProductPrice > min && p.ProductPrice < max).ToList();
        }



        public IEnumerable<Product> GetLastAddedProducts(int NumberOfProducts = 10)
        {
            if (NumberOfProducts <= 0)
                NumberOfProducts = 10;
            return context.Products.OrderByDescending(x => x.Id).Take(NumberOfProducts).ToList();
        }

        public void ChangeActiveStateToFalse(int id)
        {
           var SelectedProduct= context.Products.Find(id);
            if (SelectedProduct != null)
            {
                if(SelectedProduct.IsActive)
                SelectedProduct.IsActive = false;
            }
        }

        public void ChangeActiveStateToTrue(int id)
        {
            var SelectedProduct = context.Products.Find(id);
            if (SelectedProduct != null)
            {
                if(!SelectedProduct.IsActive)
                SelectedProduct.IsActive = true;
            }
        }
    }
}
