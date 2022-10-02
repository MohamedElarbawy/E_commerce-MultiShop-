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
        IEnumerable<Product> GetProductsInCAtegory(int id);
        //IEnumerable<Product> GetProductsPerPage(int pageNumber,int pageSize);
    }
}
