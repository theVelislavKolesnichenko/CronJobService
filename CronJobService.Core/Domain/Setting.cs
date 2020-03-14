using System.Collections.Generic;

namespace CronJobService.Domain
{
    public class Setting
    {
        public Setting()
        {
            this.TasksToSettings = new HashSet<TasksToSetting>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsSingleValue { get; set; }
        public string ValueType { get; set; }

        ///<summary>Property for foreign key FK_TasksToSettings_Settings</summary>
        public virtual ICollection<TasksToSetting> TasksToSettings { get; set; }

    }
}
