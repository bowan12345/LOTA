using LOTA.DataAccess.Data;
using LOTA.Model;
using Microsoft.EntityFrameworkCore;
using LOTA.DataAccess.Repository.IRepository;

namespace LOTA.DataAccess.Repository
{
    public class TrimesterCourseRepository : ITrimesterCourseRepository
    {
        private readonly ApplicationDbContext _context;

        public TrimesterCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrimesterCourse>> GetAllTrimesterCoursesAsync()
        {
            return await _context.TrimesterCourse
                .Include(tc => tc.Trimester)
                .Include(tc => tc.Course)
                .Include(tc => tc.Tutor)
                .OrderByDescending(tc => tc.Trimester.AcademicYear)
                .ThenByDescending(tc => tc.Trimester.TrimesterNumber)
                .ThenBy(tc => tc.Course.CourseCode)
                .ToListAsync();
        }

        public async Task<TrimesterCourse> GetTrimesterCourseByIdAsync(string id)
        {
            return await _context.TrimesterCourse
                .Include(tc => tc.Trimester)
                .Include(tc => tc.Course)
                .Include(tc => tc.Course.Qualification)
                .Include(tc => tc.Course.LearningOutcomes)
                .Include(tc => tc.Tutor)
                .FirstOrDefaultAsync(tc => tc.Id == id);
        }

        public async Task<TrimesterCourse> GetTrimesterCourseWithDetailsAsync(string id)
        {
            return await _context.TrimesterCourse
                .Include(tc => tc.Trimester)
                .Include(tc => tc.Course)
                .Include(tc => tc.Course.Qualification)
                .Include(tc => tc.Course.LearningOutcomes)
                .Include(tc => tc.Tutor)
                .FirstOrDefaultAsync(tc => tc.Id == id);
        }

        public async Task<TrimesterCourse> CreateTrimesterCourseAsync(TrimesterCourse trimesterCourse)
        {
            _context.TrimesterCourse.Add(trimesterCourse);
            return trimesterCourse;
        }

        public async Task<TrimesterCourse> UpdateTrimesterCourseAsync(TrimesterCourse trimesterCourse)
        {
            _context.TrimesterCourse.Update(trimesterCourse);
            return trimesterCourse;
        }

        public async Task<bool> DeleteTrimesterCourseAsync(string id)
        {
            var trimesterCourse = await _context.TrimesterCourse.FindAsync(id);
            if (trimesterCourse == null)
                return false;

            _context.TrimesterCourse.Remove(trimesterCourse);
            return true;
        }

        public async Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByTrimesterAsync(string trimesterId)
        {
            return await _context.TrimesterCourse
                .Include(tc => tc.Trimester)
                .Include(tc => tc.Course)
                .Include(tc => tc.Course.Qualification)
                .Include(tc => tc.Tutor)
                .Where(tc => tc.TrimesterId == trimesterId && tc.IsActive == true)
                .OrderBy(tc => tc.Course.CourseCode)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByCourseAsync(string courseId)
        {
            return await _context.TrimesterCourse
                .Include(tc => tc.Trimester)
                .Include(tc => tc.Course)
                .Include(tc => tc.Tutor)
                .Where(tc => tc.CourseId == courseId)
                .OrderByDescending(tc => tc.Trimester.AcademicYear)
                .ThenByDescending(tc => tc.Trimester.TrimesterNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByTutorAsync(string tutorId)
        {
            return await _context.TrimesterCourse
                .Include(tc => tc.Trimester)
                .Include(tc => tc.Course)
                .Include(tc => tc.Tutor)
                .Where(tc => tc.TutorId == tutorId)
                .OrderByDescending(tc => tc.Trimester.AcademicYear)
                .ThenByDescending(tc => tc.Trimester.TrimesterNumber)
                .ThenBy(tc => tc.Course.CourseCode)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByTrimesterAndCourseAsync(string trimesterId, string courseId)
        {
            return await _context.TrimesterCourse
                .Include(tc => tc.Trimester)
                .Include(tc => tc.Course)
                .Include(tc => tc.Tutor)
                .Where(tc => tc.TrimesterId == trimesterId && tc.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<bool> IsTrimesterCourseExistsAsync(string trimesterId, string courseId)
        {
            return await _context.TrimesterCourse
                .AnyAsync(tc => tc.TrimesterId == trimesterId && tc.CourseId == courseId);
        }

        public async Task<IEnumerable<TrimesterCourse>> GetLatestTrimesterCourseOfferingsAsync()
        {
            var query = _context.TrimesterCourse
                .Include(tc => tc.Trimester)
                .Include(tc => tc.Course)
                .Include(tc => tc.Course.Qualification)
                .Include(tc => tc.Tutor)
                .Where(tc => tc.IsActive == true);

            // Get the latest trimester
            var latestTrimester = await _context.Trimester
                .OrderByDescending(t => t.AcademicYear)
                .ThenByDescending(t => t.TrimesterNumber)
                .FirstOrDefaultAsync();

            if (latestTrimester != null)
            {
                query = query.Where(tc => tc.TrimesterId == latestTrimester.Id);
            }

            return await query
                .OrderBy(tc => tc.Course.CourseCode)
                .ToListAsync();
        }
    }
}
