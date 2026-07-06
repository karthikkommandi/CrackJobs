using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface IQuestionCompanyMapService
{
    Task<List<QuestionCompanyMapDto>> GetAllAsync();
    Task<QuestionCompanyMapDto?> GetByIdAsync(long id);
    Task<QuestionCompanyMapDto> CreateAsync(CreateQuestionCompanyMapDto dto);
    Task<QuestionCompanyMapDto> UpdateAsync(long id, UpdateQuestionCompanyMapDto dto);
    Task<bool> DeleteAsync(long id);
}
