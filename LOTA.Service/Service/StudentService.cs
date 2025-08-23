using LOTA.Model;
using LOTA.Model.DTO.Admin;
using LOTA.Service.Service.IService;
using LOTA.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace LOTA.Service.Service
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IEnumerable<StudentReturnDTO>> GetAllStudentsAsync()
        {
            
            // Use Repository's GetAllAsync method, then filter users with StudentNo
            var allUsers = await _unitOfWork.studentRepository.GetAllAsync();
            var students = allUsers.Where(u => !string.IsNullOrEmpty(u.StudentNo));
            return students.Select(MapToDTO);
        }

        public async Task<StudentReturnDTO?> GetStudentByIdAsync(string id)
        {
            
            var student = await _unitOfWork.studentRepository.GetByIdAsync(id);
            // Check if StudentNo exists, if yes then consider as student
            return student != null && !string.IsNullOrEmpty(student.StudentNo) ? MapToDTO(student) : null;
        }

        public async Task<IEnumerable<StudentReturnDTO>> GetStudentsByNameOrEmailAsync(string searchTerm)
        {
            
            // Use Repository's GetAllAsync method, then filter and search
            var allUsers = await _unitOfWork.studentRepository.GetAllAsync();
            var students = allUsers.Where(u => !string.IsNullOrEmpty(u.StudentNo));
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                students = students.Where(u =>
                    u.FirstName?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                    u.LastName?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                    u.Email?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                    u.StudentNo?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true);
            }
            
            return students.Select(MapToDTO);
        }

        public async Task<StudentReturnDTO> CreateStudentAsync(StudentCreateDTO studentDto)
        {
            
            // Check if email already exists
            if (await _userManager.FindByEmailAsync(studentDto.Email) != null)
            {
                throw new InvalidOperationException($"User with email '{studentDto.Email}' already exists.");
            }

             // Check if student number already exists
             if (!string.IsNullOrEmpty(studentDto.StudentNo))
             {
                 var existingStudent = _userManager.Users.FirstOrDefault(u => u.StudentNo == studentDto.StudentNo);
                 if (existingStudent != null)
                 {
                     throw new InvalidOperationException($"Student with number '{studentDto.StudentNo}' already exists.");
                 }
             }

              var student = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = studentDto.FirstName,
                    LastName = studentDto.LastName,
                    Email = studentDto.Email,
                    UserName = studentDto.Email,
                    StudentNo = studentDto.StudentNo,
                    IsActive = studentDto.IsActive,
                    EmailConfirmed = true
                };

            var result = await _userManager.CreateAsync(student, studentDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create student: {errors}");
            }

            // Add student role
            await _userManager.AddToRoleAsync(student, "Student");

            return MapToDTO(student);
        }

        public async Task<StudentReturnDTO> UpdateStudentAsync(StudentUpdateDTO studentDto)
        {
            
            var existingStudent = await _userManager.FindByIdAsync(studentDto.Id);
            if (existingStudent == null)
            {
                throw new InvalidOperationException($"Student with ID '{studentDto.Id}' not found.");
            }

            // Check if email already exists (excluding current student)
            var existingUserWithEmail = await _userManager.FindByEmailAsync(studentDto.Email);
            if (existingUserWithEmail != null && existingUserWithEmail.Id != studentDto.Id)
            {
                throw new InvalidOperationException($"User with email '{studentDto.Email}' already exists.");
            }

             // Check if student number already exists (excluding current student)
             if (!string.IsNullOrEmpty(studentDto.StudentNo))
             {
                 var existingStudentWithNo = _userManager.Users.FirstOrDefault(u => u.StudentNo == studentDto.StudentNo && u.Id != studentDto.Id);
                 if (existingStudentWithNo != null)
                 {
                     throw new InvalidOperationException($"Student with number '{studentDto.StudentNo}' already exists.");
                 }
             }

                existingStudent.FirstName = studentDto.FirstName;
                existingStudent.LastName = studentDto.LastName;
                existingStudent.Email = studentDto.Email;
                existingStudent.UserName = studentDto.Email;
                existingStudent.StudentNo = studentDto.StudentNo;
                existingStudent.IsActive = studentDto.IsActive;

            var result = await _userManager.UpdateAsync(existingStudent);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to update student: {errors}");
            }

            return MapToDTO(existingStudent);
        }

        public async Task<bool> DeleteStudentAsync(string id)
         {
            
            var student = await _userManager.FindByIdAsync(id);
             if (student == null)
             {
                 return false;
             }

             // Check if student has enrolled courses
             var enrolledCourses = student.StudentCourses?.Count ?? 0;
             if (enrolledCourses > 0)
             {
                 throw new InvalidOperationException($"Cannot delete student '{student.FirstName} {student.LastName}' because they have enrolled courses.");
             }

             var result = await _userManager.DeleteAsync(student);
             return result.Succeeded;
         }

       public async Task<bool> IsStudentEmailExistsAsync(string email, string? excludeId = null)
        {
            
            var allUsers = await _unitOfWork.studentRepository.GetAllAsync();
            var user = allUsers.FirstOrDefault(u => u.Email == email);
            
            if (user == null)
                return false;

            if (!string.IsNullOrEmpty(excludeId))
                return user.Id != excludeId;

            return true;
        }

        public async Task<bool> IsStudentNoExistsAsync(string studentNo, string? excludeId = null)
        {
            
            if (string.IsNullOrEmpty(studentNo)) 
            {
                return false;
            }
            var allUsers = await _unitOfWork.studentRepository.GetAllAsync();
            var student = allUsers.FirstOrDefault(u => u.StudentNo == studentNo);
            
            if (student == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(excludeId))
            {
                return student.Id != excludeId;
            }

            return true;
        }

        private StudentReturnDTO MapToDTO(ApplicationUser student)
        {
            return new StudentReturnDTO
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                StudentNo = student.StudentNo,
                IsActive = student.IsActive,
                EnrolledCoursesCount = student.StudentCourses?.Count ?? 0
            };
        }

        public async Task<IEnumerable<StudentReturnDTO>> GetEnrolledStudentsAsync(string courseOfferingId, string trimesterId)
        {
            var studentCourses = await _unitOfWork.studentCourseRepository.GetByCourseOfferingIdAndTrimesterAsync(courseOfferingId, trimesterId);
            return studentCourses.Select(sc => new StudentReturnDTO
            {
                Id = sc.Student.Id,
                FirstName = sc.Student.FirstName,
                LastName = sc.Student.LastName,
                Email = sc.Student.Email,
                StudentNo = sc.Student.StudentNo,
                IsActive = sc.Student.IsActive,
                EnrolledCoursesCount = 0 // We don't need this for this context
            });
        }
    }
}
