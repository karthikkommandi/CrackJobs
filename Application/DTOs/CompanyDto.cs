namespace CrackJobs.Application.DTOs;

public class CompanyDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string CompanyDescription { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
}
