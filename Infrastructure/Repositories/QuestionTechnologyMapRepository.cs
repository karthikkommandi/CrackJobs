using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;

namespace CrackJobs.Infrastructure.Repositories;

public class QuestionTechnologyMapRepository : GenericRepository<QuestionTechnologyMap>, IQuestionTechnologyMapRepository
{
    public QuestionTechnologyMapRepository(CrackJobContext context) : base(context)
    {
    }
}
