using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;

namespace CrackJobs.Infrastructure.Repositories;

public class QuestionRepository : GenericRepository<Questinon>, IQuestionRepository
{
    public QuestionRepository(CrackJobContext context) : base(context)
    {
    }
}
