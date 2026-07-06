namespace CrackJobs.Application.DTOs;

public class UpdateCommentDto
{
    public string Comment1 { get; set; }
    public long? LikeCount { get; set; }
    public long? ReplyCount { get; set; }
    public bool? IsDeleted { get; set; }
}
