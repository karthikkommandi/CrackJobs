using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;

namespace CrackJobs.Infrastructure.Repositories;

public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
{
    public AnswerRepository(CrackJobContext context) : base(context)
    {
    }
}
