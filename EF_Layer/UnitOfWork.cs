﻿using CoreLayer;
using CoreLayer.Entities;
using CoreLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BusinessLogicLayer
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly MultiShopContext _context;

        public IProductsRepo Products { get; private set; }

        public ICategoryRepo Categories {get; private set; }

        public UnitOfWork(MultiShopContext context)
        {
            this._context = context;
            Products = new ProductsRepo(_context);
            Categories = new CategoryRepo(_context);
           
        }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
