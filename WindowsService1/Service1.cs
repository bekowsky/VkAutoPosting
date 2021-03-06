using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ILogger logger;
            if (Environment.UserInteractive)
                logger = new ConsoleLogger();
            else logger = new WindowsLogger("");
            var master = new MasterService(logger);
            Thread masterThread = new Thread(new ThreadStart(master.Start));
            Thread cleaningThread = new Thread(new ThreadStart(master.Cleaning));
            masterThread.Start();
            cleaningThread.Start();


        }

        protected override void OnStop()
        {
        }
    }
}
