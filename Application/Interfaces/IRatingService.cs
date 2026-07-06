using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface IRatingService
{
    Task<List<RatingDto>> GetAllRatingsAsync();
    Task<RatingDto?> GetRatingByIdAsync(int id);
    Task<RatingDto> CreateRatingAsync(CreateRatingDto dto);
    Task<RatingDto> UpdateRatingAsync(int id, UpdateRatingDto dto);
    Task<bool> DeleteRatingAsync(int id);
}
