namespace CrackJobs.Application.DTOs;

public class CreateQuestionCompanyMapDto
{
    public long Qid { get; set; }
    public int CompId { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
}
