using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface IQuestionService
{
    Task<List<QuestionDto>> GetAllQuestionsAsync();
    Task<QuestionDto?> GetQuestionByIdAsync(long id);
    Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto dto);
    Task<QuestionDto> UpdateQuestionAsync(long id, UpdateQuestionDto dto);
    Task<bool> DeleteQuestionAsync(long id);
}
