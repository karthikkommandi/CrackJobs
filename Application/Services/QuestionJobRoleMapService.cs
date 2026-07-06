using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class QuestionJobRoleMapService : IQuestionJobRoleMapService
{
    private readonly IQuestionJobRoleMapRepository _repository;
    private readonly IMapper _mapper;

    public QuestionJobRoleMapService(IQuestionJobRoleMapRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<QuestionJobRoleMapDto>> GetAllAsync()
    {
        var maps = await _repository.GetAllAsync();
        return _mapper.Map<List<QuestionJobRoleMapDto>>(maps);
    }

    public async Task<QuestionJobRoleMapDto?> GetByIdAsync(long id)
    {
        var map = await _repository.GetByIdAsync(id);
        return map == null ? null : _mapper.Map<QuestionJobRoleMapDto>(map);
    }

    public async Task<QuestionJobRoleMapDto> CreateAsync(CreateQuestionJobRoleMapDto dto)
    {
        var map = _mapper.Map<QuestionJobRoleMap>(dto);
        map.CreatedDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(map);
        return _mapper.Map<QuestionJobRoleMapDto>(created);
    }

    public async Task<QuestionJobRoleMapDto> UpdateAsync(long id, UpdateQuestionJobRoleMapDto dto)
    {
        var map = await _repository.GetByIdAsync(id);
        if (map == null) throw new KeyNotFoundException($"QuestionJobRoleMap with ID {id} not found");

        _mapper.Map(dto, map);
        map.UpdatedDate = DateTime.UtcNow;
        var updated = await _repository.UpdateAsync(map);
        return _mapper.Map<QuestionJobRoleMapDto>(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }
}
