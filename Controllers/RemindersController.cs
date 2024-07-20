using Microsoft.AspNetCore.Mvc;
using RingoTask.IRepositories;
using RingoTask.Models;

namespace RingoTask.Controllers
{
    public class RemindersController : Controller
    {
        private readonly IReminderRepository _reminderRepository;

        public RemindersController(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                await _reminderRepository.AddReminder(reminder);
                return RedirectToAction("Index");
            }
            return View(reminder);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reminders = await _reminderRepository.GetPendingReminders();
            return View(reminders);
        }
    }
}
