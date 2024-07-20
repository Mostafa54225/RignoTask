using RingoTask.Models;

namespace RingoTask.IRepositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetSubDepartments(int departmentId);
        Task<IEnumerable<Department>> GetParentDepartments(int departmentId);
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartmentWithSubDepartments(int departmentId);

    }
}
