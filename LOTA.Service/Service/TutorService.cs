using LOTA.DataAccess.Repository;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Service.Service.IService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Service.Service
{
    public class TutorService : ITutorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public TutorService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetTutorByEmailAsync(string email)
        {
            throw new NotImplementedException("TDD Red phase: method not implemented yet");
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllTutorsAsync()
        {
            // Get only users who have TutorNo (i.e., are tutors) and include their courses
            return await _unitOfWork.tutorRepository.GetAllAsync(
                filter: u => !string.IsNullOrEmpty(u.TutorNo));
        }


        public async Task<ApplicationUser> GetTutorByIdAsync(string id)
        {
          
            var tutors = await _unitOfWork.tutorRepository.GetAllAsync(
                filter: u => u.Id == id);
            return tutors.FirstOrDefault();
        }


        public async Task<IEnumerable<ApplicationUser>> SearchTutorsAsync(string searchTerm)
        {
           
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // If search term is empty, return all tutors
                return await GetAllTutorsAsync();
            }

            // Convert search term to lowercase for case-insensitive search
            searchTerm = searchTerm.ToLower();

            // Get tutors with search filter
            return await _unitOfWork.tutorRepository.GetAllAsync(
                filter: u => !string.IsNullOrEmpty(u.TutorNo) && 
                            (u.FirstName != null && u.FirstName.ToLower().Contains(searchTerm) ||
                             u.LastName != null && u.LastName.ToLower().Contains(searchTerm) ||
                             u.Email != null && u.Email.ToLower().Contains(searchTerm))
            );
        }

        public async Task DeleteTutorAsync(string id)
        {
            var tutor = await _userManager.FindByIdAsync(id);
            if (tutor == null)
            {
                throw new ArgumentException("Tutor not found");
            }

            // Clear tutor assignment from all trimester courses
            _unitOfWork.trimesterCourseRepository.ClearTutorFromAllCourses(tutor.Id);
            await _unitOfWork.SaveAsync();

            //Delete the tutor
            var result = await _userManager.DeleteAsync(tutor);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to delete tutor: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task DeleteTutorsAsync(IEnumerable<string> ids)
        {
            foreach (var id in ids)
            {
                await DeleteTutorAsync(id);
            }
        }
    }
}
