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
        ILearningOutcomeRepository learningOutcomeRepository { get; }
        IQualificationRepository qualificationRepository { get; }
        IStudentRepository studentRepository { get; }
        IStudentCourseRepository studentCourseRepository { get; }
        ITrimesterRepository trimesterRepository { get; }
        ITrimesterCourseRepository trimesterCourseRepository { get; }
        IAssessmentRepository assessmentRepository { get; }
        IAssessmentTypeRepository assessmentTypeRepository { get; }
        IStudentScoreRepository studentScoreRepository { get; }
        IStudentLOScoreRepository studentLOScoreRepository { get; }
        
        Task<int> SaveAsync();
    }
}