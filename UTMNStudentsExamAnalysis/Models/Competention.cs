using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class Competention
{
    public int CompetentionId { get; set; }

    public string CompetitionText { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
