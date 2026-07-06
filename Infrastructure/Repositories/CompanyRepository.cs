using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;

namespace CrackJobs.Infrastructure.Repositories;

public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(CrackJobContext context) : base(context)
    {
    }
}
