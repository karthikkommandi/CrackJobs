using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface IAnswerService
{
    Task<List<AnswerDto>> GetAllAnswersAsync();
    Task<AnswerDto?> GetAnswerByIdAsync(long id);
    Task<AnswerDto> CreateAnswerAsync(CreateAnswerDto dto);
    Task<AnswerDto> UpdateAnswerAsync(long id, UpdateAnswerDto dto);
    Task<bool> DeleteAnswerAsync(long id);
}
