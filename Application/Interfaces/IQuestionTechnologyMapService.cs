using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface IQuestionTechnologyMapService
{
    Task<List<QuestionTechnologyMapDto>> GetAllAsync();
    Task<QuestionTechnologyMapDto?> GetByIdAsync(int id);
    Task<QuestionTechnologyMapDto> CreateAsync(CreateQuestionTechnologyMapDto dto);
    Task<QuestionTechnologyMapDto> UpdateAsync(int id, UpdateQuestionTechnologyMapDto dto);
    Task<bool> DeleteAsync(int id);
}
