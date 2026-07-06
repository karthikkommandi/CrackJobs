using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;

namespace CrackJobs.Infrastructure.Repositories;

public class RatingRepository : GenericRepository<Rating>, IRatingRepository
{
    public RatingRepository(CrackJobContext context) : base(context)
    {
    }
}
