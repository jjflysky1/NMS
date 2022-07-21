using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnmapTrap
{
    class Program
    {
        static void message(string text)
        {

            MessageBox.Show(text, "진짜", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

        }

        static void Main(string[] args)
        {
            DBCON.Class1 DBCON = new DBCON.Class1();
            SqlConnection CON = new SqlConnection(DBCON.DBCON);
            // Construct a socket and bind it to the trap manager port 162 
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 162);
            EndPoint ep = (EndPoint)ipep;
            socket.Bind(ep);
            // Disable timeout processing. Just block until packet is received 
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 0);
            bool run = true;
            int inlen = -1;
            while (run)
            {
                byte[] indata = new byte[16 * 1024];
                // 16KB receive buffer int inlen = 0;
                IPEndPoint peer = new IPEndPoint(IPAddress.Any, 0);
                EndPoint inep = (EndPoint)peer;
                try
                {
                    inlen = socket.ReceiveFrom(indata, ref inep);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception {0}", ex.Message);
                    inlen = -1;
                }
                if (inlen > 0)
                {
                    // Check protocol version int 
                    int ver = SnmpPacket.GetProtocolVersion(indata, inlen);
                    if (ver == (int)SnmpVersion.Ver1)
                    {
                        // Parse SNMP Version 1 TRAP packet 
                        SnmpV1TrapPacket pkt = new SnmpV1TrapPacket();
                        pkt.decode(indata, inlen);
                        Console.WriteLine("** SNMP Version 1 TRAP received from {0}:", inep.ToString());
                        Console.WriteLine("*** Trap generic: {0}", pkt.Pdu.Generic);
                        Console.WriteLine("*** Trap specific: {0}", pkt.Pdu.Specific);
                        Console.WriteLine("*** Agent address: {0}", pkt.Pdu.AgentAddress.ToString());
                        DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);  // 우리나라 시간 기준
                        double tmp; // String 자료형으로 된 TimeStamp 값을 double 형으로 바꿔서 넣을 변수
                        tmp = Convert.ToDouble(pkt.Pdu.TimeStamp);
                        string real = "";
                        real = Convert.ToString(time.AddSeconds(tmp));
                        //Console.WriteLine("*** Timestamp: {0}", real.ToString());
                        Console.WriteLine("*** Timestamp: {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                        Console.WriteLine("*** VarBind count: {0}", pkt.Pdu.VbList.Count);
                        Console.WriteLine("*** VarBind content:");
                        foreach (Vb v in pkt.Pdu.VbList)
                        {
                            Console.WriteLine("**** {0} {1}: {2}", v.Oid.ToString(), SnmpConstants.GetTypeName(v.Value.Type), v.Value.ToString());

                            if (v.Value.ToString().Contains("changed state to down") == true)
                            {
                                Thread thread11 = new Thread(() => message("나온다"));
                                thread11.Start();
                            }
                            Console.WriteLine("진짜 값 : " + v.Value.ToString().TrimStart());
                        }
                        

                        Console.WriteLine("** End of SNMP Version 1 TRAP data.");
                    }
                    else
                    {
                        // Parse SNMP Version 2 TRAP packet 
                        SnmpV2Packet pkt = new SnmpV2Packet();
                        pkt.decode(indata, inlen);
                        Console.WriteLine("** SNMP Version 2 TRAP received from {0}:", inep.ToString());
                        if ((SnmpSharpNet.PduType)pkt.Pdu.Type != PduType.V2Trap)
                        {
                            Console.WriteLine("*** NOT an SNMPv2 trap ****");
                        }
                        else
                        {
                            Console.WriteLine("*** Community: {0}", pkt.Community.ToString());
                            Console.WriteLine("*** VarBind count: {0}", pkt.Pdu.VbList.Count);
                            Console.WriteLine("*** VarBind content:");
                            string sniper = "";
                            string servertime = "";
                            double trafficbps = 0;
                            string cpu = "";
                            foreach (Vb v in pkt.Pdu.VbList)
                            {
                                //Console.WriteLine("**** {0} {1}: {2}", v.Oid.ToString(), SnmpConstants.GetTypeName(v.Value.Type), v.Value.ToString());
                                if (v.Oid.ToString().Contains("1.3.6.1.4.1.8103.1") == true)
                                {
                                    Console.WriteLine("이벤트 로그");
                                }
                                if (v.Oid.ToString() == "1.3.6.1.4.1.8103.2.1")
                                {
                                    //Console.WriteLine("Sinper id :" + v.Value.ToString());
                                    sniper = v.Value.ToString();
                                }
                                if (v.Oid.ToString() == "1.3.6.1.4.1.8103.2.2")
                                {
                                    DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);  // 우리나라 시간 기준
                                    double tmp; // String 자료형으로 된 TimeStamp 값을 double 형으로 바꿔서 넣을 변수
                                    tmp = Convert.ToDouble(v.Value.ToString());
                                    string real = Convert.ToString(time.AddSeconds(tmp));
                                    //Console.WriteLine("Server Time :" + real.ToString());
                                    servertime = real.ToString();
                                }
                                if (v.Oid.ToString() == "1.3.6.1.4.1.8103.2.3")
                                {
                                    //Console.WriteLine("traffic bps :" + v.Value.ToString());
                                    trafficbps = Convert.ToDouble(v.Value.ToString()) / 1024 / 1024;
                                }
                                if (v.Oid.ToString() == "1.3.6.1.4.1.8103.2.5")
                                {
                                    //Console.WriteLine("CPU :" + v.Value.ToString());
                                    cpu = v.Value.ToString();
                                }
                            }
                            string[] serverip = inep.ToString().Split(':');
                            Console.WriteLine("server ip :" + serverip[0].ToString());
                            Console.WriteLine("Sinper id :" + sniper.ToString());
                            Console.WriteLine("Server Time :" + servertime.ToString());
                            Console.WriteLine("traffic bps :" + trafficbps.ToString("n3"));
                            Console.WriteLine("CPU :" + cpu.ToString());




                            Console.WriteLine("** End of SNMP Version 2 TRAP data.");
                        }
                    }
                }
                else
                {
                    if (inlen == 0)
                        Console.WriteLine("Zero length packet received.");
                }
            }
        }
    
        public void trhead()
        {
         
        }
        
        static void Linux(int a)
        {
            int b = 1;
            Console.WriteLine(a + " 번 스레드 : " + b);
            b = b + 1;
            Thread.Sleep(1000);
        }
    }
}
