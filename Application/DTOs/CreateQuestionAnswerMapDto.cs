namespace CrackJobs.Application.DTOs;

public class CreateQuestionAnswerMapDto
{
    public long Qid { get; set; }
    public long Aid { get; set; }
    public bool IsActive { get; set; }
    public long RatingId { get; set; }
    public long CommentId { get; set; }
    public string CreatedBy { get; set; }
}
