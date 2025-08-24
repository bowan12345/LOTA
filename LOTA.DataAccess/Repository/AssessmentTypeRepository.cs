using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;

namespace LOTA.DataAccess.Repository
{
    public class AssessmentTypeRepository : Repository<AssessmentType>, IAssessmentTypeRepository
    {
        public AssessmentTypeRepository(ApplicationDbContext db) : base(db) { }
    }
}
