using Microsoft.EntityFrameworkCore;
using RingoTask.IRepositories;
using RingoTask.Models;

namespace RingoTask.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly RingoTaskContext _context;

        public ReminderRepository(RingoTaskContext context)
        {
            _context = context;
        }
        public async Task AddReminder(Reminder reminder)
        {
            _context.Reminders.Add(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reminder>> GetPendingReminders()
        {
            return await _context.Reminders
                .Where(r => r.ReminderDateTime >= DateTime.Now)
                .ToListAsync();
        }

        public async Task RemoveReminder(int reminderId)
        {
            var reminder = await _context.Reminders.FindAsync(reminderId);
            if (reminder != null)
            {
                _context.Reminders.Remove(reminder);
                await _context.SaveChangesAsync();
            }
        }
    }
}
