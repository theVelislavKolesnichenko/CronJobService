using System.Configuration.Install;
using System.ServiceProcess;

namespace CronJobService.App
{
    public class CronJobInstaller : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller serviceProcessInstaller;

        public CronJobInstaller()
        {
            // This call is required by the Designer.
            InitializeComponent();
        }

        /// <summary>
        /// Required method for Designer support - do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceInstaller = new ServiceInstaller();
            this.serviceProcessInstaller = new ServiceProcessInstaller();
            // 
            // serviceInstaller
            // 
            this.serviceInstaller.Description = "Kolesnichenko Cron Job Windows Service";
            this.serviceInstaller.DisplayName = "Kolesnichenko Cron Job Windows Service";
            this.serviceInstaller.ServiceName = "KolesnichenkoCronJobWinService";
            this.serviceInstaller.StartType = ServiceStartMode.Automatic;

            // 
            // serviceProcessInstaller
            // 
            this.serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;

            // 
            // ServiceInstaller
            // 
            this.Installers.AddRange(new Installer[] { this.serviceProcessInstaller, this.serviceInstaller });

            this.Committed += new InstallEventHandler(SitemapGeneratorServiceInstaller_Committed);
        }

        private void SitemapGeneratorServiceInstaller_Committed(object sender, InstallEventArgs e)
        {
            // Auto Start the Service Once Installation is Finished.
            ServiceController controller = new ServiceController(this.serviceInstaller.ServiceName);
            if (controller.Status == ServiceControllerStatus.Stopped)
            {
                controller.Start();
            }
        }
    }
}
