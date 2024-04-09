using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class TestTemplate
{
    public int TestTemplateId { get; set; }

    public string Year { get; set; } = null!;

    public int TestTypeId { get; set; }

    public int SubjectId { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual Subject? Subject { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual TestType? TestType { get; set; }
}
