namespace CrackJobs.Application.DTOs;

public class CommentDto
{
    public long Id { get; set; }
    public string Comment1 { get; set; }
    public string UserId { get; set; }
    public long? ParrentCommentId { get; set; }
    public long? LikeCount { get; set; }
    public long? ReplyCount { get; set; }
    public bool? IsDeleted { get; set; }
    public long? QuestionId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public List<CommentDto> Replies { get; set; } = new();
}
