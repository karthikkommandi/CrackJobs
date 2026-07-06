namespace CrackJobs.Application.DTOs;

public class AnswerDto
{
    public long Aid { get; set; }
    public string Answer1 { get; set; }
    public string AnswerReference { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public long LikeCount { get; set; }
}
