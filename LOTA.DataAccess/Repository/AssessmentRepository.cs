using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;

namespace LOTA.DataAccess.Repository
{
    public class AssessmentRepository : Repository<Assessment>, IAssessmentRepository
    {
        public AssessmentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
