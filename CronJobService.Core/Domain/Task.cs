using System.Collections.Generic;

namespace CronJobService.Domain
{
    public class Task
    {
        public Task()
        {
            this.TasksToSettings = new HashSet<TasksToSetting>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public virtual ICollection<TasksToSetting> TasksToSettings { get; set; }

    }
}
