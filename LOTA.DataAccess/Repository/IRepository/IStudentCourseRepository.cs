using LOTA.Model;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IStudentCourseRepository : IRepository<StudentCourse, string>
    {
        Task<IEnumerable<StudentCourse>> GetByStudentIdAsync(string studentId);
        Task<IEnumerable<StudentCourse>> GetByCourseOfferingIdAsync(string courseId);
        Task<StudentCourse?> GetByStudentAndCourseAsync(string studentId, string courseOfferingId);
        /// <summary>
        /// Get student courses by offered course ID and trimester
        /// </summary>
        /// <param name="courseOfferingId"> offered Course ID</param>
        /// <param name="trimesterId">Trimester id</param>
        /// <returns>List of student courses</returns>
        Task<IEnumerable<StudentCourse>> GetByCourseOfferingIdAndTrimesterAsync(string courseOferingId, string trimesterId);

        /// <summary>
        ///  delete all student courses by trimester id
        /// </summary>
        /// <param name="id"> trimester id</param>
        void RemoveAllByTrimesterId(string id);

        /// <summary>
        ///  delete all student courses by trimester ids
        /// </summary>
        /// <param name="ids"> a list of trimester id</param>
        void RemoveAllByTrimesterIds(IEnumerable<string> ids);
        void RemoveEnrolledStudentByCourseOfferingId(string courseOfferingId);
    }
}
