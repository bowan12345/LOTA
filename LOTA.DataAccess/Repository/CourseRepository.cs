using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
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


    }
}
