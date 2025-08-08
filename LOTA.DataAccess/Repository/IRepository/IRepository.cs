using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IRepository<T, TKey> where T : class
    {
        /// <summary>
        /// get object by id
        /// </summary>
        /// <param name="id"> id of object </param>
        /// <returns> object </returns>
        Task<T> GetByIdAsync(TKey id, string? includeProperties = null);

        /// <summary>
        /// get all objects
        /// </summary>
        /// <param name="filter"> filter expression </param>
        /// <param name="includeProperties"> include properties </param>
        /// <returns> objects </returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties= null);

        /// <summary>
        /// get objects by filter
        /// </summary>
        /// <param name="filter"> filter expression </param>
        /// <param name="includeProperties"> include properties </param>
        /// <returns> objects </returns>
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);

        /// <summary>
        /// add object
        /// </summary>
        /// <param name="entity"> object to add </param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// add range of objects
        /// </summary>
        /// <param name="entities"> objects to add </param>
        /// <returns></returns>
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// remove object
        /// </summary>
        /// <param name="entity"> object to remove </param>
        void Remove(T entity);

        /// <summary>
        ///  remove range of objects
        /// </summary>
        /// <param name="entities"> objects to remove </param>
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>
        /// update object
        /// </summary>
        /// <param name="entity"> object to update </param>
        void Update(T entity);
    }
}
