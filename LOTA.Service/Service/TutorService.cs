using LOTA.DataAccess.Repository;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Service.Service
{
    public class TutorService : ITutorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TutorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddTutorAsync(ApplicationUser user)
        {
            throw new NotImplementedException("TDD Red phase: method not implemented yet");
        }

        public async Task<ApplicationUser> GetTutorByEmailAsync(string email)
        {
            throw new NotImplementedException("TDD Red phase: method not implemented yet");
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllTutorsAsync()
        {
            // Get only users who have TutorNo (i.e., are tutors)
            return await _unitOfWork.tutorRepository.GetAllAsync(
                filter: u => !string.IsNullOrEmpty(u.TutorNo)
            );
        }
    }
}
