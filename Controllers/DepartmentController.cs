using Microsoft.AspNetCore.Mvc;
using RingoTask.IRepositories; 
using RingoTask.ViewModels;

namespace RingoTask.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> SubDepartments(int id)
        {
            var department = await _departmentRepository.GetDepartmentWithSubDepartments(id);
            if (department == null)
            {
                return NotFound();
            }

            var parentHierarchy = await _departmentRepository.GetParentDepartments(id);
            var viewModel = new DepartmentDetailsViewModel
            {
                Department = department,
                ParentHierarchy = parentHierarchy
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Index()
        {
            var parentDepartments = await _departmentRepository.GetDepartments();
            return View(parentDepartments);
        }

    }
}
