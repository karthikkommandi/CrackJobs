using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class TechnologyService : ITechnologyService
{
    private readonly ITechnologyRepository _repository;
    private readonly IMapper _mapper;

    public TechnologyService(ITechnologyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<TechnologyDto>> GetAllTechnologiesAsync()
    {
        var technologies = await _repository.GetAllAsync();
        return _mapper.Map<List<TechnologyDto>>(technologies);
    }

    public async Task<TechnologyDto?> GetTechnologyByIdAsync(int id)
    {
        var technology = await _repository.GetByIdAsync(id);
        return technology == null ? null : _mapper.Map<TechnologyDto>(technology);
    }

    public async Task<TechnologyDto> CreateTechnologyAsync(CreateTechnologyDto dto)
    {
        var technology = _mapper.Map<Technology>(dto);
        technology.CreatedDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(technology);
        return _mapper.Map<TechnologyDto>(created);
    }

    public async Task<TechnologyDto> UpdateTechnologyAsync(int id, UpdateTechnologyDto dto)
    {
        var technology = await _repository.GetByIdAsync(id);
        if (technology == null) throw new KeyNotFoundException($"Technology with ID {id} not found");

        _mapper.Map(dto, technology);
        technology.UpdatedDate = DateTime.UtcNow;
        var updated = await _repository.UpdateAsync(technology);
        return _mapper.Map<TechnologyDto>(updated);
    }

    public async Task<bool> DeleteTechnologyAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
