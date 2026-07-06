using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;

namespace CrackJobs.Infrastructure.Repositories;

public class TechnologyRepository : GenericRepository<Technology>, ITechnologyRepository
{
    public TechnologyRepository(CrackJobContext context) : base(context)
    {
    }
}
