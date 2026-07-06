using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class JobRoleService : IJobRoleService
{
    private readonly IJobRoleRepository _repository;
    private readonly IMapper _mapper;

    public JobRoleService(IJobRoleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<JobRoleDto>> GetAllJobRolesAsync()
    {
        var roles = await _repository.GetAllAsync();
        return _mapper.Map<List<JobRoleDto>>(roles);
    }

    public async Task<JobRoleDto?> GetJobRoleByIdAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);
        return role == null ? null : _mapper.Map<JobRoleDto>(role);
    }

    public async Task<JobRoleDto> CreateJobRoleAsync(CreateJobRoleDto dto)
    {
        var role = _mapper.Map<JobRole>(dto);
        role.CreatedDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(role);
        return _mapper.Map<JobRoleDto>(created);
    }

    public async Task<JobRoleDto> UpdateJobRoleAsync(int id, UpdateJobRoleDto dto)
    {
        var role = await _repository.GetByIdAsync(id);
        if (role == null) throw new KeyNotFoundException($"JobRole with ID {id} not found");

        _mapper.Map(dto, role);
        role.UpdatedDate = DateTime.UtcNow;
        var updated = await _repository.UpdateAsync(role);
        return _mapper.Map<JobRoleDto>(updated);
    }

    public async Task<bool> DeleteJobRoleAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
