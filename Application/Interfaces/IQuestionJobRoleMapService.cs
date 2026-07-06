using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface IQuestionJobRoleMapService
{
    Task<List<QuestionJobRoleMapDto>> GetAllAsync();
    Task<QuestionJobRoleMapDto?> GetByIdAsync(long id);
    Task<QuestionJobRoleMapDto> CreateAsync(CreateQuestionJobRoleMapDto dto);
    Task<QuestionJobRoleMapDto> UpdateAsync(long id, UpdateQuestionJobRoleMapDto dto);
    Task<bool> DeleteAsync(long id);
}
