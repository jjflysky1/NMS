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

        
        // user32.dll에 선언된 함수들을 가져옵니다.
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        const int SW_HIDE = 0; // 숨기기
        //const int SW_SHOW = 1; // 보이기

        private const int SW_SHOW = 5;        // 창을 보여줌
        private const int SW_RESTORE = 9;     // 창을 복원함

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
                        rdcProcess.StartInfo.FileName = @"C:\Program Files\PuTTY\putty";

                        // PuTTY 실행
                        rdcProcess.Start();

                        try
                        {
                            // 프로세스가 MainWindowHandle을 가질 때까지 대기
                            rdcProcess.WaitForInputIdle();

                            IntPtr handle = rdcProcess.MainWindowHandle;

                            // 핸들이 초기화될 때까지 최대 5번 시도
                            int retries = 5;
                            while (handle == IntPtr.Zero && retries > 0)
                            {
                                Thread.Sleep(500); // 잠시 대기
                                handle = rdcProcess.MainWindowHandle;
                                retries--;
                            }

                            if (handle != IntPtr.Zero)
                            {
                                // 현재 쓰레드와 창의 쓰레드를 연결
                                uint appThread = GetWindowThreadProcessId(handle, IntPtr.Zero);
                                uint currentThread = GetCurrentThreadId();

                                if (appThread != currentThread)
                                {
                                    AttachThreadInput(currentThread, appThread, true);
                                    SetForegroundWindow(handle);
                                    AttachThreadInput(currentThread, appThread, false);
                                }
                                else
                                {
                                    SetForegroundWindow(handle);
                                }

                                // 창을 복원하고 보여줌
                                ShowWindow(handle, SW_RESTORE);
                            }
                            else
                            {
                                Console.WriteLine("프로세스의 메인 창 핸들을 가져오지 못했습니다.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("오류 발생: " + ex.Message);
                        }
                        finally
                        {
                            rdcProcess.Dispose();
                        }
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
