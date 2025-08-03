using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
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
        /*public ICategoryRepository categoryRepository { get; private set; }*/


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            /*categoryRepository = new CategoryRepository(_db);*/
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}