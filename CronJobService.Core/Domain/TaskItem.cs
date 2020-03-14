using CronJobService.Domain.Enums;
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Threading;

namespace CronJobService.Domain
{
    public class TaskItem
    {
        #region Properties

        [Newtonsoft.Json.JsonIgnore]
        public CultureInfo GermanCulture => CultureInfo.CreateSpecificCulture("bg-BG");

        public DateTime NextExecutionDate { get; set; }

        private TasksName taskName { get; }
        public TasksName TaskName => taskName;

        public string CustomServiceName => taskName.ToString();

        private string logFileName = null;

        public string LogFileName
        {
            get
            {
                if (string.IsNullOrEmpty(logFileName))
                {
                    logFileName = System.IO.Path.Combine("logs", $"{taskName}Log.txt");
                }
                return logFileName;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public Timer JobTimer { get; set; }

        public bool InProgress { get; set; }

        public TaskSettings TaskSettings { get; set; }

        #endregion

        #region Public Methods

        public TaskItem() { }

        public TaskItem(TasksName name)
        {
            taskName = name;
        }

        #endregion
    }
}
