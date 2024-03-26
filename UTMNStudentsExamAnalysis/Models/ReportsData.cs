using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class ReportsData
{
    public int ReportsDataId { get; set; }

    public int[]? SchoolIds { get; set; }

    public int[]? ClassIds { get; set; }

    public int[]? TypeIds { get; set; }

    public int[]? SubjectIds { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
