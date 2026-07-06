using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Domain;

namespace CrackJobs.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Question mappings
        CreateMap<Questinon, QuestionDto>().ReverseMap();
        CreateMap<CreateQuestionDto, Questinon>();
        CreateMap<UpdateQuestionDto, Questinon>();

        // Answer mappings
        CreateMap<Answer, AnswerDto>().ReverseMap();
        CreateMap<CreateAnswerDto, Answer>();
        CreateMap<UpdateAnswerDto, Answer>();

        // Comment mappings
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<UpdateCommentDto, Comment>();

        // Company mappings
        CreateMap<Company, CompanyDto>().ReverseMap();
        CreateMap<CreateCompanyDto, Company>();
        CreateMap<UpdateCompanyDto, Company>();

        // Technology mappings
        CreateMap<Technology, TechnologyDto>().ReverseMap();
        CreateMap<CreateTechnologyDto, Technology>();
        CreateMap<UpdateTechnologyDto, Technology>();

        // Rating mappings
        CreateMap<Rating, RatingDto>().ReverseMap();
        CreateMap<CreateRatingDto, Rating>();
        CreateMap<UpdateRatingDto, Rating>();

        // QuestionAnswerMap mappings
        CreateMap<QuestionAnswerMap, QuestionAnswerMapDto>().ReverseMap();
        CreateMap<CreateQuestionAnswerMapDto, QuestionAnswerMap>();
        CreateMap<UpdateQuestionAnswerMapDto, QuestionAnswerMap>();

        // QuestionCompanyMap mappings
        CreateMap<QuestionCompanyMap, QuestionCompanyMapDto>().ReverseMap();
        CreateMap<CreateQuestionCompanyMapDto, QuestionCompanyMap>();
        CreateMap<UpdateQuestionCompanyMapDto, QuestionCompanyMap>();

        // QuestionTechnologyMap mappings
        CreateMap<QuestionTechnologyMap, QuestionTechnologyMapDto>().ReverseMap();
        CreateMap<CreateQuestionTechnologyMapDto, QuestionTechnologyMap>();
        CreateMap<UpdateQuestionTechnologyMapDto, QuestionTechnologyMap>();

        // JobRole mappings
        CreateMap<JobRole, JobRoleDto>().ReverseMap();
        CreateMap<CreateJobRoleDto, JobRole>();
        CreateMap<UpdateJobRoleDto, JobRole>();

        // QuestionJobRoleMap mappings
        CreateMap<QuestionJobRoleMap, QuestionJobRoleMapDto>().ReverseMap();
        CreateMap<CreateQuestionJobRoleMapDto, QuestionJobRoleMap>();
        CreateMap<UpdateQuestionJobRoleMapDto, QuestionJobRoleMap>();

        // Like mappings
        CreateMap<Like, LikeDto>().ReverseMap();
    }
}
