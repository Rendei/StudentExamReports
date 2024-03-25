using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class School
{
    public int SchoolCode { get; set; }

    public string LawAddress { get; set; } = null!;

    public string? ShortName { get; set; }

    public int? TownTypeId { get; set; }

    public int? AreaId { get; set; }

    public int? SchoolKindId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual SchoolKind? SchoolKind { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual TownType? TownType { get; set; }
}
