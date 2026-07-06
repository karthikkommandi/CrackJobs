using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class QuestionTechnologyMapService : IQuestionTechnologyMapService
{
    private readonly IQuestionTechnologyMapRepository _repository;
    private readonly IMapper _mapper;

    public QuestionTechnologyMapService(IQuestionTechnologyMapRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<QuestionTechnologyMapDto>> GetAllAsync()
    {
        var maps = await _repository.GetAllAsync();
        return _mapper.Map<List<QuestionTechnologyMapDto>>(maps);
    }

    public async Task<QuestionTechnologyMapDto?> GetByIdAsync(int id)
    {
        var map = await _repository.GetByIdAsync(id);
        return map == null ? null : _mapper.Map<QuestionTechnologyMapDto>(map);
    }

    public async Task<QuestionTechnologyMapDto> CreateAsync(CreateQuestionTechnologyMapDto dto)
    {
        var map = _mapper.Map<QuestionTechnologyMap>(dto);
        // The Id column is not database-generated, so assign the next available value.
        if (map.Id == 0)
        {
            var existing = await _repository.GetAllAsync();
            map.Id = existing.Count == 0 ? 1 : existing.Max(m => m.Id) + 1;
        }
        map.CreatedDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(map);
        return _mapper.Map<QuestionTechnologyMapDto>(created);
    }

    public async Task<QuestionTechnologyMapDto> UpdateAsync(int id, UpdateQuestionTechnologyMapDto dto)
    {
        var map = await _repository.GetByIdAsync(id);
        if (map == null) throw new KeyNotFoundException($"QuestionTechnologyMap with ID {id} not found");

        _mapper.Map(dto, map);
        map.UpdatedDate = DateTime.UtcNow;
        var updated = await _repository.UpdateAsync(map);
        return _mapper.Map<QuestionTechnologyMapDto>(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
