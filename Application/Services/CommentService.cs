using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CommentDto>> GetAllCommentsAsync()
    {
        var comments = await _repository.GetAllAsync();
        return _mapper.Map<List<CommentDto>>(comments);
    }

    // Returns top-level comments for a question, each with its replies nested one level deep.
    public async Task<List<CommentDto>> GetByQuestionAsync(long questionId)
    {
        var all = (await _repository.GetAllAsync())
            .Where(c => c.QuestionId == questionId && !(c.IsDeleted ?? false))
            .ToList();

        var dtos = _mapper.Map<List<CommentDto>>(all);
        var byParent = dtos
            .Where(c => c.ParrentCommentId != null)
            .GroupBy(c => c.ParrentCommentId!.Value)
            .ToDictionary(g => g.Key, g => g.OrderBy(c => c.CreatedDate).ToList());

        foreach (var dto in dtos)
        {
            dto.Replies = byParent.TryGetValue(dto.Id, out var replies) ? replies : new List<CommentDto>();
        }

        return dtos
            .Where(c => c.ParrentCommentId == null)
            .OrderByDescending(c => c.CreatedDate)
            .ToList();
    }

    public async Task<CommentDto?> GetCommentByIdAsync(long id)
    {
        var comment = await _repository.GetByIdAsync(id);
        return comment == null ? null : _mapper.Map<CommentDto>(comment);
    }

    public async Task<CommentDto> CreateCommentAsync(CreateCommentDto dto)
    {
        var comment = _mapper.Map<Comment>(dto);
        comment.LikeCount = 0;
        comment.ReplyCount = 0;
        comment.IsDeleted = false;
        comment.CreatedDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(comment);

        // Bump the parent's reply count so list views can show it without a recount.
        if (dto.ParrentCommentId.HasValue)
        {
            var parent = await _repository.GetByIdAsync(dto.ParrentCommentId.Value);
            if (parent != null)
            {
                parent.ReplyCount = (parent.ReplyCount ?? 0) + 1;
                await _repository.UpdateAsync(parent);
            }
        }

        return _mapper.Map<CommentDto>(created);
    }

    public async Task<CommentDto> UpdateCommentAsync(long id, UpdateCommentDto dto)
    {
        var comment = await _repository.GetByIdAsync(id);
        if (comment == null) throw new KeyNotFoundException($"Comment with ID {id} not found");

        _mapper.Map(dto, comment);
        var updated = await _repository.UpdateAsync(comment);
        return _mapper.Map<CommentDto>(updated);
    }

    public async Task<bool> DeleteCommentAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }
}
