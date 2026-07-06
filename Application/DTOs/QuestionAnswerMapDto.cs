namespace CrackJobs.Application.DTOs;

public class QuestionAnswerMapDto
{
    public long Id { get; set; }
    public long Qid { get; set; }
    public long Aid { get; set; }
    public bool IsActive { get; set; }
    public long RatingId { get; set; }
    public long CommentId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
