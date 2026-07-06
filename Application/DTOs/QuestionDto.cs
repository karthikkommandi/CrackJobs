namespace CrackJobs.Application.DTOs;

public class QuestionDto
{
    public long Qid { get; set; }
    public string Qname { get; set; }
    public string Qdesc { get; set; }
    public List<CompanyDto> Companies { get; set; }
    public List<AnswerDto> Answers { get; set; }
    public List<CommentDto> Comments { get; set; }
    public List<TechnologyDto> Technologies { get; set; }
    public List<JobRoleDto> JobRoles { get; set; }
    public long LikeCount { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
}
