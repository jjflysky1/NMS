using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace test4
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            SERVICECLS.SERVICECLS cls = new SERVICECLS.SERVICECLS();
            Service2.Service2 cls2 = new Service2.Service2();
            Traffic.Class1 cls3 = new Traffic.Class1();
            pingtime.Class1 cls4 = new pingtime.Class1();
            Thread thread = new Thread(cls.iis);
            thread.Start();

            Thread thread2 = new Thread(cls4.pingtime);
            thread2.Start();

            Thread thread3 = new Thread(cls2.autoadd);
            thread3.Start();

            Thread thread4 = new Thread(cls2.systeminfo);
            thread4.Start();

            Thread thread6 = new Thread(cls3.networkinfo);
            thread6.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
