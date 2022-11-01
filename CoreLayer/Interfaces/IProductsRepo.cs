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
        IEnumerable<Product> GetProductsIncludeCategoryNColors();
        IEnumerable<Product> GetAllActiveProductsIncludecolors();
        IEnumerable<Product> GetAllActiveProductsIncludecolors(int numbersOfProducts);
        IEnumerable<Product> FilterProductsByPrice(string priceRange);
        IEnumerable<Product> FilterProductsByColor(string colorsIds);
        IEnumerable<Product> GetLastAddedProducts(int NumberOfProducts);
        public Product GetProductWithItsRelatedColors(int id);
        void Edit(Product newProduct, Product oldProduct);
        void ChangeActiveState(int id);
        IEnumerable<Product> GetAllProductsWithIds(List<int> ids);
    }
}
