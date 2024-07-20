using System;
using System.Collections.Generic;

namespace RingoTask.Models;

public partial class Reminder
{
    public int ReminderId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime ReminderDateTime { get; set; }

    public string Email { get; set; } = null!;
}
