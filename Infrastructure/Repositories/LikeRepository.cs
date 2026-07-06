using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using CrackJobs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrackJobs.Infrastructure.Repositories;

public class LikeRepository : GenericRepository<Like>, ILikeRepository
{
    private readonly CrackJobContext _context;

    public LikeRepository(CrackJobContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Like?> FindAsync(string userId, string targetType, long targetId)
    {
        return await _context.Likes.FirstOrDefaultAsync(l =>
            l.UserId == userId && l.TargetType == targetType && l.TargetId == targetId);
    }

    public async Task<long> CountAsync(string targetType, long targetId)
    {
        return await _context.Likes.CountAsync(l =>
            l.TargetType == targetType && l.TargetId == targetId);
    }

    public async Task<List<Like>> GetByUserAsync(string userId)
    {
        return await _context.Likes.Where(l => l.UserId == userId).ToListAsync();
    }
}
