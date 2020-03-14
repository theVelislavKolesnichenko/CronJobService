using System;
using System.Collections.Generic;
using System.Text;

namespace CronJobService.Domain
{
    public class TaskSettings
    {
        public bool StopTask { get; set; }

        public DateTime LastExecutionDate { get; set; }

        public double Hours { get; set; }

        public int Interval { get; set; }

        public bool StartImmediately { get; set; }

        public bool SendNotificationEmail { get; set; }
    }
}
