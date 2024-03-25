using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string TaskNumber { get; set; } = null!;

    public string? Criteria { get; set; }

    public string Difficulty { get; set; } = null!;

    public int TotalPoints { get; set; }

    public string PartNumber { get; set; } = null!;

    public string? NumberInPart { get; set; }

    public int CompetitionId { get; set; }

    public int? TestTemplateId { get; set; }

    public virtual Competention Competition { get; set; } = null!;

    public virtual TestTemplate? TestTemplate { get; set; }
}
