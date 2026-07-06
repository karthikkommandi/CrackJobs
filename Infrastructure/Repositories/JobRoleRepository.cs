using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;

namespace CrackJobs.Infrastructure.Repositories;

public class JobRoleRepository : GenericRepository<JobRole>, IJobRoleRepository
{
    public JobRoleRepository(CrackJobContext context) : base(context)
    {
    }
}
