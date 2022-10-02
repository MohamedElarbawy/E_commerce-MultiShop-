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

        public IEnumerable<Product> GetProductsInCAtegory(int id)
        {
            return context.Products.Where(p => p.ProductCaregoryId == id).ToList();
        }

        //public IEnumerable<Product> GetProductsPerPage(int pageNumber, int pageSize)
        //{
        //    return context.Products.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        //}
    }
}
