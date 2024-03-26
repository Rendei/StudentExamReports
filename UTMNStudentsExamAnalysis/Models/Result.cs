using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class Result
{
    public int ResultId { get; set; }

    public int PrimaryPoints { get; set; }

    public int FirstPartPrimaryPoints { get; set; }

    public int SecondPartPrimaryPoints { get; set; }

    public int ThirdPartPrimaryPoints { get; set; }

    public int? Mark { get; set; }

    public int CompletionPercent { get; set; }

    public int SecondaryPoints { get; set; }

    public Guid StudentId { get; set; }

    public int? TestTemplateId { get; set; }

    public string? FirstPartAnswers { get; set; }

    public string? SecondPartAnswers { get; set; }

    public string? ThirdPartAnswers { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual TestTemplate? TestTemplate { get; set; }
}
