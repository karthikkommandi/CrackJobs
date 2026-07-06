using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _repository;
    private readonly IMapper _mapper;

    public RatingService(IRatingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<RatingDto>> GetAllRatingsAsync()
    {
        var ratings = await _repository.GetAllAsync();
        return _mapper.Map<List<RatingDto>>(ratings);
    }

    public async Task<RatingDto?> GetRatingByIdAsync(int id)
    {
        var rating = await _repository.GetByIdAsync(id);
        return rating == null ? null : _mapper.Map<RatingDto>(rating);
    }

    public async Task<RatingDto> CreateRatingAsync(CreateRatingDto dto)
    {
        var rating = _mapper.Map<Rating>(dto);
        var created = await _repository.AddAsync(rating);
        return _mapper.Map<RatingDto>(created);
    }

    public async Task<RatingDto> UpdateRatingAsync(int id, UpdateRatingDto dto)
    {
        var rating = await _repository.GetByIdAsync(id);
        if (rating == null) throw new KeyNotFoundException($"Rating with ID {id} not found");

        _mapper.Map(dto, rating);
        var updated = await _repository.UpdateAsync(rating);
        return _mapper.Map<RatingDto>(updated);
    }

    public async Task<bool> DeleteRatingAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
