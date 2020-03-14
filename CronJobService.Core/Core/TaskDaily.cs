using System;
using System.Collections.Generic;
using System.Text;

namespace CronJobService.Core
{
    public abstract class TaskDaily : TaskBasic
    {
        public override DateTime GetNextExecutionDate(DateTime lastExecutionDate)
        {
            return lastExecutionDate.Date.AddDays(TaskItem.TaskSettings.Interval).AddHours(TaskItem.TaskSettings.Hours);
        }
    }
}
