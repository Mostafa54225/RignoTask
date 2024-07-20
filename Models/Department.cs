using System;
using System.Collections.Generic;

namespace RingoTask.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Logo { get; set; }

    public int? ParentDepartmentId { get; set; }

    public virtual ICollection<Department> InverseParentDepartment { get; set; } = new List<Department>();

    public virtual Department? ParentDepartment { get; set; }
}
