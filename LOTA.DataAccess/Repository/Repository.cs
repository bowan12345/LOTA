using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.DataAccess.Repository
{
    public class Repository<T> : IRepository<T,string> where T : class
    {
        protected readonly ApplicationDbContext _db;
        protected readonly DbSet<T> dbset;
        public Repository(ApplicationDbContext db)
        {
            this._db = db;
            dbset = _db.Set<T>();
           /* _db.Movies.Include(u => u.Category).Include(u=>u.CategoryId);*/
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbset.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await dbset.AddRangeAsync(entities);
            return entities;
        }

        public async Task<T> GetByIdAsync(string id, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync(x => EF.Property<string>(x,"Id") == id);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            query = query.Where(filter);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties)) 
            {
                foreach (var item in includeProperties.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            List<T> ts = await query.ToListAsync();
            return ts;
        }

        public void Update(T entity)
        {
            dbset.Update(entity);
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }


    }
}
