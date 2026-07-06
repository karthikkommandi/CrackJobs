namespace CrackJobs.Application.DTOs;

public class CreateCommentDto
{
    public string Comment1 { get; set; }
    public string UserId { get; set; }
    public long? ParrentCommentId { get; set; }
    public long? QuestionId { get; set; }
}
