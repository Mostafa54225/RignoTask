using RingoTask.Models;

namespace RingoTask.ViewModels
{
    public class DepartmentDetailsViewModel
    {
        public Department Department { get; set; }
        public IEnumerable<Department> ParentHierarchy { get; set; }
    }
}
