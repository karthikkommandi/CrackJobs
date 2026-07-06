using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public AnswerService(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<AnswerDto>> GetAllAnswersAsync()
    {
        var answers = await _repository.GetAllAsync();
        return _mapper.Map<List<AnswerDto>>(answers);
    }

    public async Task<AnswerDto?> GetAnswerByIdAsync(long id)
    {
        var answer = await _repository.GetByIdAsync(id);
        return answer == null ? null : _mapper.Map<AnswerDto>(answer);
    }

    public async Task<AnswerDto> CreateAnswerAsync(CreateAnswerDto dto)
    {
        var answer = _mapper.Map<Answer>(dto);
        answer.CreatedDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(answer);
        return _mapper.Map<AnswerDto>(created);
    }

    public async Task<AnswerDto> UpdateAnswerAsync(long id, UpdateAnswerDto dto)
    {
        var answer = await _repository.GetByIdAsync(id);
        if (answer == null) throw new KeyNotFoundException($"Answer with ID {id} not found");

        _mapper.Map(dto, answer);
        answer.UpdatedDate = DateTime.UtcNow;
        var updated = await _repository.UpdateAsync(answer);
        return _mapper.Map<AnswerDto>(updated);
    }

    public async Task<bool> DeleteAnswerAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }
}
