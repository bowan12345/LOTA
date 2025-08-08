using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository;
using LOTA.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICourseRepository courseRepository { get; private set; }
        public ITutorRepository tutorRepository { get; private set; }
        public ITutorCourseRepository tutorCourseRepository { get; private set; }
        public ILearningOutcomeRepository learningOutcomeRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            courseRepository = new CourseRepository(_db);
            tutorRepository = new TutorRepository(_db);
            tutorCourseRepository = new TutorCourseRepository(_db);
            learningOutcomeRepository = new LearningOutcomeRepository(_db);
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}