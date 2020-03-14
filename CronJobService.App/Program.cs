using CronJobService.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CronJobService.App
{
    public class Program : ServiceBase
    {
        private static string CustomServiceName = "KolesnichenkoCronJobWinService";
        private static string EventSource
        {
            get
            {
                return CustomServiceName + "Log";
            }
        }

        // The main entry point for the process
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("bg-BG");

#if(DEBUG)
            try
            {
                TaskBasic task = null;
                task.RunTask();

            }
            catch (Exception ex)
            {
                //LogManager.LogError(ex, EventSource);
                //LogManager.WriteToEventLog(EventSource, "End " + CustomServiceName, EventLogOperation.TaskOperationError, System.Diagnostics.EventLogEntryType.Error);
            }
#else
            if (Environment.UserInteractive)
            {
                try
                {
                    string parameter = string.Concat(args);
                    switch (parameter)
                    {
                        case "--install":
                            ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                            break;
                        case "--uninstall":
                            ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                            break;
                    }
                }
                catch (System.Security.SecurityException secex) 
                {
                    LogManager.WriteToEventLog(EventSource, secex, "Command Prompt need to be 'Run as Administrator'!!! ");
                }
                catch (Exception ex)
                {
                    LogManager.WriteToEventLog(EventSource, ex);
                }
            }
            else
            {
                RunService();
            }                    
#endif
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //LogManager.WriteToEventLog(EventSource, (Exception)e.ExceptionObject);
        }

        private static void RunService()
        {
            Program serviceToRun = new Program();
            ServiceBase.Run(serviceToRun);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServiceName = CustomServiceName;
            this.AutoLog = true;
        }

        /// <summary>
        /// Set things in motion so your service can do its work.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            //LogManager.WriteToEventLog(EventSource, CustomServiceName + " started", eventOperationID: EventLogOperation.MainOperationBegin);

            try
            {
                TaskBasic[] tasks = new TaskBasic[] { };

                foreach (TaskBasic task in tasks)
                {
                    task.InitializeTask(true);
                }
            }
            catch (Exception ex)
            {
                //LogManager.LogError(ex, EventSource);
            }
        }

        /// <summary>
        /// Stop this service.
        /// </summary>
        protected override void OnStop()
        {
            //LogManager.WriteToEventLog(EventSource, CustomServiceName + " stop", eventOperationID: EventLogOperation.MainOperationEnd);
        }
    }
}
