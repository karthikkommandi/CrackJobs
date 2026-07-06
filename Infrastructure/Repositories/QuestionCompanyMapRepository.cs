using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;

namespace CrackJobs.Infrastructure.Repositories;

public class QuestionCompanyMapRepository : GenericRepository<QuestionCompanyMap>, IQuestionCompanyMapRepository
{
    public QuestionCompanyMapRepository(CrackJobContext context) : base(context)
    {
    }
}
