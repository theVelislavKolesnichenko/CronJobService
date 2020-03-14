using System;
using System.Collections.Generic;
using System.Text;

namespace CronJobService.Core
{
    public abstract class ScheduledTaskRegular : TaskBasic
    {
        public override DateTime GetNextExecutionDate(DateTime nextDate)
        {
            return nextDate.AddMinutes(TaskItem.TaskSettings.Interval);
        }
    }
}
