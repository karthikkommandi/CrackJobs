using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface ICommentService
{
    Task<List<CommentDto>> GetAllCommentsAsync();
    Task<List<CommentDto>> GetByQuestionAsync(long questionId);
    Task<CommentDto?> GetCommentByIdAsync(long id);
    Task<CommentDto> CreateCommentAsync(CreateCommentDto dto);
    Task<CommentDto> UpdateCommentAsync(long id, UpdateCommentDto dto);
    Task<bool> DeleteCommentAsync(long id);
}
