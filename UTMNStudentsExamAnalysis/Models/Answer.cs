using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class Answer
{
    public Guid AnswerId { get; set; }

    public int ResultId { get; set; }

    public string PartNumber { get; set; } = null!;

    public string? NumberInPart { get; set; }

    public int Points { get; set; }
}
