using Renci.SshNet;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;
using System.Management;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;

namespace LNM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //LNM_TRA 감시 살리기
                Service_reload.RELOAD reload = new Service_reload.RELOAD();
                //윈도우,리눅스 서비스 살리기
                SERVICECLS.SERVICECLS cls = new SERVICECLS.SERVICECLS();
                //자동 장비등록
                autoadd.Service2 cls2 = new autoadd.Service2();
                //장비 살아있는지 없는지 핑 테스트
                pingtime.Class1 cls4 = new pingtime.Class1();
                Dashbord_resource.Class1 cls1 = new Dashbord_resource.Class1();
                //시스템 원격으로 알아보는 근데 이제 안씀 보안상 문제
                //systeminfo.Class1 cls6 = new systeminfo.Class1();
                //비밀번호 변경 안쓰고있음
                //changepwd.Class1 cls7 = new changepwd.Class1();
                //리눅스,보안,스위치장비 서비스
                //SecurityServer.Class1 Sercurity = new SecurityServer.Class1();
                //txt로 파일을 떨어뜨려주면 그 파일을 읽어 이벤트 읽기
                //EventRead.Class1 eventread = new EventRead.Class1();
                //snmptrap 읽기
                SNMPTRAP.Class1 SNMPTRAP = new SNMPTRAP.Class1();
                //AP장비
                //AP.Class1 APThread = new AP.Class1();
                //라이센스
                License.Class1 License = new License.Class1();
                //00-E0-4C-62-22-EC
                //if (License.uuid("20244D56-F6BD-E22A-1D52-3669DF035654") == true)
                //{


                //// LNM_TRA, Client 감시
                Thread thread = new Thread(reload.reload);
                thread.Start();

                //// 자동등록
                Thread thread3 = new Thread(cls2.autoadd);
                thread3.Start();

                ///윈도우서비스
                Thread thread1 = new Thread(cls.service_window_thread);
                thread1.Start();
                
                /////핑 확인
                Thread thread2 = new Thread(cls4.pingthread);
                thread2.Start();

                ///////리눅스 서비스
                //Thread thread1_1 = new Thread(cls.service_linux_thread);
                //thread1_1.Start();



                ////보안장비
                //Thread thread11 = new Thread(Sercurity.Securethread);
                //thread11.Start();

                ///////리눅스
                //Thread thread12 = new Thread(Sercurity.Linuxthread);
                //thread12.Start();

                //////AP장비
                //Thread thread17 = new Thread(APThread.APThread);
                //thread17.Start();

                /////대시보드보안장비
                Thread thread13 = new Thread(cls1.secure);
                thread13.Start();

                /////대시보드서버
                Thread thread14 = new Thread(cls1.server);
                thread14.Start();

                /////이벤트(안씀)
                //Thread thread15 = new Thread(eventread.readtext);
                //thread15.Start();

                /////SNMPTRAP
                Thread thread16 = new Thread(SNMPTRAP.snmptrap);
                thread16.Start();
                //}
                //else
                //{
                //    Console.WriteLine("라이센스가 맞지 않습니다");
                //}







                ////Thread thread4 = new Thread(cls6.systeminfo_thread);
                ////thread4.Start();

                ////Thread thread6 = new Thread(cls3.networkinfo_thread);
                ////thread6.Start();

                /////비밀번호 변경
                //Thread thread7 = new Thread(cls7.changepwd);
                //thread7.Start();


                //test();


                // test2();
                //test3();





                //SimpleSnmp snmp = new SimpleSnmp("192.168.0.210", "public");
                //Pdu pdu = new Pdu();

                ////유선 사용하는 맥주소
                //Dictionary<Oid, AsnType> result = snmp.Walk(SnmpVersion.Ver2, "1.3.6.1.4.1.11898.2.1.33.1.1.2");
                //if (result == null)
                //{
                //    Console.WriteLine("Request failed.");
                //}
                //else
                //{


                //    foreach (KeyValuePair<Oid, AsnType> entry in result)
                //    {
                //        //Console.WriteLine( entry.Key.ToString() + " , " + entry.Value.ToString());
                //        //list.Add(entry.Value.ToString());
                //        Console.WriteLine(entry.Value.ToString());



                //    }
                //}









                //    //CON.Close();
                //    //DBCON.Class1 DBCON = new DBCON.Class1();
                //    //MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                //    //string SQL = "";
                //    //SQL = "select @ROWNUM := @ROWNUM + 1 as rownum, portname from secure_port_traffic where serverip = '192.168.0.200' ";
                //    //MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                //    //DataSet DBSET = new DataSet();
                //    //ADT.Fill(DBSET, "BD");
                //    //foreach (DataRow row in DBSET.Tables["BD"].Rows)
                //    //{
                //    //    Console.WriteLine(row["portname"].ToString());
                //    //}



                //라이선스 
                //}
                //else
                //{
                //    Console.WriteLine("라이센스 에러");
                //}





            }
            catch (Exception e)
            {
                Console.WriteLine("에러");

                string sDirPath;
                sDirPath = @"C:\NMS(maria)\debug";
                DirectoryInfo di = new DirectoryInfo(sDirPath);
                if (di.Exists == false)
                {
                    di.Create();
                }

                string path = @"C:\NMS(maria)\debug\error.txt"; // path to file
                string txt = e.Message + "\n";
                File.AppendAllText(path, txt);

            }
        }




        public static void test3()
        {
            try
            {
                string ComputerName = "localhost";
                ManagementScope Scope;
                Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", ComputerName), null);
                Scope.Connect();
                ObjectQuery Query = new ObjectQuery("SELECT UUID FROM Win32_ComputerSystemProduct");
                ManagementObjectSearcher Searcher = new ManagementObjectSearcher(Scope, Query);

                foreach (ManagementObject WmiObject in Searcher.Get())
                {
                    Console.WriteLine("{0,-35} {1,-40}", "UUID", WmiObject["UUID"]);// String                     
                    //uidvalue = WmiObject["UUID"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Exception {0} Trace {1}", e.Message, e.StackTrace));
            }

        }
        public static void test2()
        {
            try
            {
                ConnectionOptions con = new ConnectionOptions();
                con.Username = "jun";
                con.Password = "dnjsvltm1";
                ManagementScope servercon = new ManagementScope(@"\\" + "." + @"\root\cimv2", con);

                servercon.Connect();

                ObjectQuery computer_name = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
                ManagementObjectSearcher computer_name2 = new ManagementObjectSearcher(servercon, computer_name);
                // Loop through the drives retrieved, although it should normally be only one loop going on here
                ManagementObjectCollection loResult = computer_name2.Get();

                foreach (ManagementObject incomputer_name in loResult)
                {
                    Console.WriteLine(incomputer_name["name"].ToString());


                }
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }




        public static void test()
        {

            /////CUI OS VERSION
            //try
            //{
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.172", "jjflysky");
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
            //            if(entry.Value.ToString() == "Null")
            //            {

            //            }
            //            else
            //            {
            //                Console.WriteLine("OS : " + entry.Value.ToString());
            //            }

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            //}

            ////hostname
            //try
            //{
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.172", "jjflysky");
            //    Pdu pdu = new Pdu();
            //    Pdu pdu1 = new Pdu();
            //    pdu.VbList.Add(".1.3.6.1.2.1.1.5.0");

            //    Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);


            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {
            //        string hostname = "";
            //        foreach (KeyValuePair<Oid, AsnType> entry in result)
            //        {
            //            hostname = entry.Value.ToString();
            //            Console.WriteLine(entry.Value.ToString());
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{

            //}


            double percen = 0;
            /// MEMORY
            try
            {
                SimpleSnmp snmp = new SimpleSnmp("192.168.0.170", "public");
                Pdu pdu = new Pdu();
                Pdu pdu1 = new Pdu();
                //토탈
                pdu.VbList.Add(".1.3.6.1.4.1.2021.4.5.0");
                //사용량
                pdu1.VbList.Add(".1.3.6.1.4.1.2021.4.5.0");
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
                    Console.WriteLine(percen.ToString("#.#") + "%");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            }

            // percen = 0;
            /////HD
            //try
            //{
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.172", "jjflysky");
            //    Pdu pdu = new Pdu();
            //    Pdu pdu1 = new Pdu();
            //    //토탈
            //    pdu.VbList.Add(".1.3.6.1.4.1.9560.1.10.2.3.3.2.0");
            //    //사용량
            //    pdu1.VbList.Add(".1.3.6.1.4.1.9560.1.10.2.3.3.3.0");
            //    Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
            //    Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);

            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {
            //        string total = "";
            //        string used = "";
            //        foreach (KeyValuePair<Oid, AsnType> entry in result)
            //        {
            //            total = entry.Value.ToString();
            //            //Console.WriteLine(entry.Value.ToString());
            //        }
            //        foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
            //        {
            //            used = entry1.Value.ToString();
            //            //Console.WriteLine(entry1.Value.ToString());
            //        }
            //        percen = (Convert.ToDouble(used) / Convert.ToDouble(total)) * 100;
            //        Console.WriteLine("SDB : " + percen.ToString("#.#") + "%");

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            //}


            /////CPU
            //try
            //{
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.170", "public");
            //    Pdu pdu = new Pdu();
            //    //1분 평균
            //    pdu.VbList.Add(".1.3.6.1.4.1.9560.1.10.2.3.1.1.6.0");
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
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.172", "jjflysky");
            //    Pdu pdu = new Pdu();
            //    Pdu pdu1 = new Pdu();
            //    Pdu pdu2 = new Pdu();
            //    Pdu pdu3 = new Pdu();
            //    //inboundinputpackets
            //    pdu.VbList.Add(".1.3.6.1.4.1.9560.1.10.2.4.3.0");
            //    //inboundinputbytes
            //    pdu1.VbList.Add(".1.3.6.1.4.1.9560.1.10.2.4.4.0");
            //    //outboundinputpackets
            //    pdu2.VbList.Add(".1.3.6.1.4.1.9560.1.10.2.4.5.0");
            //    //outboundinputbytes
            //    pdu3.VbList.Add(".1.3.6.1.4.1.9560.1.10.2.4.6.0");
            //    Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); 
            //    Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); 
            //    Dictionary<Oid, AsnType> result2 = snmp.Get(SnmpVersion.Ver2, pdu2); 
            //    Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu3); 
            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {
            //        foreach (KeyValuePair<Oid, AsnType> entry in result)
            //        {
            //            Console.WriteLine("INboundinputpackets : " + entry.Value.ToString() + " Packets");
            //        }
            //        foreach (KeyValuePair<Oid, AsnType> entry in result1)
            //        {
            //            Console.WriteLine("INboundinputbytes : " + entry.Value.ToString() + " bytes");
            //        }
            //        foreach (KeyValuePair<Oid, AsnType> entry in result2)
            //        {
            //            Console.WriteLine("OUTboundinputpackets : " + entry.Value.ToString() + " Packets");
            //        }
            //        foreach (KeyValuePair<Oid, AsnType> entry in result3)
            //        {
            //            Console.WriteLine("OUTboundinputbytes : " + entry.Value.ToString() + " bytes");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            //}
            //포트
            //try
            //{
            //    int portcount = 0;
            //    SimpleSnmp snmp = new SimpleSnmp("192.168.0.172", "jjflysky");
            //    Pdu pdu = new Pdu();
            //    //포트 카운트
            //    pdu.VbList.Add(".1.3.6.1.2.1.2.1.0");
            //    Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {

            //        foreach (KeyValuePair<Oid, AsnType> entry in result)
            //        {
            //            Console.WriteLine("포트갯수 : " + entry.Value.ToString() + "개");
            //            portcount = Convert.ToInt32(entry.Value.ToString());

            //        }

            //    }

            //    string[] portname = { };
            //    string a = "";
            //    for (int i = 1; i < portcount + 1; i++)
            //    {
            //        SimpleSnmp snmp1 = new SimpleSnmp("192.168.0.254", "public");
            //        Pdu pdu1 = new Pdu();
            //        //포트들 이름
            //        pdu1.VbList.Add(".1.3.6.1.2.1.31.1.1.1.1." + i);
            //        Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
            //        if (result == null)
            //        {
            //            Console.WriteLine("Request failed.");
            //        }
            //        else
            //        {


            //            foreach (KeyValuePair<Oid, AsnType> entry in result1)
            //            {
            //                a += entry.Value.ToString() + ",";

            //                // Console.WriteLine("포트이름들 : " + entry.Value.ToString());

            //            }
            //            //for (int j = 1; j < portcount + 1; j++)
            //            //{
            //            //    portname[0] = a.Split('\n').ToString();
            //            //}

            //        }

            //        //Console.WriteLine("포트이름들 : " + portname[0]);
            //    }
            //    portname = a.Split(',');
            //    for (int j = 1; j < portcount + 1; j++)
            //    {
            //        //Console.WriteLine(portname[j]);
            //    }

            //}
            //catch
            //{

            //}


            //    for (int i = 1; i < portcount + 1; i++)
            //{
            //    SimpleSnmp snmp1 = new SimpleSnmp("192.168.0.254", "public");
            //    Pdu pdu1 = new Pdu();
            //    //살아있는지 1삼 2죽음
            //    pdu1.VbList.Add(".1.3.6.1.2.1.2.2.1.7." + i);
            //    Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {

            //        foreach (KeyValuePair<Oid, AsnType> entry in result1)
            //        {
            //            Console.WriteLine("살아있나 : " + entry.Value.ToString());

            //        }
            //    }
            //}




            ////트래픽 들어오는 양 체크
            //for (int i = 1; i < portcount + 1; i++)
            //{
            //    SimpleSnmp snmp1 = new SimpleSnmp("192.168.0.254", "public");
            //    Pdu pdu1 = new Pdu();
            //    Pdu pdu2 = new Pdu();
            //    //살아있는지 1삼 2죽음
            //    pdu1.VbList.Add(".1.3.6.1.2.1.2.2.1.10." + i);
            //    pdu2.VbList.Add(".1.3.6.1.2.1.2.2.1.16." + i);
            //    Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
            //    Dictionary<Oid, AsnType> result2 = snmp.Get(SnmpVersion.Ver2, pdu2); //.GetNext(pdu);
            //    double total = 0;
            //    double total1 = 0;
            //    double traffic = 0;
            //    double traffic1 = 0;
            //    if (result == null)
            //    {
            //        Console.WriteLine("Request failed.");
            //    }
            //    else
            //    {

            //        foreach (KeyValuePair<Oid, AsnType> entry in result1)
            //        {
            //            traffic = Convert.ToDouble(entry.Value.ToString());
            //            //Console.WriteLine("들어오는 : " + traffic);
            //        }
            //        foreach (KeyValuePair<Oid, AsnType> entry in result2)
            //        {
            //            traffic1 = Convert.ToDouble(entry.Value.ToString());
            //            //Console.WriteLine("나가는 : " + traffic1);
            //        }

            //        total = traffic + traffic1;
            //        //Console.WriteLine(i+ " : "+total +" bytes");

            //        Thread.Sleep(1000);
            //        //IN
            //        double total2 = 0;
            //        double traffic2 = 0;
            //        double traffic3 = 0;
            //        Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
            //        Dictionary<Oid, AsnType> result4 = snmp.Get(SnmpVersion.Ver2, pdu2); //.GetNext(pdu);
            //        if (result == null)
            //        {
            //            Console.WriteLine("Request failed.");
            //        }
            //        else
            //        {
            //            foreach (KeyValuePair<Oid, AsnType> entry in result3)
            //            {
            //                traffic2 = Convert.ToDouble(entry.Value.ToString());
            //                //Console.WriteLine("들어오는 : " + traffic);
            //            }
            //            foreach (KeyValuePair<Oid, AsnType> entry in result4)
            //            {
            //                traffic3 = Convert.ToDouble(entry.Value.ToString());
            //                //Console.WriteLine("나가는 : " + traffic1);
            //            }
            //            total2 = traffic2 + traffic3;
            //            total2 = total2 - total;

            //            Console.WriteLine(i + " : " + total2 + " bytes");

            //        }
            //    }
            //}








        }
    }
}
