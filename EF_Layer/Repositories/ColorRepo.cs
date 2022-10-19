using CoreLayer.Entities;
using CoreLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class ColorRepo : GenericRepository<Color>, IColorRepo
    {
        private readonly MultiShopContext context;

        public ColorRepo(MultiShopContext context) : base(context)
        {
            this.context = context;
        }
    }
}
