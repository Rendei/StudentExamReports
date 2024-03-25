using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class TestType
{
    public int TestTypeId { get; set; }

    public string TestTypeName { get; set; } = null!;

    public virtual ICollection<TestTemplate> TestTemplates { get; set; } = new List<TestTemplate>();
}
