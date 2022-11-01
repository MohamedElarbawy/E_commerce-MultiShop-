using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);

        IEnumerable<T> GetAllThatMatchesACriteria(Expression<Func<T, bool>> criteria);
        IEnumerable<T> GetAllThatMatchesACriteria(Expression<Func<T, bool>> criteria, int numberOfT);

        IEnumerable<T> GetAll();
        T Add(T entity);
        void AddRange(List<T> entities);
        void Update(T entity);
        void Delete(int Id);
        void DeleteRange(List<T> entities);

        public IEnumerable<T> Search(Expression<Func<T, bool>> criteria);

        //pagination
        IEnumerable<T> GetItemsPerPage(int pageNumber, int pageSize, IEnumerable<T> totalItems);
        int NumberOfItems();


        //Asynchronous
        //Task<T> GetByIdAsync(int id);
        //Task<IEnumerable<T>> GetAllAsync();
        //Task<T> AddAsync(T entity);

        //Task<IEnumerable<T>> GetAllThatMatchesACriteriaAsync(Expression<Func<T, bool>> criteria);

        //Task<IEnumerable<T>> GetAllThatMatchesACriteriaAsync(Expression<Func<T, bool>> criteria, int numberOfT);
        //Task<IEnumerable<T>> GetItemsPerPageAsync(int pageNumber, int pageSize, IEnumerable<T> totalItems);

    }
}
