using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _repository;
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public LikeService(ILikeRepository repository, ICommentRepository commentRepository, IMapper mapper)
    {
        _repository = repository;
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<LikeStatusDto> ToggleAsync(ToggleLikeDto dto)
    {
        var type = (dto.TargetType ?? string.Empty).ToLowerInvariant();
        var existing = await _repository.FindAsync(dto.UserId, type, dto.TargetId);

        bool liked;
        if (existing != null)
        {
            await _repository.DeleteAsync(existing.Id);
            liked = false;
        }
        else
        {
            await _repository.AddAsync(new Like
            {
                UserId = dto.UserId,
                TargetType = type,
                TargetId = dto.TargetId,
                CreatedDate = DateTime.UtcNow
            });
            liked = true;
        }

        var count = await _repository.CountAsync(type, dto.TargetId);

        // Keep the denormalized LikeCount on comments in sync so list views stay accurate.
        if (type == "comment")
        {
            var comment = await _commentRepository.GetByIdAsync(dto.TargetId);
            if (comment != null)
            {
                comment.LikeCount = count;
                await _commentRepository.UpdateAsync(comment);
            }
        }

        return new LikeStatusDto { Liked = liked, Count = count };
    }

    public async Task<LikeStatusDto> GetStatusAsync(string userId, string targetType, long targetId)
    {
        var type = (targetType ?? string.Empty).ToLowerInvariant();
        var existing = string.IsNullOrEmpty(userId) ? null : await _repository.FindAsync(userId, type, targetId);
        var count = await _repository.CountAsync(type, targetId);
        return new LikeStatusDto { Liked = existing != null, Count = count };
    }

    public async Task<long> GetCountAsync(string targetType, long targetId)
    {
        return await _repository.CountAsync((targetType ?? string.Empty).ToLowerInvariant(), targetId);
    }

    public async Task<List<LikeDto>> GetByUserAsync(string userId)
    {
        var likes = await _repository.GetByUserAsync(userId);
        return _mapper.Map<List<LikeDto>>(likes);
    }
}
