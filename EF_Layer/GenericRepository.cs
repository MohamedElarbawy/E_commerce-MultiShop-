using BusinessLogicLayer.Helper;
using CoreLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MultiShopContext _context;

        public GenericRepository(MultiShopContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            var entity= _context.Set<T>().Find(id);
            return entity;
        }
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);

            return entity;

        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _context.Set<T>().Remove(entity);

        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
                return entity;
        }

        //pagination
        public IEnumerable<T> GetItemsPerPage(int pageNumber, int pageSize,IEnumerable<T> totalItems)
        {
            if(pageNumber<1)
                pageNumber = 1;
            if(pageSize<1)
                pageSize = 10;
           
            
            return totalItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int NumberOfItems()
        {
            return _context.Set<T>().Count();
        }
    }
}
