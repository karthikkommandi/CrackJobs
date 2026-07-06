namespace CrackJobs.Application.DTOs;

public class CreateQuestionTechnologyMapDto
{
    public long Qid { get; set; }
    public int TechId { get; set; }
    public bool IsActive { get; set; }
    public byte[] CreatedBy { get; set; }
}
