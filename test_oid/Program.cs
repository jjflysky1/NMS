using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace test_oid
{
    internal class Program
    {
        static void run(string comm, string serverip, string mib)
        {

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c snmpwalk -v 2c -c " + comm + " " + serverip + " " + mib,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            //string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            //Console.WriteLine(output);

            List<string> list = new List<string>();
            while (!process.StandardOutput.EndOfStream)
            {
                list.Add(process.StandardOutput.ReadLine());
            }
            foreach (string line in list)
            {
                string[] result = line.Split('\x020');
                Console.WriteLine(result[4] + result[5] + result.Last());
            }

        }

        static void run2(string comm, string serverip, string mib)
        {
            string[] inresult = { };
            string[] outresult = { };
            string temp1 = "";
            string temp2 = "";
            double totalresult = 0;

            //포트이름
            var process_port = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c snmpwalk -v 2c -c " + comm + " " + serverip + " " + "ifdescr",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process_port.Start();
            //string output = process.StandardOutput.ReadToEnd();
            process_port.WaitForExit();
            //Console.WriteLine(output);

            List<string> list_port = new List<string>();
            while (!process_port.StandardOutput.EndOfStream)
            {
                //list_port.Add(process_port.StandardOutput.ReadLine());
                inresult = process_port.StandardOutput.ReadLine().Split('\x020');
                list_port.Add(inresult.Last());
            }
            

            //포트사용여부
            var process_live = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c snmpwalk -v 2c -c " + comm + " " + serverip + " " + "ifoperstatus",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process_live.Start();
            //string output = process.StandardOutput.ReadToEnd();
            process_live.WaitForExit();
            //Console.WriteLine(output);

            List<string> list_live = new List<string>();
            while (!process_live.StandardOutput.EndOfStream)
            {
                //list_live.Add(process_live.StandardOutput.ReadLine());
                string[] result = process_live.StandardOutput.ReadLine().Split('\x020');
                string temp = "";
                if (result.Last() == "up(1)")
                {
                    temp = "1";
                }
                if (result.Last() == "down(2)")
                {
                    temp = "2";
                }
                list_live.Add(temp);
            }

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c snmpwalk -v 2c -c " + comm + " " + serverip + " " + "ifinOctets",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            //string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            //Console.WriteLine(output);

            List<string> list = new List<string>();
            while (!process.StandardOutput.EndOfStream)
            {
                //list.Add(process.StandardOutput.ReadLine());
                outresult = process.StandardOutput.ReadLine().Split('\x020');
                list.Add(outresult.Last());
            }

            var process2 = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c snmpwalk -v 2c -c " + comm + " " + serverip + " " + "ifoutOctets",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process2.Start();
            //string output = process.StandardOutput.ReadToEnd();
            process2.WaitForExit();
            //Console.WriteLine(output);

            List<string> list2 = new List<string>();
            while (!process2.StandardOutput.EndOfStream)
            {
                //list2.Add(process2.StandardOutput.ReadLine());
                outresult = process2.StandardOutput.ReadLine().Split('\x020');
                list2.Add(outresult.Last());
            }
          
            List<Double> total = new List<Double>();
            for (int i = 0; i < list2.Count; i++)
            {
                total.Add(Convert.ToInt64(list[i].ToString()) + Convert.ToInt64(list2[i].ToString()));
                //Console.WriteLine("인 : " + list_1[i].ToString());
                //Console.WriteLine("아웃 :" + list2_1[i].ToString());
                //Console.WriteLine("토탈 : " + total[i]);
            }
            Thread.Sleep(1000);

            var process3 = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c snmpwalk -v 2c -c " + comm + " " + serverip + " " + "ifinOctets",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process3.Start();
            //string output = process.StandardOutput.ReadToEnd();
            process3.WaitForExit();
            //Console.WriteLine(output);

            List<string> list3 = new List<string>();
            while (!process3.StandardOutput.EndOfStream)
            {
                //list3.Add(process3.StandardOutput.ReadLine());
                inresult = process3.StandardOutput.ReadLine().Split('\x020');
                list3.Add(inresult.Last());
            }

            var process4 = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c snmpwalk -v 2c -c " + comm + " " + serverip + " " + "ifoutOctets",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process4.Start();
            //string output = process.StandardOutput.ReadToEnd();
            process4.WaitForExit();
            //Console.WriteLine(output);

            List<string> list4 = new List<string>();
            while (!process4.StandardOutput.EndOfStream)
            {
                //list4.Add(process4.StandardOutput.ReadLine());
                outresult = process4.StandardOutput.ReadLine().Split('\x020');
                list4.Add(outresult.Last());
            }

            List<Double> total_1 = new List<Double>();
            for (int i = 0; i < list2.Count; i++)
            {
                total_1.Add(Convert.ToInt64(list3[i].ToString()) + Convert.ToInt64(list4[i].ToString()));
                //Console.WriteLine("인 : " + list3_1[i].ToString());
                //Console.WriteLine("아웃 :" + list4_1[i].ToString());
                Console.WriteLine(list_port[i] +"토탈 : " + (total_1[i] - total[i]) + " 죽살 : " + list_live[i]);
            }




        }
        static void Main(string[] args)
        {
            while(true)
            {
                string comm = "anjw77";
                string serverip = "192.168.0.250";
                string mib_uptime = "sysuptime"; // 업타임
                string mib_interface = "ifinOctets"; // 포트트래픽
                string mib_outterface = "ifoutOctets"; // 포트트래픽
                string mib_portname = "ifdescr"; // 포트이름
                string mib_portstatus = "ifoperstatus"; //포트 상태

                //run(comm, serverip, mib_uptime);

                run2(comm, serverip, mib_interface);
                
                //run2(comm, serverip, mib_outterface);

                //run2(comm, serverip, mib_portname);

                //run2(comm, serverip, mib_portstatus);

                Thread.Sleep(5000);
            }
            
            //list.Add(output);

            //string[] result = { };
            //foreach (string i in list)
            //{
            //    Console.Write("{0}\n", i.ToString());
            //}
            //result[0].ToString();

            //Console.WriteLine(string.Join("\t", line));

            //double percen = 0;
            //try
            //{
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.170", "public");
            //    Pdu pdu = new Pdu();
            //    Pdu pdu1 = new Pdu();
            //    Pdu pdu2 = new Pdu();
            //    Pdu pdu3 = new Pdu();

            //    pdu.VbList.Add(".1.3.6.1.4.1.2021.4.5.0");//토탈
            //    pdu1.VbList.Add(".1.3.6.1.4.1.2021.4.6.0");//유즈
            //    pdu2.VbList.Add(".1.3.6.1.4.1.2021.4.14.0");//버퍼
            //    pdu3.VbList.Add(".1.3.6.1.4.1.2021.4.15.0");//캐시
            //    Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
            //    Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
            //    Dictionary<Oid, AsnType> result2 = snmp.Get(SnmpVersion.Ver2, pdu2); //.GetNext(pdu);
            //    Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu3); //.GetNext(pdu);

            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {
            //        double total = 0;
            //        double use = 0;
            //        double buffer = 0;
            //        double cache = 0;
            //        foreach (KeyValuePair<Oid, AsnType> entry in result)
            //        {
            //            total = Convert.ToDouble(entry.Value.ToString());
            //            //Console.WriteLine(entry.Value.ToString());
            //        }
            //        foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
            //        {
            //            use = Convert.ToDouble(entry1.Value.ToString());
            //            //Console.WriteLine(entry1.Value.ToString());
            //        }
            //        foreach (KeyValuePair<Oid, AsnType> entry2 in result2)
            //        {
            //            buffer = Convert.ToDouble(entry2.Value.ToString());
            //            //Console.WriteLine(entry1.Value.ToString());
            //        }
            //        foreach (KeyValuePair<Oid, AsnType> entry1 in result3)
            //        {
            //            cache = Convert.ToDouble(entry1.Value.ToString());
            //            //Console.WriteLine(entry1.Value.ToString());
            //        }
            //        Console.WriteLine("total : " + total);
            //        Console.WriteLine("use : " + use);
            //        Console.WriteLine("buffer : " + buffer);
            //        Console.WriteLine("cache : " + cache);
            //        percen = total / (total - cache) * 0.01;
            //        Console.WriteLine(percen);

            //        use = total - (use - (buffer + cache));
            //        percen = total / (total - cache);
            //        Console.WriteLine(percen.ToString("#.#") + "%");

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            //}
        }
    }
}
