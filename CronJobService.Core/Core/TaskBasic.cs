using CronJobService.Domain;
using CronJobService.Domain.Enums;
using CronJobService.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CronJobService.Core
{
    public abstract class TaskBasic
    {
        #region Properties

        public abstract TasksName TasksName { get; }

        public TaskItem TaskItem { get; set; }

        private readonly TimeSpan NegativeOneMilliSecond = new TimeSpan(0, 0, 0, 0, -1);

        #endregion

        #region Abstract Methods

        public abstract DateTime GetNextExecutionDate(DateTime lastExecutionDate);

        public abstract void RunTask();

        #endregion

        #region Private Methods
        private void InitializeTaskItem()
        {
            TaskItem = new TaskItem(TasksName);
            TaskItem.TaskSettings = CreateSettings(TasksName);
        }

        protected virtual TaskSettings LoadSettings(IEnumerable<TasksToSetting> tasksToSettings)
        {
            //ToDo: return new TaskSettings

            throw new NotImplementedException();
        }
        protected virtual TaskSettings CreateSettings(TasksName tasksName)
        {
            IEnumerable<TasksToSetting> tasksToSettings = null;//ToDo: read settings

            return LoadSettings(tasksToSettings);
        }
        protected virtual bool SaveSettings(bool saveSettings)
        {
            return !saveSettings; //ToDo: Update Settings
        }

        private Timer CreateTimer()
        {
            DateTime nowTime = DateTime.Now;
            TaskItem.NextExecutionDate = TaskItem.TaskSettings.LastExecutionDate;

            do
            {
                TaskItem.NextExecutionDate = GetNextExecutionDate(TaskItem.NextExecutionDate);
            }
            while (TaskItem.NextExecutionDate < DateTime.Now);

            TimeSpan dueTime = (TaskItem.NextExecutionDate - DateTime.Now);

            //ToDo: Log to EventLog

            return new Timer(OnElapsedJobTimer, null, dueTime, NegativeOneMilliSecond);// Specify negative one (-1) milliseconds to disable periodic signaling.
        }
        private void OnElapsedJobTimer(object state)
        {
            ExecuteTask(true);

            // Stop timer after job is completed - a new timer is created with the next execution time.
            //TaskItem.JobTimer.Dispose();
        }
        private void ExecuteTask(bool saveSettings)
        {
            TaskItem.TaskSettings = CreateSettings(TasksName);

            if (TaskItem.TaskSettings.StopTask)
            {
                //ToDo: Log to EventLog
            }
            else if (TaskItem.InProgress)
            {
                //ToDo: Log to EventLog
            }
            else
            {
                PrepareAndRunTask(saveSettings);
            }
        }
        private void PrepareAndRunTask(bool saveSettings)
        {
            Thread.CurrentThread.CurrentCulture = TaskItem.GermanCulture;

            TaskItem.InProgress = true;
            try
            {
                //ToDo: Log to EventLog Begin
                RunTask();
                //ToDo: Log to EventLog End
            }
            catch (Exception ex)
            {
                ////ToDo: Log to EventLog End

            }
            finally
            {
                TaskItem.InProgress = false;
                SaveSettings(saveSettings);
                InitializeJobTimer(false);
                SendNotificationEmail();
            }
        }
        private void InitializeJobTimer(bool runTask)
        {
            TaskItem.TaskSettings = CreateSettings(TasksName);

            if (runTask && TaskItem.TaskSettings.StartImmediately)
            {
                ExecuteTask(false);
            }
            else
            {
                if (TaskItem.JobTimer != null)
                {
                    // Stop timer after job is completed
                    TaskItem.JobTimer.Dispose(); // the timer period is -1 millisecond and that means no disable periodic signaling
                }
                TaskItem.JobTimer = CreateTimer();
            }
        }
        public void InitializeTask(bool initializeJobTimer)
        {
            InitializeTaskItem();
            if (initializeJobTimer)
            {
                InitializeJobTimer(true);
            }
        }
        private void SendNotificationEmail()
        {
            if (TaskItem.TaskSettings.SendNotificationEmail)
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    DateFormatString = FormatProviders.DateTimeISO,
                    DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc
                };
                var taskSettings = Newtonsoft.Json.JsonConvert.SerializeObject(TaskItem, settings).Replace(",", ",<br />").Replace("{", "{<br />").Replace("}", "}<br />");
                SendNotificationByEmail($"Task Settings: {taskSettings}", addMachineNameAndIp: true);
            }
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Send Notification Email with the given bodyContent. 
        /// All settings are get with CustomServiceName prefix.
        /// </summary>
        /// <param name="bodyContent"></param>
        public string SendNotificationByEmail(string bodyContent, string[] attachments = null, bool addMachineNameAndIp = false)
        {
            string subject = $"{TaskItem.CustomServiceName}";
            string displayName = string.Empty;//ToDo: read setings accept empty value
            string from = string.Empty;//ToDo: read setings required
            string to = string.Empty;//ToDo: read setings required
            string cc = string.Empty;//ToDo: read setings accepts empty value

            return null; //ToDo: send email
        }

        #endregion
    }
}
