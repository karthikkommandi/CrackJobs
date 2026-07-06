using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface IQuestionAnswerMapService
{
    Task<List<QuestionAnswerMapDto>> GetAllAsync();
    Task<QuestionAnswerMapDto?> GetByIdAsync(long id);
    Task<QuestionAnswerMapDto> CreateAsync(CreateQuestionAnswerMapDto dto);
    Task<QuestionAnswerMapDto> UpdateAsync(long id, UpdateQuestionAnswerMapDto dto);
    Task<bool> DeleteAsync(long id);
}
