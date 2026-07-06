#nullable disable
using System;
using System.Collections.Generic;

namespace CrackJobs.Domain;

public partial class Like
{
    public long Id { get; set; }

    public string UserId { get; set; }

    /// <summary>question | answer | comment</summary>
    public string TargetType { get; set; }

    public long TargetId { get; set; }

    public DateTime? CreatedDate { get; set; }
}
