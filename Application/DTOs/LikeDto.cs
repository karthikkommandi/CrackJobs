namespace CrackJobs.Application.DTOs;

public class LikeDto
{
    public long Id { get; set; }
    public string UserId { get; set; }
    public string TargetType { get; set; }
    public long TargetId { get; set; }
    public DateTime? CreatedDate { get; set; }
}
