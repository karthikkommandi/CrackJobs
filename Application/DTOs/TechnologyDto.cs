namespace CrackJobs.Application.DTOs;

public class TechnologyDto
{
    public int TechId { get; set; }
    public string TechName { get; set; }
    public string TechDescription { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
}
