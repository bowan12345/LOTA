using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.DataAccess.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
       
        public CourseRepository(ApplicationDbContext db) : base(db){}

        public async Task<Course> GetCourseByCodeAsync(string courseCode)
        {
            //throw new NotImplementedException();
           return await _db.Course.Where(c => c.CourseCode == courseCode).FirstOrDefaultAsync();
        }
    }
}
