using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface ICompanyService
{
    Task<List<CompanyDto>> GetAllCompaniesAsync();
    Task<CompanyDto?> GetCompanyByIdAsync(int id);
    Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto dto);
    Task<CompanyDto> UpdateCompanyAsync(int id, UpdateCompanyDto dto);
    Task<bool> DeleteCompanyAsync(int id);
}
