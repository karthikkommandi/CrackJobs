using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class QuestionCompanyMapService : IQuestionCompanyMapService
{
    private readonly IQuestionCompanyMapRepository _repository;
    private readonly IMapper _mapper;

    public QuestionCompanyMapService(IQuestionCompanyMapRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<QuestionCompanyMapDto>> GetAllAsync()
    {
        var maps = await _repository.GetAllAsync();
        return _mapper.Map<List<QuestionCompanyMapDto>>(maps);
    }

    public async Task<QuestionCompanyMapDto?> GetByIdAsync(long id)
    {
        var map = await _repository.GetByIdAsync(id);
        return map == null ? null : _mapper.Map<QuestionCompanyMapDto>(map);
    }

    public async Task<QuestionCompanyMapDto> CreateAsync(CreateQuestionCompanyMapDto dto)
    {
        var map = _mapper.Map<QuestionCompanyMap>(dto);
        // The Id column is not database-generated, so assign the next available value.
        if (map.Id == 0)
        {
            var existing = await _repository.GetAllAsync();
            map.Id = existing.Count == 0 ? 1 : existing.Max(m => m.Id) + 1;
        }
        map.CreatedDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(map);
        return _mapper.Map<QuestionCompanyMapDto>(created);
    }

    public async Task<QuestionCompanyMapDto> UpdateAsync(long id, UpdateQuestionCompanyMapDto dto)
    {
        var map = await _repository.GetByIdAsync(id);
        if (map == null) throw new KeyNotFoundException($"QuestionCompanyMap with ID {id} not found");

        _mapper.Map(dto, map);
        map.UpdatedDate = DateTime.UtcNow;
        var updated = await _repository.UpdateAsync(map);
        return _mapper.Map<QuestionCompanyMapDto>(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }
}
