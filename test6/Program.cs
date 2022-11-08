
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedNativeWifi;
using NativeWifi;
using System.Threading;
using System.Data;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace test6
{
    class Program
    {
        public class a
        {
            public void name()
            {
                Console.WriteLine("a");
            }
        }
        public class b : a
        {
            public void name()
            {
                Console.WriteLine("b");
            }
        }

        static WlanClient wlanClient = new WlanClient();
        static string GetMACAddress(byte[] macByteArray)
        {
            string macString = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                macString += macByteArray[i].ToString("X2") + "-";
            }
            return macString.Substring(0, macString.Length - 1);
        }


        static string GetSSIDString(Wlan.WlanAvailableNetwork wlanAvailableNetwork)
        {
            Wlan.Dot11Ssid ssid = wlanAvailableNetwork.dot11Ssid; return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }
        static int GetChannel(Wlan.WlanAvailableNetwork wlanAvailableNetwork)
        {
            string name = GetMACAddress(wlanAvailableNetwork.dot11Ssid.SSID);
            Wlan.WlanBssEntry[] wlanBssEntryArray = wlanClient.Interfaces[0].GetNetworkBssList();

            foreach (Wlan.WlanBssEntry wlanBssEntry in wlanBssEntryArray)
            {
                if (GetMACAddress(wlanBssEntry.dot11Ssid.SSID) == name)
                {
                    uint chCenterFrequency = wlanBssEntry.chCenterFrequency;

                    switch (chCenterFrequency)
                    {
                        case 2412000: return 1;
                        case 2417000: return 2;
                        case 2422000: return 3;
                        case 2427000: return 4;
                        case 2432000: return 5;
                        case 2437000: return 6;
                        case 2442000: return 7;
                        case 2447000: return 8;
                        case 2452000: return 9;
                        case 2457000: return 10;
                        case 2462000: return 11;
                        case 2467000: return 12;
                        case 2472000: return 13;
                    }
                }
            }
            return -1;
        }
        static string GetMACAddress(Wlan.WlanAvailableNetwork wlanAvailableNetwork)
        {
            string name = GetMACAddress(wlanAvailableNetwork.dot11Ssid.SSID);
            Wlan.WlanBssEntry[] wlanBssEntryArray = wlanClient.Interfaces[0].GetNetworkBssList();
            foreach (Wlan.WlanBssEntry wlanBssEntry in wlanBssEntryArray)
            {
                if (GetMACAddress(wlanBssEntry.dot11Ssid.SSID) == name)
                {
                    return GetMACAddress(wlanBssEntry.dot11Bssid);
                }
            }
            return null;
        }

        static void Main(string[] args)
        {

            MySqlConnection CON = new MySqlConnection("Server = 192.168.1.174; Database = cs; User id = nms; Password = P@ssw0rd");
            //MySqlConnection CON = new MySqlConnection("Server = 192.168.0.190; Database = cs; User id = nms; Password = P@ssw0rd");
            string SQL = "";
            SQL = "select distinct a.email, a.mailno, c.phone , c.mailip, fORMAT(c.trafficlimit, 0) AS trafficlimit , mail_sender from mail_target a , service b ,  mail_info c where  c.flag=1 and  DATE_ADD(b.trafficmail, INTERVAL 10 MINUTE) < now() and a.serverip = b.ServerIP " +
                " and b.serverip = '192.168.53.234' limit 1";
            ///192.168.53.234
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                if (row["email"].ToString() != "")
                {
                    try
                    {
                        //Response.Write("TEST");

                        MailMessage MAIL = new MailMessage();
                        SmtpClient SMTPMAIL = new SmtpClient(row["mailip"].ToString());
                        //MAIL.From = new MailAddress(row["mail_sender"].ToString());
                        MAIL.From = new MailAddress("noreply@jusung.com");
                        SMTPMAIL.Port = 25;
                        //MAIL.To.Add(Recever.Text.ToString());
                        //MAIL.To.Add(row["email"].ToString());
                        MAIL.To.Add("it@jusung.com");
                        //if (CC.Text.ToString() != "")
                        //{
                        //    MAIL.CC.Add(CC.Text.ToString());
                        //}

                        MAIL.Subject = "메일 테스트 입니다";
                        //MAIL.Body = serverip + " 장비 " + portname + " 현재 트래픽 알림 기준치인 " + row["trafficlimit"].ToString() + " 넘어 " + String.Format("{0:n0}", nowtraffic) + " 입니다. ";
                        MAIL.Body = "메일 테스트 입니다";
                        MAIL.BodyEncoding = System.Text.Encoding.UTF8;
                        MAIL.SubjectEncoding = System.Text.Encoding.UTF8;

                        SMTPMAIL.Send(MAIL);

                        Console.WriteLine("메일보냄");
                        Console.ReadLine();

                    }
                    catch(Exception e)
                    {
                       Console.WriteLine(e.Message);
                       Console.ReadLine();
                    }
                }

            }


            //while (true)
            //{
            //    Wlan.WlanAvailableNetwork[] wlanAvailaleNetworkArray = wlanClient.Interfaces[0].GetAvailableNetworkList(0);

            //    try
            //    {
            //        foreach (Wlan.WlanAvailableNetwork wlanAvailableNetwork in wlanAvailaleNetworkArray)
            //        {
            //            Console.WriteLine("S  S  I  D   : " + GetSSIDString(wlanAvailableNetwork));
            //            Console.WriteLine("신 호 강 도  : " + wlanAvailableNetwork.wlanSignalQuality.ToString());
            //            Console.WriteLine("암   호   화 : " + wlanAvailableNetwork.securityEnabled.ToString());
            //            Console.WriteLine("채        널 : " + GetChannel(wlanAvailableNetwork).ToString());
            //            Console.WriteLine("암 호 방 식  : " + wlanAvailableNetwork.dot11DefaultCipherAlgorithm.ToString());
            //            Console.WriteLine("암호알고리즘 : " + wlanAvailableNetwork.dot11DefaultAuthAlgorithm.ToString());
            //            Console.WriteLine("맥   주   소 : " + GetMACAddress(wlanAvailableNetwork));
            //            Console.WriteLine("------------------------------------------------------------------------");
            //        }
            //    }
            //    catch (Exception E)
            //    {
            //        Console.WriteLine(E.Message);
            //    }
            //    Thread.Sleep(5000);
            //}








            //a a = new a();
            //b b = new b();

            //a.name();
            //b.name();

            //TrapAgent agent = new TrapAgent();

            //// Variable Binding collection to send with the trap
            //VbCollection col = new VbCollection();
            //col.Add(new Oid("1.3.6.1.2.1.1.1.0"), new OctetString("Test string"));
            //col.Add(new Oid("1.3.6.1.2.1.1.2.0"), new Oid("1.3.6.1.9.1.1.0"));
            //col.Add(new Oid("1.3.6.1.2.1.1.3.0"), new TimeTicks(2324));
            //col.Add(new Oid("1.3.6.1.2.1.1.4.0"), new OctetString("Milan"));

            //// Send the trap to the localhost port 162
            //agent.SendV1Trap(new IpAddress("192.168.0.106"), 162, "public",
            //                 new Oid("1.3.6.1.2.1.1.1.0"), new IpAddress("127.0.0.1"),
            //                 SnmpConstants.LinkUp, 0, 13432, col);


            //SimpleSnmp snmp = new SimpleSnmp("192.168.0.200", "public");
            //Pdu pdu = new Pdu();

            ////유선 사용하는 맥주소
            //Dictionary<Oid, AsnType> result = snmp.Walk(SnmpVersion.Ver2, ".1.3.6.1.2.1.4.22.1.2");

            //if (result == null)
            //{
            //    Console.WriteLine("Request failed.");
            //}
            //else
            //{
            //    List<string> list = new List<string>();
            //    int i = 0;
            //    foreach (var entry3 in result)
            //    {
            //        Console.WriteLine(entry3.Key.ToString() + " || " + entry3.Value.ToString());
            //        i++;
            //    }
            //    Console.WriteLine(i);
            //}


            //string sDirPath;
            //sDirPath = @"C:\NMS(maria)\debug";
            //DirectoryInfo di = new DirectoryInfo(sDirPath);
            //if (di.Exists == false)
            //{
            //    di.Create();
            //}

            //string path = @"C:\NMS(maria)\debug\error.txt"; // path to file
            //string txt = "tset" + "\n";
            //File.AppendAllText(path, txt);

        }
    }
}
