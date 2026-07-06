using CrackJobs.Domain;

namespace CrackJobs.Application.Interfaces;

public interface ILikeRepository : IRepository<Like>
{
    Task<Like?> FindAsync(string userId, string targetType, long targetId);
    Task<long> CountAsync(string targetType, long targetId);
    Task<List<Like>> GetByUserAsync(string userId);
}
