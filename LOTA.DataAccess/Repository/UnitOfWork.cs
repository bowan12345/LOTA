using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository;
using LOTA.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICourseRepository courseRepo { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            courseRepo = new CourseRepository(_db);
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}