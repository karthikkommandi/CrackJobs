namespace CrackJobs.Application.DTOs;

public class ToggleLikeDto
{
    public string UserId { get; set; }
    /// <summary>question | answer | comment</summary>
    public string TargetType { get; set; }
    public long TargetId { get; set; }
}

public class LikeStatusDto
{
    public bool Liked { get; set; }
    public long Count { get; set; }
}
