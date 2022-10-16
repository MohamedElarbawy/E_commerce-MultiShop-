using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        void Delete(int Id);

       //pagination
        public IEnumerable<T> GetItemsPerPage(int pageNumber, int pageSize,IEnumerable<T> totalItems);
        public int NumberOfItems();
       

        //Asynchronous
        //Task<T> GetByIdAsync(int id);
        //Task<IEnumerable<T>> GetAllAsync();
        //Task<T> AddAsync(T entity);


    }
}
