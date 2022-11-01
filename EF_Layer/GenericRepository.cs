using BusinessLogicLayer.Helper;
using CoreLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        
        public IEnumerable<T> GetAllThatMatchesACriteria(Expression<Func<T,bool>> critria)
        {
            return _context.Set<T>().Where(critria).ToList();
        }
        public IEnumerable<T> GetAllThatMatchesACriteria(Expression<Func<T,bool>> critria,int numberOfT)
        {
            return _context.Set<T>().Where(critria).Take(numberOfT).ToList();
        }
        public T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
                 return entity;
            
        }
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);

            return entity;

        }
        public void AddRange(List<T> entities)
        {
           _context.Set<T>().AddRange(entities);
        }
        public void Delete(int id)
        {
            var entity = GetById(id);
            _context.Set<T>().Remove(entity);

        } 
        public void DeleteRange(List<T> entities)
        {
            
            _context.Set<T>().RemoveRange(entities);

        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
           
        }

        //pagination
        public IEnumerable<T> GetItemsPerPage(int pageNumber, int pageSize, IEnumerable<T> totalItems)
        {
            if (pageNumber < 1)
                pageNumber = 1;
            if (pageSize < 1)
                pageSize = 10;


            return totalItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int NumberOfItems()
        {
            return _context.Set<T>().Count();
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> criteria)
        {
           
            return _context.Set<T>().Where(criteria).ToList();
           
        }

      
    }
}
