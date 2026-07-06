namespace CrackJobs.Application.DTOs;

public class QuestionCompanyMapDto
{
    public long Id { get; set; }
    public long Qid { get; set; }
    public int CompId { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
