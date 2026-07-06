using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface ILikeService
{
    Task<LikeStatusDto> ToggleAsync(ToggleLikeDto dto);
    Task<LikeStatusDto> GetStatusAsync(string userId, string targetType, long targetId);
    Task<long> GetCountAsync(string targetType, long targetId);
    Task<List<LikeDto>> GetByUserAsync(string userId);
}
