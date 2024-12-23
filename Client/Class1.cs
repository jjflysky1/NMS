using System;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Class1
    {
        public void SYSTEMINFO()
        {

            try
            {
                while (true)
                {

                    SOAP.Systeminfo soapclient = new SOAP.Systeminfo();

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
                    Console.WriteLine(IPAddress);


                    ///Computer Name
                    try
                    {
                        ManagementObjectSearcher searcher =
                            new ManagementObjectSearcher("root\\CIMV2", "SELECT Name FROM Win32_ComputerSystem");

                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            Console.WriteLine("Name: {0}", queryObj["Name"]);
                            soapclient.COMPUTERNAME(queryObj["Name"].ToString(), IPAddress);
                        }
                    }
                    catch (ManagementException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    ///OS 
                    try
                    {
                        ManagementObjectSearcher searcher =
                            new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption FROM Win32_OperatingSystem");

                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            Console.WriteLine("OS: {0}", queryObj["Caption"]);
                            soapclient.OS(queryObj["Caption"].ToString(), IPAddress);
                        }
                    }
                    catch (ManagementException e)
                    {
                        Console.WriteLine(e.Message);
                    }


                    ///CPU 
                    ///    
                    int i = 0;
                    int sumcpu = 0;
                    int cpu = 0;
                    try
                    {
                        ManagementObjectSearcher searcher =
                            new ManagementObjectSearcher("root\\CIMV2", "SELECT LoadPercentage FROM Win32_Processor");

                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            Console.WriteLine("CPU: {0}", queryObj["LoadPercentage"]);
                            i++;
                            sumcpu += Convert.ToInt32(queryObj["LoadPercentage"]);
                            cpu = Convert.ToInt32(queryObj["LoadPercentage"]);

                        }
                    }
                    catch (ManagementException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    sumcpu = Convert.ToInt32(cpu) / i;
                    try
                    {
                        soapclient.CPU(sumcpu.ToString(), IPAddress);
                    }
                    catch
                    {

                    }

                    ///MEMORY 
                    double real = 0;
                    try
                    {
                        ManagementObjectSearcher searcher =
                            new ManagementObjectSearcher("root\\CIMV2", "SELECT FreePhysicalMemory,TotalVisibleMemorySize FROM Win32_OperatingSystem");

                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            double free = Double.Parse(queryObj["FreePhysicalMemory"].ToString());
                            double total = Double.Parse(queryObj["TotalVisibleMemorySize"].ToString());

                            real = (total - free) / total * 100;
                            Console.WriteLine("Memory: " + real.ToString("N1"));
                            soapclient.MEMORY(real.ToString("N1"), IPAddress);

                        }
                    }
                    catch (ManagementException e)
                    {
                        Console.WriteLine(e.Message);
                    }


                    ///DISK 
                    int iPercent = 0;
                    string Drives = "";
                    try
                    {
                        ManagementObjectSearcher searcher =
                            new ManagementObjectSearcher("root\\CIMV2", "SELECT Size,FreeSpace,Name FROM Win32_LogicalDisk where drivetype=3");

                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            decimal Size = Convert.ToDecimal(queryObj["Size"]) / 1073741824;
                            decimal FreeSpace = Convert.ToDecimal(queryObj["FreeSpace"]) / 1073741824;
                            decimal usespace = Size - FreeSpace;
                            iPercent = Convert.ToInt32((usespace / Size) * 100);
                            string sDriveLetter = Convert.ToString(queryObj["Name"]);
                            Console.WriteLine(sDriveLetter + " " + iPercent + "%");

                            Drives += sDriveLetter + " " + iPercent + "%  ";
                            soapclient.DISK(Drives, IPAddress);
                        }
                    }
                    catch (ManagementException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    soapclient.ALL(cpu.ToString(), real.ToString("N1"), Drives, IPAddress);

                    ///트래픽
                    float traffic = 0;
                    try
                    {
                        ManagementObjectSearcher searcher =
                            new ManagementObjectSearcher("root\\CIMV2", "SELECT name,BytesTotalPersec FROM Win32_PerfFormattedData_TCPIP_NetworkInterface");

                        foreach (ManagementObject queryObj in searcher.Get())
                        {
                            if (Convert.ToDecimal(queryObj["BytesTotalPersec"]) > 0)
                            {
                                Console.WriteLine("Name: {0}", queryObj["name"]);
                                Console.WriteLine("Traffic: {0}", queryObj["BytesTotalPersec"]);
                                Console.WriteLine(soapclient.Traffic(queryObj["BytesTotalPersec"].ToString(), IPAddress));
                                traffic += Convert.ToInt32(queryObj["BytesTotalPersec"]);
                                
                            }
                            
                        }
                        Console.WriteLine(traffic);
                        
                    }
                    catch (ManagementException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    try
                    {

                        Console.WriteLine(soapclient.create_log(IPAddress, sumcpu.ToString(), real.ToString("N1"), traffic.ToString()));
                        //soapclient.create_log(IPAddress, sumcpu.ToString(), real.ToString("N1"), traffic);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }


                    //Thread.Sleep(30000);
                    Thread.Sleep(5000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }






        }
    }
}
