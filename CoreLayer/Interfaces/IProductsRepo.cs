﻿using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces
{
    public interface IProductsRepo:IGenericRepository<Product>
    {
        IEnumerable<Product> GetAllActiveProducts();
        IEnumerable<Product> GetProductsInCAtegory(int id);
        //IEnumerable<Product> GetProductsPerPage(int pageNumber,int pageSize);
        IEnumerable<Product> FilterProductsByPrice(int min, int max);
        IEnumerable<Product> GetLastAddedProducts(int NumberOfProducts);
        void ChangeActiveStateToFalse(int id);
        void ChangeActiveStateToTrue(int id);
    }
}
