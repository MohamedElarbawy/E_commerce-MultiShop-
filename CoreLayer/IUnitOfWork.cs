using CoreLayer.Entities;
using CoreLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer
{
    public interface IUnitOfWork : IDisposable
    {

       
        IProductsRepo Products { get; }
        ICategoryRepo Categories { get; }

        IColorRepo Colors { get; }
        int Complete();
       
    }
}
