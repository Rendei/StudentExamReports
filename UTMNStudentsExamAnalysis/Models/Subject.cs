using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public virtual ICollection<TestTemplate> TestTemplates { get; set; } = new List<TestTemplate>();
}
