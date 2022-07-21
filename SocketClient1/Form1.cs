using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketClient1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static byte[] Buffer { get; set; }
        static Socket sck;
        private void Form1_Load(object sender, EventArgs e)
        {
            while (true)
            {
                try
                {

                    sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sck.Bind(new IPEndPoint(IPAddress.Any, 1234));
                    sck.Listen(100);

                    Socket accepted = sck.Accept();

                    Buffer = new byte[accepted.SendBufferSize];
                    int bytesRead = accepted.Receive(Buffer);
                    byte[] formatted = new byte[bytesRead];
                    for (int i = 0; i < bytesRead; ++i)
                    {
                        formatted[i] = Buffer[i];
                    }


                    string strdata = Encoding.UTF8.GetString(formatted);
                    if (strdata == "")
                    {

                    }
                    else
                    {
                        Process rdcProcess = new Process();
                        rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
                        rdcProcess.StartInfo.Arguments = "/v " + strdata; // ip or name of computer to connect
                        rdcProcess.Start();
                    }



                    Console.Write(strdata + "\r\n");
                    //Console.Read();


                    accepted.Close();
                    sck.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Thread.Sleep(1000);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
           
        }
    }
}
