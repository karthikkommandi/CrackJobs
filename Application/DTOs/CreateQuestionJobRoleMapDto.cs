namespace CrackJobs.Application.DTOs;

public class CreateQuestionJobRoleMapDto
{
    public long Qid { get; set; }
    public int RoleId { get; set; }
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; }
}
