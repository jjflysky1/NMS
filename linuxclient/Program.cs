using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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

                    //OS
                    // /etc/os-release 파일을 읽기
                    string osReleaseFile = "/etc/os-release";

                    if (File.Exists(osReleaseFile))
                    {
                        string[] lines = File.ReadAllLines(osReleaseFile);

                        Console.WriteLine("OS Information from /etc/os-release:");
                        string osName = string.Empty;
                        string osVersion = string.Empty;

                        foreach (var line in lines)
                        {
                            // 'NAME='과 'VERSION=' 값을 찾고 저장
                            if (line.StartsWith("NAME="))
                            {
                                osName = line.Split('=')[1].Trim('\"'); // "이 포함될 수 있기 때문에 제거
                            }
                            if (line.StartsWith("VERSION="))
                            {
                                osVersion = line.Split('=')[1].Trim('\"');
                            }
                        }

                        // OS 이름과 버전을 한 줄로 합치기
                        if (!string.IsNullOrEmpty(osName) && !string.IsNullOrEmpty(osVersion))
                        {
                            string osInfo = $"{osName} {osVersion}";
                            Console.WriteLine("OS Name and Version: " + osInfo);
                            soapclient.OS(osInfo, IPAddress);  // SOAP 클라이언트에 전달
                        }
                        else
                        {
                            Console.WriteLine("OS 정보가 불완전합니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("/etc/os-release 파일을 찾을 수 없습니다.");
                    }



                    //hostname
                    string hostName = Dns.GetHostName();
                    Console.WriteLine("Hostname: " + hostName);
                    soapclient.COMPUTERNAME(hostName, IPAddress);

                    // CPU 정보 가져오기
                    string cpuInfo = File.ReadAllText("/proc/stat");
                    Console.WriteLine("CPU Info:");
                    double cpuUsage = 0;
                    // 첫 번째 줄은 CPU의 전체 사용량에 대한 정보
                    var cpuLine = cpuInfo.Split('\n')[0];  // 첫 번째 줄만 가져오기
                    var cpuParts = cpuLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);  // 공백으로 분리하고 빈 항목 제거
                    if (cpuParts.Length > 1)
                    {
                        // CPU 사용량 계산 (user, nice, system, idle 등)
                        long user = long.Parse(cpuParts[1]);
                        long nice = long.Parse(cpuParts[2]);
                        long system = long.Parse(cpuParts[3]);
                        long idle = long.Parse(cpuParts[4]);
                        long total = user + nice + system + idle;

                        long used = user + nice + system;  // 사용된 시간

                        // CPU 사용률 계산
                        cpuUsage = (double)used / total * 100;

                        Console.WriteLine($"CPU Usage: {cpuUsage:F2}%");

                        soapclient.CPU(cpuUsage.ToString("N1"), IPAddress);
                    }

                    // 메모리 정보 가져오기
                    string memoryInfo = File.ReadAllText("/proc/meminfo");
                    Console.WriteLine("\nMemory Info:");

                    // "MemTotal"과 "MemFree" 값을 추출하여 사용된 메모리 계산
                    var memLines = memoryInfo.Split('\n')
                                             .Where(line => line.StartsWith("MemTotal") || line.StartsWith("MemFree"))
                                             .ToArray();

                    long memTotal = 0;
                    long memFree = 0;
                    double memUsage = 0;
                    foreach (var line in memLines)
                    {
                        var parts = line.Split(':');
                        if (parts[0].Trim() == "MemTotal")
                        {
                            memTotal = long.Parse(parts[1].Trim().Split(' ')[0]);
                        }
                        else if (parts[0].Trim() == "MemFree")
                        {
                            memFree = long.Parse(parts[1].Trim().Split(' ')[0]);
                        }
                    }

                    // 사용된 메모리 계산
                    long memUsed = memTotal - memFree;

                    // 메모리 사용률 계산
                    memUsage = (double)memUsed / memTotal * 100;

                    //Console.WriteLine($"Total Memory: {memTotal} kB");
                    //Console.WriteLine($"Free Memory: {memFree} kB");
                    //Console.WriteLine($"Used Memory: {memUsed} kB");
                    Console.WriteLine($"Memory Usage: {memUsage:F2}%");
                    soapclient.MEMORY(memUsage.ToString("N1"), IPAddress);


                    soapclient.ALL(cpuUsage.ToString("N1"), memUsage.ToString("N1"), null, IPAddress);

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
                            //Console.WriteLine("net.Id: {0}", net.Id); // 네트워크의 고유id
                            //Console.WriteLine("net.Name: {0}", net.Name); // 표기되는 이름
                            //Console.WriteLine("net.IsReceiveOnly: {0}", net.IsReceiveOnly);
                            //Console.WriteLine("net.OperationalStatus: {0}", net.OperationalStatus); // 연결됐습니까?
                            //Console.WriteLine("net.NetworkInterfaceType: {0}", net.NetworkInterfaceType); // 구분용??
                            //Console.WriteLine("net.Description: {0}", net.Description); // 장치설명
                            //Console.WriteLine("net.SupportsMulticast: {0}", net.SupportsMulticast);
                            //Console.WriteLine("------------------");
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


                        Console.WriteLine(soapclient.create_log(IPAddress, cpuUsage.ToString("N1"), memUsage.ToString("N1"), total1.ToString()));


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
