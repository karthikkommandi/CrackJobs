using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;

namespace CrackJobs.Infrastructure.Repositories;

public class QuestionJobRoleMapRepository : GenericRepository<QuestionJobRoleMap>, IQuestionJobRoleMapRepository
{
    public QuestionJobRoleMapRepository(CrackJobContext context) : base(context)
    {
    }
}
