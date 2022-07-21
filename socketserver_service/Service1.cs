using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace socketserver_service
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            Process.Start("C:\\SSIM WATCHER\\SocketServer.exe");
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
            foreach (Process pro in Process.GetProcesses())
            {
                if (pro.ProcessName.StartsWith("SocketServer"))
                {
                    pro.Kill();
                }
            }
        }
    }
}
