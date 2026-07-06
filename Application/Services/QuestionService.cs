using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;

namespace CrackJobs.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _repository;
    private readonly IQuestionCompanyMapService _questionCompanyMapService;
    private readonly IQuestionAnswerMapService _questionAnswerMapService;
    private readonly IQuestionTechnologyMapService _questionTechnologyMapService;
    private readonly IQuestionJobRoleMapService _questionJobRoleMapService;
    private readonly ICompanyService _companyService;
    private readonly IAnswerService _answerService;
    private readonly ICommentService _commentService;
    private readonly ITechnologyService _technologyService;
    private readonly IJobRoleService _jobRoleService;
    private readonly ILikeService _likeService;
    private readonly IMapper _mapper;

    public QuestionService(
        IQuestionRepository repository,
        IQuestionCompanyMapService questionCompanyMapService,
        IQuestionAnswerMapService questionAnswerMapService,
        IQuestionTechnologyMapService questionTechnologyMapService,
        IQuestionJobRoleMapService questionJobRoleMapService,
        ICompanyService companyService,
        IAnswerService answerService,
        ICommentService commentService,
        ITechnologyService technologyService,
        IJobRoleService jobRoleService,
        ILikeService likeService,
        IMapper mapper)
    {
        _repository = repository;
        _questionCompanyMapService = questionCompanyMapService;
        _questionAnswerMapService = questionAnswerMapService;
        _questionTechnologyMapService = questionTechnologyMapService;
        _questionJobRoleMapService = questionJobRoleMapService;
        _companyService = companyService;
        _answerService = answerService;
        _commentService = commentService;
        _technologyService = technologyService;
        _jobRoleService = jobRoleService;
        _likeService = likeService;
        _mapper = mapper;
    }

    public async Task<List<QuestionDto>> GetAllQuestionsAsync()
    {
        var questions = await _repository.GetAllAsync();
        var dtos = _mapper.Map<List<QuestionDto>>(questions);
        await PopulateRelatedDataAsync(dtos, includeComments: false);
        return dtos;
    }

    public async Task<QuestionDto?> GetQuestionByIdAsync(long id)
    {
        var question = await _repository.GetByIdAsync(id);
        if (question == null) return null;

        var dto = _mapper.Map<QuestionDto>(question);
        await PopulateRelatedDataAsync(new List<QuestionDto> { dto }, includeComments: true);
        return dto;
    }

    public async Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto dto)
    {
        var question = _mapper.Map<Questinon>(dto);
        question.CreatedDate = DateTime.UtcNow;
        var created = await _repository.AddAsync(question);

        // Loop over the supplied map collections and persist each row against the
        // newly created question. The client-supplied Qid (if any) is overwritten.
        foreach (var companyMap in dto.CompanyMaps ?? new List<CreateQuestionCompanyMapDto>())
        {
            companyMap.Qid = created.Qid;
            await _questionCompanyMapService.CreateAsync(companyMap);
        }

        foreach (var answerMap in dto.AnswerMaps ?? new List<CreateQuestionAnswerMapDto>())
        {
            answerMap.Qid = created.Qid;
            await _questionAnswerMapService.CreateAsync(answerMap);
        }

        foreach (var technologyMap in dto.TechnologyMaps ?? new List<CreateQuestionTechnologyMapDto>())
        {
            technologyMap.Qid = created.Qid;
            await _questionTechnologyMapService.CreateAsync(technologyMap);
        }

        foreach (var roleMap in dto.RoleMaps ?? new List<CreateQuestionJobRoleMapDto>())
        {
            roleMap.Qid = created.Qid;
            await _questionJobRoleMapService.CreateAsync(roleMap);
        }

        var result = _mapper.Map<QuestionDto>(created);
        await PopulateRelatedDataAsync(new List<QuestionDto> { result }, includeComments: true);
        return result;
    }

    public async Task<QuestionDto> UpdateQuestionAsync(long id, UpdateQuestionDto dto)
    {
        var question = await _repository.GetByIdAsync(id);
        if (question == null) throw new KeyNotFoundException($"Question with ID {id} not found");

        _mapper.Map(dto, question);
        question.UpdatedDate = DateTime.UtcNow;
        var updated = await _repository.UpdateAsync(question);
        return _mapper.Map<QuestionDto>(updated);
    }

    public async Task<bool> DeleteQuestionAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }

    // The Question entity has no navigation properties to companies, answers, roles etc.;
    // those relationships live in the map tables. Resolve them here so the DTO carries the
    // related data instead of returning empty collections. Comments (which can be numerous)
    // are only loaded for the single-question detail view.
    private async Task PopulateRelatedDataAsync(List<QuestionDto> questions, bool includeComments)
    {
        if (questions.Count == 0) return;

        var companyMaps = await _questionCompanyMapService.GetAllAsync();
        var answerMaps = await _questionAnswerMapService.GetAllAsync();
        var technologyMaps = await _questionTechnologyMapService.GetAllAsync();
        var roleMaps = await _questionJobRoleMapService.GetAllAsync();

        var companies = (await _companyService.GetAllCompaniesAsync())
            .GroupBy(c => c.Id).ToDictionary(g => g.Key, g => g.First());
        var answers = (await _answerService.GetAllAnswersAsync())
            .GroupBy(a => a.Aid).ToDictionary(g => g.Key, g => g.First());
        var technologies = (await _technologyService.GetAllTechnologiesAsync())
            .GroupBy(t => t.TechId).ToDictionary(g => g.Key, g => g.First());
        var roles = (await _jobRoleService.GetAllJobRolesAsync())
            .GroupBy(r => r.RoleId).ToDictionary(g => g.Key, g => g.First());

        foreach (var question in questions)
        {
            question.Companies = companyMaps
                .Where(m => m.Qid == question.Qid && m.IsActive)
                .Where(m => companies.ContainsKey(m.CompId))
                .Select(m => companies[m.CompId])
                .ToList();

            question.Answers = answerMaps
                .Where(m => m.Qid == question.Qid && m.IsActive)
                .Where(m => answers.ContainsKey(m.Aid))
                .Select(m => answers[m.Aid])
                .ToList();

            // Attach like counts to each answer.
            foreach (var answer in question.Answers)
            {
                answer.LikeCount = await _likeService.GetCountAsync("answer", answer.Aid);
            }

            question.Technologies = technologyMaps
                .Where(m => m.Qid == question.Qid && m.IsActive)
                .Where(m => technologies.ContainsKey(m.TechId))
                .Select(m => technologies[m.TechId])
                .ToList();

            question.JobRoles = roleMaps
                .Where(m => m.Qid == question.Qid && m.IsActive)
                .Where(m => roles.ContainsKey(m.RoleId))
                .Select(m => roles[m.RoleId])
                .ToList();

            question.LikeCount = await _likeService.GetCountAsync("question", question.Qid);

            question.Comments = includeComments
                ? await _commentService.GetByQuestionAsync(question.Qid)
                : new List<CommentDto>();
        }
    }
}
