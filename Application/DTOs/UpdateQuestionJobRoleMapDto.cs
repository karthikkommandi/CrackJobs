namespace CrackJobs.Application.DTOs;

public class UpdateQuestionJobRoleMapDto
{
    public long Qid { get; set; }
    public int RoleId { get; set; }
    public bool IsActive { get; set; }
    public string UpdatedBy { get; set; }
}
