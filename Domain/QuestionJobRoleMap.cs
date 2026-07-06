#nullable disable
using System;
using System.Collections.Generic;

namespace CrackJobs.Domain;

public partial class QuestionJobRoleMap
{
    public long Id { get; set; }

    public long Qid { get; set; }

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }
}
