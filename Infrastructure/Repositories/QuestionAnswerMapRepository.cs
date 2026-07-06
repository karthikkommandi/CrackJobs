using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;

namespace CrackJobs.Infrastructure.Repositories;

public class QuestionAnswerMapRepository : GenericRepository<QuestionAnswerMap>, IQuestionAnswerMapRepository
{
    public QuestionAnswerMapRepository(CrackJobContext context) : base(context)
    {
    }
}
