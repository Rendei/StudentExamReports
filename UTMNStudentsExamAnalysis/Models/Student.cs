using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class Student
{
    public Guid StudentId { get; set; }

    public string Class { get; set; } = null!;

    public string? Sex { get; set; }

    public int SchoolCode { get; set; }

    public int? StudentCategory { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual School SchoolCodeNavigation { get; set; } = null!;

    public virtual StudentCategory? StudentCategoryNavigation { get; set; }
}
