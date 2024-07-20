using RingoTask.Models;

namespace RingoTask.IRepositories
{
    public interface IReminderRepository
    {
        Task AddReminder(Reminder reminder);
        Task<IEnumerable<Reminder>> GetPendingReminders();
        Task RemoveReminder(int reminderId);
    }
}
