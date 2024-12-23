using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
           

            Client.Class1 cls1 = new Client.Class1();
     

            
            //foreach (Process process in Process.GetProcesses())
            //{
            //    if (process.ProcessName.StartsWith("SocketClient1"))
            //    {
            //        process.Kill();
            //    }
            //    else
            //    {

            //    }
            //}

            //string strappname = "C:\\SSIM WATCHER\\SocketClient1.exe";
            //Process.Start(strappname);



            try
            {
                //클라이언트
                Thread thread1 = new Thread(cls1.SYSTEMINFO);
                thread1.Start();
            }
            catch (Exception ex)
            {
                    Console.WriteLine(ex.Message);  
            }



            //try
            //{
            //    // 네트워크 어댑터 정보 검색
            //    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = true");

            //    foreach (ManagementObject queryObj in searcher.Get())
            //    {
            //        // 네트워크 어댑터 이름 출력
            //        Console.WriteLine("Adapter: " + queryObj["Description"]);

            //        // 대표 IP 주소 한 개만 출력
            //        string[] ipAddresses = (string[])queryObj["IPAddress"];
            //        if (ipAddresses != null && ipAddresses.Length > 0)
            //        {
            //            Console.WriteLine("IP Address: " + ipAddresses[0]);
            //        }

            //        // 첫 번째 활성화된 네트워크 어댑터만 가져오기 위해 break 추가
            //        break;
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("An error occurred: " + e.Message);
            //}



        }

        
    }
}
