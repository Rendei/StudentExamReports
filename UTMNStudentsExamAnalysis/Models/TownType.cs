using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class TownType
{
    public int TownTypeId { get; set; }

    public string TownTypeName { get; set; } = null!;

    public virtual ICollection<School> Schools { get; set; } = new List<School>();
}
