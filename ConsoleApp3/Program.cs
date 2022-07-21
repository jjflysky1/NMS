using System;
using System.Threading;
using System.Data.SqlClient;

using System.Management;
using System.Security;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.NetworkInformation;

using System.Net.Mail;
using SnmpSharpNet;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using Renci.SshNet;

namespace SSNMS
{

    class Programs
    {
        private static SqlConnection CON = new SqlConnection("Server =192.168.72.128; Database=cs; User id=sa; Password=P@ssw0rd");
 
    

        static void Main(string[] args)
        {
            try
            {
                //SERVICECLS.SERVICECLS cls = new SERVICECLS.SERVICECLS();
                //Service2.Service2 cls2 = new Service2.Service2();
                //Traffic.Class1 cls3 = new Traffic.Class1();
                //pingtime.Class1 cls4 = new pingtime.Class1();
                //OS.Class1 cls5 = new OS.Class1();
                //systeminfo.Class1 cls6 = new systeminfo.Class1();
                //changepwd.Class1 cls7 = new changepwd.Class1();
                //SecurityServer.Class1 Sercurity = new SecurityServer.Class1();

                ////Thread thread3 = new Thread(cls2.autoadd);
                ////thread3.Start();

                //Thread thread = new Thread(cls5.OS);
                //thread.Start();

                //Thread thread1 = new Thread(cls.service_window);
                //thread1.Start();

                //Thread thread1_1 = new Thread(cls.service_linux);
                //thread1_1.Start();

                //Thread thread2 = new Thread(cls4.pingtime);
                //thread2.Start();

                //Thread thread4 = new Thread(cls6.systeminfo);
                //thread4.Start();

                //Thread thread6 = new Thread(cls3.networkinfo);
                //thread6.Start();

                //Thread thread7 = new Thread(cls7.changepwd);
                //thread7.Start();

                //Thread thread8 = new Thread(cls6.HDinfo);
                //thread8.Start();

                ////Thread thread9 = new Thread(cls6.APPinfo);
                ////thread9.Start();

                //Thread thread10 = new Thread(cls6.computer_name);
                //thread10.Start();

                //Thread thread11 = new Thread(Sercurity.Secure);
                //thread11.Start();

                //Thread thread12 = new Thread(Sercurity.Linux);
                //thread12.Start();

                //test();


                //test2();
            }
            catch (Exception e)
            {
                Console.WriteLine("에러");
            }
            
        }

        public static void test2()
        {
            Console.WriteLine("192.168.233.130" + " status -Connecting...");
            SshClient ssh = new SshClient("192.168.233.130",22, "root", "dnjsvltm1");
            ssh.Connect();
            var sshcmd = ssh.RunCommand("ifconfig");
            sshcmd.Execute();
            Console.WriteLine(sshcmd.Result);

        }


     

        public static void test() 
        {

            /////OS VERSION
            //try
            //{
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.254", "public");
            //    Pdu pdu = new Pdu();
            //    pdu.VbList.Add(".1.3.6.1.2.1.1.1.0");
            //    Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {
            //        foreach (KeyValuePair<Oid, AsnType> entry in result)
            //        {
            //            Console.WriteLine("OS : " + entry.Value.ToString());
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            //}

            //double percen = 0;
            ///// MEMORY
            //try
            //{
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.254", "public");
            //    Pdu pdu = new Pdu();
            //    Pdu pdu1 = new Pdu();
            //    //토탈
            //    pdu.VbList.Add(".1.3.6.1.2.1.25.2.3.1.5.1");
            //    //사용량
            //    pdu1.VbList.Add(".1.3.6.1.2.1.25.2.3.1.6.1");
            //    Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
            //    Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);

            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {

            //        double total = 0;
            //        double free = 0;
            //        foreach (KeyValuePair<Oid, AsnType> entry in result)
            //        {
            //            total = Convert.ToDouble(entry.Value.ToString());
            //            //Console.WriteLine(entry.Value.ToString());
            //        }
            //        foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
            //        {
            //            free = Convert.ToDouble(entry1.Value.ToString());
            //            //Console.WriteLine(entry1.Value.ToString());
            //        }
            //        free = total - free;
            //        percen = (Convert.ToDouble(free) / Convert.ToDouble(total)) * 100;
            //        Console.WriteLine(percen.ToString("#.#") + "%");

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            //}

            double percen = 0;
            ///HD
            try
            {
                SimpleSnmp snmp = new SimpleSnmp("192.168.0.200", "public");
                Pdu pdu = new Pdu();
                Pdu pdu1 = new Pdu();
                //토탈
                pdu.VbList.Add(".1.3.6.1.2.1.25.2.3.1.5.3");
                //사용량
                pdu1.VbList.Add(".1.3.6.1.2.1.25.2.3.1.6.3");
                Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);

                if (result == null)
                {
                    Console.WriteLine("Request failed.");
                }
                else
                {
                    string total = "";
                    string used = "";
                    foreach (KeyValuePair<Oid, AsnType> entry in result)
                    {
                        total = entry.Value.ToString();
                        //Console.WriteLine(entry.Value.ToString());
                    }
                    foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
                    {
                        used = entry1.Value.ToString();
                        //Console.WriteLine(entry1.Value.ToString());
                    }
                    percen = (Convert.ToDouble(used) / Convert.ToDouble(total)) * 100;
                    Console.WriteLine("SDB : " + percen.ToString("#.#") + "%");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            }


            /////CPU
            //try
            //{
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.254", "public");
            //    Pdu pdu = new Pdu();
            //    //1분 평균
            //    pdu.VbList.Add("1.3.6.1.4.1.37288.1.1.3.1.1.0");
            //    Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {
            //        foreach (KeyValuePair<Oid, AsnType> entry in result)
            //        {
            //            Console.WriteLine("CPU : " + entry.Value.ToString() + "%");

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            //}

            ////트래픽
            //try
            //{
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.254", "public");
            //    Pdu pdu = new Pdu();
            //    //IN
            //    Dictionary<Oid, AsnType> result = snmp.Walk(SnmpVersion.Ver2, "1.3.6.1.2.1.31.1.1.1.6"); //.GetNext(pdu);
            //    //OUT
            //    Dictionary<Oid, AsnType> result2 = snmp.Walk(SnmpVersion.Ver2, "1.3.6.1.2.1.31.1.1.1.10");
            //    double total = 0;
            //    double total1 = 0;
            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {
            //        double traffic = 0;
            //        double traffic1 = 0;
            //        foreach (KeyValuePair<Oid, AsnType> entry in result)
            //        {
            //            traffic += Convert.ToDouble(entry.Value.ToString());
            //        }
            //        //Console.WriteLine("IN Traffic : " + traffic.ToString());
            //        foreach (KeyValuePair<Oid, AsnType> entry1 in result2)
            //        {
            //            traffic1 += Convert.ToDouble(entry1.Value.ToString());
            //        }
            //        //Console.WriteLine("OUT Traffic : " + traffic1.ToString());
            //         total = traffic + traffic1;
            //        //Console.WriteLine(total);

            //    }
            //    Thread.Sleep(1000);
            //    //IN
            //    Dictionary<Oid, AsnType> result3 = snmp.Walk(SnmpVersion.Ver2, "1.3.6.1.2.1.31.1.1.1.6"); //.GetNext(pdu);
            //    //OUT
            //    Dictionary<Oid, AsnType> result4 = snmp.Walk(SnmpVersion.Ver2, "1.3.6.1.2.1.31.1.1.1.10");

            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    { 
            //        double traffic = 0;
            //        double traffic1 = 0;
            //        foreach (KeyValuePair<Oid, AsnType> entry in result3)
            //        {
            //            traffic += Convert.ToDouble(entry.Value.ToString());
            //        }
            //        //Console.WriteLine("IN Traffic : " + traffic.ToString());
            //        foreach (KeyValuePair<Oid, AsnType> entry1 in result4)
            //        {
            //            traffic1 += Convert.ToDouble(entry1.Value.ToString());
            //        }
            //        // Console.WriteLine("OUT Traffic : " + traffic1.ToString());
            //        total1 = traffic + traffic1;
            //        total1 = total1 - total;
            //        Console.WriteLine(total1);
            //    }


            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            //}

            //포트
            try
            {
                int portcount = 0;
                SimpleSnmp snmp = new SimpleSnmp("192.168.0.200", "public");
                Pdu pdu = new Pdu();
                //포트 카운트
                pdu.VbList.Add(".1.3.6.1.2.1.2.1.0");
                Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                if (result == null)
                {
                    Console.WriteLine("Request failed.");
                }
                else
                {

                    foreach (KeyValuePair<Oid, AsnType> entry in result)
                    {
                        //Console.WriteLine("포트갯수 : " + entry.Value.ToString() + "개");
                        portcount = Convert.ToInt32(entry.Value.ToString());

                    }

                }
                string[] portname = { };
                string a = "";
                for (int i = 1; i < portcount + 1; i++)
                {
                    SimpleSnmp snmp1 = new SimpleSnmp("192.168.0.200", "public");
                    Pdu pdu1 = new Pdu();
                    //포트들 이름
                    pdu1.VbList.Add(".1.3.6.1.2.1.31.1.1.1.1." + i);
                    Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                    if (result == null)
                    {
                        Console.WriteLine("Request failed.");
                    }
                    else
                    {
                       

                        foreach (KeyValuePair<Oid, AsnType> entry in result1)
                        {
                            a += entry.Value.ToString() + ",";

                            // Console.WriteLine("포트이름들 : " + entry.Value.ToString());

                        }
                        //for (int j = 1; j < portcount + 1; j++)
                        //{
                        //    portname[0] = a.Split('\n').ToString();
                        //}
                        
                    }

                    //Console.WriteLine("포트이름들 : " + portname[0]);
                }
                portname = a.Split(',');
                for (int j = 1; j < portcount + 1; j++)
                {
                    Console.WriteLine(portname[j]);
                }
                
            }


            //    for (int i = 1; i < portcount+1; i++)
            //    {
            //        SimpleSnmp snmp1 = new SimpleSnmp("192.168.0.254", "public");
            //        Pdu pdu1 = new Pdu();
            //        //살아있는지 1삼 2죽음
            //        pdu1.VbList.Add(".1.3.6.1.2.1.2.2.1.7." + i);
            //        Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
            //        if (result == null)
            //        {
            //            Console.WriteLine("Request failed.");
            //        }
            //        else
            //        {

            //            foreach (KeyValuePair<Oid, AsnType> entry in result1)
            //            {
            //                Console.WriteLine("살아있나 : " + entry.Value.ToString());

            //            }
            //        }
            //    }




            //    //트래픽 들어오는 양 체크
            //    for (int i = 1; i < portcount + 1; i++)
            //    {
            //        SimpleSnmp snmp1 = new SimpleSnmp("192.168.0.254", "public");
            //        Pdu pdu1 = new Pdu();
            //        Pdu pdu2 = new Pdu();
            //        //살아있는지 1삼 2죽음
            //        pdu1.VbList.Add(".1.3.6.1.2.1.2.2.1.10." + i);
            //        pdu2.VbList.Add(".1.3.6.1.2.1.2.2.1.16." + i);
            //        Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
            //        Dictionary<Oid, AsnType> result2 = snmp.Get(SnmpVersion.Ver2, pdu2); //.GetNext(pdu);
            //        double total = 0;
            //        double total1 = 0;
            //        double traffic = 0;
            //        double traffic1 = 0;
            //        if (result == null)
            //        {
            //            Console.WriteLine("Request failed.");
            //        }
            //        else
            //        {

            //            foreach (KeyValuePair<Oid, AsnType> entry in result1)
            //            {
            //                traffic = Convert.ToDouble(entry.Value.ToString());
            //                //Console.WriteLine("들어오는 : " + traffic);
            //            }
            //            foreach (KeyValuePair<Oid, AsnType> entry in result2)
            //            {
            //                traffic1 = Convert.ToDouble(entry.Value.ToString());
            //                //Console.WriteLine("나가는 : " + traffic1);
            //            }

            //            total = traffic + traffic1;
            //            //Console.WriteLine(i+ " : "+total +" bytes");



            //            foreach (KeyValuePair<Oid, AsnType> entry in result1)
            //            {
            //                traffic = Convert.ToDouble(entry.Value.ToString());
            //                //Console.WriteLine("들어오는 : " + traffic);
            //            }
            //            foreach (KeyValuePair<Oid, AsnType> entry in result2)
            //            {
            //                traffic1 = Convert.ToDouble(entry.Value.ToString());
            //                //Console.WriteLine("나가는 : " + traffic1);
            //            }

            //            total1 = traffic + traffic1;

            //            Console.WriteLine(i + " : " + total1 + " bytes");

            //        }
            //    }





            //}
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            }

        }




    }
}