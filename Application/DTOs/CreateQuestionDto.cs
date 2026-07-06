namespace CrackJobs.Application.DTOs;

public class CreateQuestionDto
{
    public string Qname { get; set; }
    public string Qdesc { get; set; }
    public string CreatedBy { get; set; }

    // Optional related map rows created together with the question.
    // The Qid on each entry is ignored/overwritten with the new question's id.
    public List<CreateQuestionCompanyMapDto> CompanyMaps { get; set; } = new();
    public List<CreateQuestionAnswerMapDto> AnswerMaps { get; set; } = new();
    public List<CreateQuestionTechnologyMapDto> TechnologyMaps { get; set; } = new();
    public List<CreateQuestionJobRoleMapDto> RoleMaps { get; set; } = new();
}
