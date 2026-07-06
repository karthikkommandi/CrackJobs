#nullable disable
using System;
using System.Collections.Generic;

namespace CrackJobs.Domain;

public partial class JobRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; }

    public string RoleDescription { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }
}
