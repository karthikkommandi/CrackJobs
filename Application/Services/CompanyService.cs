using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;

    public CompanyService(ICompanyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CompanyDto>> GetAllCompaniesAsync()
    {
        var companies = await _repository.GetAllAsync();
        return _mapper.Map<List<CompanyDto>>(companies);
    }

    public async Task<CompanyDto?> GetCompanyByIdAsync(int id)
    {
        var company = await _repository.GetByIdAsync(id);
        return company == null ? null : _mapper.Map<CompanyDto>(company);
    }

    public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto dto)
    {
        var company = _mapper.Map<Company>(dto);
        company.CreatedDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(company);
        return _mapper.Map<CompanyDto>(created);
    }

    public async Task<CompanyDto> UpdateCompanyAsync(int id, UpdateCompanyDto dto)
    {
        var company = await _repository.GetByIdAsync(id);
        if (company == null) throw new KeyNotFoundException($"Company with ID {id} not found");

        _mapper.Map(dto, company);
        company.UpdatedDate = DateTime.UtcNow;
        var updated = await _repository.UpdateAsync(company);
        return _mapper.Map<CompanyDto>(updated);
    }

    public async Task<bool> DeleteCompanyAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
