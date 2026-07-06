using CrackJobs.Application.DTOs;

namespace CrackJobs.Application.Interfaces;

public interface ITechnologyService
{
    Task<List<TechnologyDto>> GetAllTechnologiesAsync();
    Task<TechnologyDto?> GetTechnologyByIdAsync(int id);
    Task<TechnologyDto> CreateTechnologyAsync(CreateTechnologyDto dto);
    Task<TechnologyDto> UpdateTechnologyAsync(int id, UpdateTechnologyDto dto);
    Task<bool> DeleteTechnologyAsync(int id);
}
