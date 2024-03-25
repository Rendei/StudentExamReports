using System;
using System.Collections.Generic;

namespace UTMNStudentsExamAnalysis.Models;

public partial class StudentCategory
{
    public int StudentCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
