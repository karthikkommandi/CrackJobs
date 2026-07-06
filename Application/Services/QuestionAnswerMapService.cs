using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class QuestionAnswerMapService : IQuestionAnswerMapService
{
    private readonly IQuestionAnswerMapRepository _repository;
    private readonly IMapper _mapper;

    public QuestionAnswerMapService(IQuestionAnswerMapRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<QuestionAnswerMapDto>> GetAllAsync()
    {
        var maps = await _repository.GetAllAsync();
        return _mapper.Map<List<QuestionAnswerMapDto>>(maps);
    }

    public async Task<QuestionAnswerMapDto?> GetByIdAsync(long id)
    {
        var map = await _repository.GetByIdAsync(id);
        return map == null ? null : _mapper.Map<QuestionAnswerMapDto>(map);
    }

    public async Task<QuestionAnswerMapDto> CreateAsync(CreateQuestionAnswerMapDto dto)
    {
        var map = _mapper.Map<QuestionAnswerMap>(dto);
        // The Id column is not database-generated, so assign the next available value.
        if (map.Id == 0)
        {
            var existing = await _repository.GetAllAsync();
            map.Id = existing.Count == 0 ? 1 : existing.Max(m => m.Id) + 1;
        }
        map.CreatedDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(map);
        return _mapper.Map<QuestionAnswerMapDto>(created);
    }

    public async Task<QuestionAnswerMapDto> UpdateAsync(long id, UpdateQuestionAnswerMapDto dto)
    {
        var map = await _repository.GetByIdAsync(id);
        if (map == null) throw new KeyNotFoundException($"QuestionAnswerMap with ID {id} not found");

        _mapper.Map(dto, map);
        map.UpdatedDate = DateTime.UtcNow;
        var updated = await _repository.UpdateAsync(map);
        return _mapper.Map<QuestionAnswerMapDto>(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }
}
