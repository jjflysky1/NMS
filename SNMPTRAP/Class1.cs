using MySql.Data.MySqlClient;
using SnmpSharpNet;
using System;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SNMPTRAP
{
    public class Class1
    {
        static void message(string text)
        {

            MessageBox.Show(text, "진짜", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

        }

        public void snmptrap()
        {
            try
            {
                DBCON.Class1 DBCON = new DBCON.Class1();
                MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
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
                            string[] serverip = inep.ToString().Split(':');
                            foreach (Vb v in pkt.Pdu.VbList)
                            {
                                Console.WriteLine("**** {0} {1}: {2}", v.Oid.ToString(), SnmpConstants.GetTypeName(v.Value.Type), v.Value.ToString());
                                if (v.Value.ToString().Contains("changed state to down") == true)
                                {
                                    //Thread thread11 = new Thread(() => message("나온다"));
                                    //thread11.Start();
                                }
                                string SQL = "";
                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                SQL = "select * from event_list where flag = '1'";
                                MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                                DataSet DBSET = new DataSet();
                                ADT.Fill(DBSET, "BD");
                                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                                {
                                    if (v.Value.ToString().Contains(row["name"].ToString()) == true)
                                    {
                                        Console.WriteLine(row["name"].ToString() + " 이벤트 발견");


                                        MySqlCommand cmd1 = new MySqlCommand();
                                        cmd1.Connection = CON;
                                        cmd1.CommandType = System.Data.CommandType.Text;
                                        cmd1.CommandText = "insert into event_log (serverip,event_log,time) values(@serverip,@event_log,now()) ";
                                        cmd1.Parameters.Add("@event_log", MySqlDbType.VarChar, 100).Value = SnmpConstants.GetTypeName(v.Value.Type) + ": " + v.Value.ToString();
                                        cmd1.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = serverip[0].ToString();
                                        cmd1.ExecuteNonQuery();
                                        cmd1.Dispose();
                                        cmd1 = null;


                                        mail mail = new mail();
                                        Console.WriteLine("진짜 값 : " + v.Value.ToString());
                                        mail.Event_sendmail(serverip[0].ToString(), v.Value.ToString(), row["name"].ToString());
                                    }
                                    else
                                    {
                                        Console.WriteLine("없다");
                                    }
                                }
                                CON.Close();

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


                                try
                                {
                                    string SQL2 = "select top 1 SUBSTRING(Convert(nvarchar,dateadd(mi, 30,time),121),0,17)  as time from Secure_Log " +
                                                "where serverip = '" + serverip[0].ToString() + "' order by no desc ";
                                    MySqlDataAdapter ADT7 = new MySqlDataAdapter(SQL2, CON);
                                    DataSet DBSET7 = new DataSet();
                                    ADT7.Fill(DBSET7, "BD2");
                                    foreach (DataRow row2 in DBSET7.Tables["BD2"].Rows)
                                    {
                                        string time1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                                        string time2 = DateTime.Now.ToString(row2["time"].ToString());
                                        //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                                        //time2 = Regex.Replace(time2, " ", ":");
                                        if (Convert.ToDateTime(time1) > Convert.ToDateTime(time2))
                                        {
                                            CON.Open();
                                            MySqlCommand cmd3 = new MySqlCommand();
                                            cmd3.Connection = CON;
                                            cmd3.CommandType = System.Data.CommandType.Text;
                                            cmd3.CommandText = "insert into Secure_Log (serverip,traffic,time,cpu,memory,hd) values(@serverip,@traffic,now(),@cpu,null,null) ";
                                            cmd3.Parameters.Add("@traffic", MySqlDbType.VarChar, 100).Value = trafficbps.ToString("n3");
                                            cmd3.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = serverip[0].ToString();
                                            cmd3.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = cpu.ToString();
                                            cmd3.ExecuteNonQuery();
                                            cmd3.Dispose();
                                            cmd3 = null;
                                            CON.Close();
                                        }
                                    }

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }



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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
