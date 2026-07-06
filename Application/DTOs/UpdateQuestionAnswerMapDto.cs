namespace CrackJobs.Application.DTOs;

public class UpdateQuestionAnswerMapDto
{
    public bool IsActive { get; set; }
    public long RatingId { get; set; }
    public long CommentId { get; set; }
    public string UpdatedBy { get; set; }
}
