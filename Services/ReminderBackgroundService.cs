using RingoTask.IRepositories;

namespace RingoTask.Services
{
    public class ReminderBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReminderBackgroundService> _logger;
        private readonly IEmailService _emailService;

        public ReminderBackgroundService(IServiceProvider serviceProvider, ILogger<ReminderBackgroundService> logger, IEmailService emailService)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var reminderRepository = scope.ServiceProvider.GetRequiredService<IReminderRepository>();

                    var pendingReminders = await reminderRepository.GetPendingReminders();
                    foreach (var reminder in pendingReminders)
                    {
                        try
                        {
                            await _emailService.SendEmailAsync(reminder.Email, "Reminder: " + reminder.Title, $"This is a reminder for: {reminder.Title}");
                            await reminderRepository.RemoveReminder(reminder.ReminderId);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error sending email for reminder ID: {ReminderID}", reminder.ReminderId);
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Check every minute
            }
        }
    }
}
