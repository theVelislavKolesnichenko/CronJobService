using System;
using System.Collections.Generic;
using System.Text;

namespace CronJobService.Domain
{
    public class TasksToSetting
    {
        public int ID { get; set; }
        public int Tasks_ID { get; set; }
        public int Settings_ID { get; set; }
        public string Value { get; set; }

        ///<summary>Property for foreign key FK_TasksToSettings_Settings</summary>
        public virtual Setting Setting { get; set; }

        ///<summary>Property for foreign key FK_TasksToSettings_Tasks</summary>
        public virtual Task Task { get; set; }
    }
}
