using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class SchoolKind
{
    public int SchoolKindId { get; set; }

    public string SchoolKindName { get; set; } = null!;

    public virtual ICollection<School> Schools { get; set; } = new List<School>();
}
