using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICourseRepository courseRepository { get; }
        ITutorRepository tutorRepository { get; }
        ITutorCourseRepository tutorCourseRepository { get; }

        Task<int> SaveAsync();
    }
}