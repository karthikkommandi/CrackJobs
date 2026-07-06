using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface IJobRoleService
{
    Task<List<JobRoleDto>> GetAllJobRolesAsync();
    Task<JobRoleDto?> GetJobRoleByIdAsync(int id);
    Task<JobRoleDto> CreateJobRoleAsync(CreateJobRoleDto dto);
    Task<JobRoleDto> UpdateJobRoleAsync(int id, UpdateJobRoleDto dto);
    Task<bool> DeleteJobRoleAsync(int id);
}
