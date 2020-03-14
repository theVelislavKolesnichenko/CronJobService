using System;
using System.Collections.Generic;
using System.Text;

namespace CronJobService.Core
{
    public abstract class ScheduledTaskMonthly : TaskBasic
    {
        public override DateTime GetNextExecutionDate(DateTime nextDate)
        {
            return nextDate.Date.AddMonths(TaskItem.TaskSettings.Interval).AddHours(TaskItem.TaskSettings.Hours);
        }
    }
}
