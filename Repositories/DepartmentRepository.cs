using Microsoft.EntityFrameworkCore;
using RingoTask.IRepositories;
using RingoTask.Models;

namespace RingoTask.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly RingoTaskContext _context;

        public DepartmentRepository(RingoTaskContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetSubDepartments(int departmentId)
        {
            var departments = await _context.Departments.ToListAsync();
            return GetSubDepartmentsRecursive(departments, departmentId);
        }

        private IEnumerable<Department> GetSubDepartmentsRecursive(IEnumerable<Department> departments, int parentId)
        {
            var result = new List<Department>();
            var subDepartments = departments.Where(d => d.ParentDepartmentId == parentId).ToList();

            foreach (var department in subDepartments)
            {
                result.Add(department);
                result.AddRange(GetSubDepartmentsRecursive(departments, department.DepartmentId));
            }

            return result;
        }

        public async Task<IEnumerable<Department>> GetParentDepartments(int departmentId)
        {
            var departments = await _context.Departments.ToListAsync();
            return GetParentDepartmentsRecursive(departments, departmentId);
        }

        private IEnumerable<Department> GetParentDepartmentsRecursive(IEnumerable<Department> departments, int departmentId)
        {
            var result = new List<Department>();
            var department = departments.FirstOrDefault(d => d.DepartmentId == departmentId);

            while (department != null && department.ParentDepartmentId != null)
            {
                department = departments.FirstOrDefault(d => d.DepartmentId == department.ParentDepartmentId);
                if (department != null)
                {
                    result.Insert(0, department); // Insert at the beginning to maintain the hierarchy order
                }
            }

            return result;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            var departments = await _context.Departments.Where(x => x.ParentDepartmentId == null).ToListAsync();
            return departments;
        }
        public async Task<Department> GetDepartmentWithSubDepartments(int departmentId)
        {
            var department = await _context.Departments
                                           .Include(d => d.InverseParentDepartment)
                                           .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
            return department;
        }
    }
}
