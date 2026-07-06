namespace CrackJobs.Application.DTOs;

public class QuestionTechnologyMapDto
{
    public int Id { get; set; }
    public long Qid { get; set; }
    public int TechId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public byte[] CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdateBy { get; set; }
}
