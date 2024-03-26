using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public int? UserId { get; set; }

    public int? SchoolCode { get; set; }

    public int? AreaId { get; set; }

    public int? ReportDataId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual ReportsData? ReportData { get; set; }

    public virtual School? SchoolCodeNavigation { get; set; }
}
