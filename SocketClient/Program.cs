using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {
        static byte[] Buffer { get; set; }
        static Socket sck;
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0; // 숨기기
        const int SW_SHOW = 1; // 보이기


        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();

            ShowWindow(handle, SW_HIDE); // 숨기기

            //ShowWindow(handle, SW_SHOW); // 보이기


            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(IPAddress.Any, 1234));
            sck.Listen(100);

            Thread thread = new Thread(Socket);
            thread.Start();

            
        }
        
        static void Socket()
        {
            while (true)
            {
                try
                {
                    Socket accepted = sck.Accept();

                    Buffer = new byte[accepted.SendBufferSize];
                    int bytesRead = accepted.Receive(Buffer);
                    byte[] formatted = new byte[bytesRead];
                    for (int i = 0; i < bytesRead; ++i)
                    {
                        formatted[i] = Buffer[i];
                    }

                    string strdata = Encoding.UTF8.GetString(formatted);
                    string[] getdata = { };
                    getdata = strdata.Split(',');

                    if (getdata[1].ToString().Contains("Windows") == true)
                    //if (getdata[1].ToString() == "서버")
                    {
                        Process rdcProcess = new Process();
                        //rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
                        rdcProcess.StartInfo.FileName = "mstsc";
                        rdcProcess.StartInfo.Arguments = "/v " + getdata[0]; // ip or name of computer to connect
                        rdcProcess.Start();
                        rdcProcess.Dispose();
                    }
                    else
                    {
                        Console.WriteLine("NO PC");
                        Process rdcProcess = new Process();
                        //rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
                        //rdcProcess.StartInfo.FileName = "cmd";
                        rdcProcess.StartInfo.FileName = "C:\\Program Files\\PuTTY\\putty";

                        //rdcProcess.StartInfo.Arguments = "ssh 192.168.0.170";
                        rdcProcess.Start();

                        rdcProcess.Dispose();
                    }


                    Console.Write(strdata + "\r\n");
                    //Console.Read();


                    //accepted.Close();
                    //sck.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("들어왔다");
                    Console.WriteLine(e.Message);
                }
                
            }
        }
    }
}
