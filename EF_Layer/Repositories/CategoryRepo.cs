using CoreLayer;
using CoreLayer.Entities;
using CoreLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class CategoryRepo :GenericRepository<Category>, ICategoryRepo

    {
        private readonly MultiShopContext context;

        public CategoryRepo(MultiShopContext context):base(context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAllCategoreis()
        {
        return context.Categories.ToList();
        }

        
    }
}
