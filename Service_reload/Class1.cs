using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service_reload
{
    public class RELOAD
    {
        public void reload()
        {
            while (true)
            {
                try
                {
                    Process[] processes = Process.GetProcessesByName("LNM_TRA");
                    if (processes.Length == 0)
                    {

                        Console.WriteLine("Not running");
                        Process.Start("LNM_TRA.exe");

                    }
                    else
                    {

                        Console.WriteLine("Running");
                    }
                    Process[] processes2 = Process.GetProcessesByName("Client");
                    if (processes2.Length == 0)
                    {

                        Console.WriteLine("Not running");
                        Process.Start("Client.exe");

                    }
                    else
                    {

                        Console.WriteLine("Running");
                    }
                    Thread.Sleep(10000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("LNMS 서비스 감지 에러");
                }
                
            }
        }

    }
}
