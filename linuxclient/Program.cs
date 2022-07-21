using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace linuxclient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    SOAP.Systeminfo soapclient = new SOAP.Systeminfo();

                    //IP
                    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                    string IPAddress = string.Empty;
                    foreach (IPAddress ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            IPAddress = ip.ToString();
                            break;
                        }
                    }
                    //Console.WriteLine("ip : " + IPAddress);
                    DBCON.Class1 DBCON = new DBCON.Class1();
                    MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                    string SQL = "";
                    //while (true)
                    //{
                    SQL = "select distinct serverip, os, serverid, log_time , trafficlimit , cpulimit, memorylimit, ifnull (Community, 'public') as community from service a , log_time_config b, mail_info c where a.flag = '1' and category = N'서버 장비'  and status = 'Server Connect'" +
                        " and serverip = '" + IPAddress + "' ";
                    MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                    DataSet DBSET = new DataSet();
                    ADT.Fill(DBSET, "BD");

                    foreach (DataRow row in DBSET.Tables["BD"].Rows)
                    {

                        Console.WriteLine("들어옴" + IPAddress);



                        //Traffic
                        if (!NetworkInterface.GetIsNetworkAvailable())
                            return;

                        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

                        double sent = 0;
                        double recevied = 0;
                        double sent1 = 0;
                        double recevied1 = 0;
                        double total = 0;
                        double total1 = 0;

                        foreach (NetworkInterface net in interfaces)
                        {
                            Console.WriteLine("net.Id: {0}", net.Id); // 네트워크의 고유id
                            Console.WriteLine("net.Name: {0}", net.Name); // 표기되는 이름
                            Console.WriteLine("net.IsReceiveOnly: {0}", net.IsReceiveOnly);
                            Console.WriteLine("net.OperationalStatus: {0}", net.OperationalStatus); // 연결됐습니까?
                            Console.WriteLine("net.NetworkInterfaceType: {0}", net.NetworkInterfaceType); // 구분용??
                            Console.WriteLine("net.Description: {0}", net.Description); // 장치설명
                            Console.WriteLine("net.SupportsMulticast: {0}", net.SupportsMulticast);
                            Console.WriteLine("------------------");
                        }

                        foreach (NetworkInterface ni in interfaces)
                        {
                            if (ni.NetworkInterfaceType.ToString() == "Ethernet" && ni.OperationalStatus.ToString() == "Up")
                            {
                                //Console.WriteLine("    Bytes Sent: {0}", ni.GetIPv4Statistics().BytesSent);
                                //Console.WriteLine("    Bytes Received: {0}", ni.GetIPv4Statistics().BytesReceived);

                                sent = Convert.ToDouble(ni.GetIPv4Statistics().BytesSent);
                                recevied = Convert.ToDouble(ni.GetIPv4Statistics().BytesReceived);

                                break;
                            }
                        }
                        total = sent + recevied;
                        Console.WriteLine("traffic IN : " + total);

                        Thread.Sleep(1000);
                        foreach (NetworkInterface ni in interfaces)
                        {
                            if (ni.NetworkInterfaceType.ToString() == "Ethernet" && ni.OperationalStatus.ToString() == "Up")
                            {
                                //Console.WriteLine("    Bytes Sent: {0}", ni.GetIPv4Statistics().BytesSent);
                                //Console.WriteLine("    Bytes Received: {0}", ni.GetIPv4Statistics().BytesReceived);

                                sent1 = Convert.ToDouble(ni.GetIPv4Statistics().BytesSent);
                                recevied1 = Convert.ToDouble(ni.GetIPv4Statistics().BytesReceived);

                                break;
                            }

                        }
                        total1 = sent1 + recevied1;
                        Console.WriteLine("traffic out : " + total1);



                        total1 = total1 - total;


                        //Console.WriteLine("traffic : " + total1 );


                        //Console.WriteLine(soapclient.Traffic(total1.ToString(), IPAddress));
                        soapclient.Traffic(total1.ToString(), IPAddress);

                        if (Convert.ToInt64(row["trafficlimit"]) < total1 && Convert.ToInt64(row["trafficlimit"]) > 0)
                        {
                            double nowtraffic = total1;
                            Traffic_Mail mail = new Traffic_Mail();
                            mail.Traffic_sendmail(row["serverip"].ToString(), null, nowtraffic.ToString("N1"));
                        }

                        Thread.Sleep(5000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);  
                }
                






            }

        }
    }
}
