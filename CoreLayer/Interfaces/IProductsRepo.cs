using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
    public interface IProductsRepo:IGenericRepository<Product>
    {
        IEnumerable<Product> GetAllProductsWithCategory();
        IEnumerable<Product> GetAllActiveProducts();
        IEnumerable<Product> GetProductsInCAtegory(int id);
        //IEnumerable<Product> GetProductsPerPage(int pageNumber,int pageSize);
        IEnumerable<Product> FilterProductsByPrice(string priceRange);
        IEnumerable<Product> FilterProductsByColor(string colorsIds);
        IEnumerable<Product> GetLastAddedProducts(int NumberOfProducts);
        public Product GetByIdWithColors(int id);
        void Edit(Product newProduct, Product oldProduct);
        void ChangeActiveStateToFalse(int id);
        void ChangeActiveStateToTrue(int id);
        IEnumerable<Product> GetAllProductsWithIds(List<int> ids);
    }
}
