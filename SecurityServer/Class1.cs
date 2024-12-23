using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityServer
{
    public class Class1
    {
        /// <summary>
        /// 
        /// 라이센스 갯수
        /// </summary>
        /// 

        int sleep = 10000;
        int trafficsleep = 1000;
        ///10초 : 10000
        ///1분 : 60000

        public void Securethread()
        {
            try
            {
                while (true)
                {
                    DBCON.Class1 DBCON = new DBCON.Class1();
                    MySqlConnection CON = new MySqlConnection(DBCON.DBCON);

                    ///
                    ///특정날짜 넘어가면 아웃
                    ///
                    string SQL2 = "select  date_format(now(),'%Y%m%d') as time";
                    MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL2, CON);
                    DataSet DBSET2 = new DataSet();
                    ADT2.Fill(DBSET2, "BD2");
                    string time = "";
                    foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                    {
                        Console.WriteLine("시간 ================================================================" + row2["time"].ToString());
                        time = row2["time"].ToString();
                    }

                    if (Convert.ToInt32(time) > Convert.ToInt32(DBCON.date))
                    {
                        Process[] processes = Process.GetProcessesByName("alert");
                        if (processes.Length == 0)
                        {
                            Process.Start("C:/NMS(maria)/alert.exe");
                            //Console.WriteLine("Not running");
                        }
                        else
                        {

                            //Console.WriteLine("Running");
                        }
                        //break;

                    }

                    string SQL1 = "";
                    string[] serverip = { };
                    string tempip = "";
                    int count = 0;
                    MySqlDataAdapter ADT1 = new MySqlDataAdapter("Server_list_check", CON);
                    ADT1.SelectCommand.CommandType = CommandType.StoredProcedure;
                    ADT1.SelectCommand.Parameters.AddWithValue("@where1", "네트워크/보안 장비");
                    ADT1.SelectCommand.Parameters.AddWithValue("@liesence", DBCON.Liesence);
                    DataSet DBSET1 = new DataSet();
                    ADT1.Fill(DBSET1, "BD1");
                    foreach (DataRow row in DBSET1.Tables["BD1"].Rows)
                    {
                        tempip += row["serverip"].ToString() + ",";
                        count++;
                    }

                    serverip = tempip.Split(',');
                    Thread[] thread = new Thread[count];

                    try
                    {
                        //작업갯수
                        int maxWorkingCount = count;
                        int workingRangeSize = 1;
                        if (maxWorkingCount > Environment.ProcessorCount)
                        {
                            workingRangeSize = maxWorkingCount / Environment.ProcessorCount;
                        }
                        //스레드 최대갯수
                        var options = new ParallelOptions()
                        {
                            MaxDegreeOfParallelism = workingRangeSize
                        };
                        Parallel.For(0, count, options, i =>
                        {
                            //Secure(serverip[i]);
                            var task1 = Task.Run(() => Secure(serverip[i]));
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        //string sDirPath;
                        //sDirPath = @"C:\NMS(maria)\debug";
                        //DirectoryInfo di = new DirectoryInfo(sDirPath);
                        //if (di.Exists == false)
                        //{
                        //    di.Create();
                        //}

                        //string path = @"C:\NMS(maria)\debug\error.txt"; // path to file
                        //string txt = e.Message + "\n";
                        //File.AppendAllText(path, txt);

                    }



                    //for (int i = 0; i < count; i++)
                    //{
                    //    thread[i] = new Thread(delegate ()
                    //    {
                    //        Secure(serverip[i]);
                    //    });

                    //    thread[i].Start();
                    //    //thread[i].IsBackground = true;
                    //    Thread.Sleep(500);
                    //}
                    //for (int i = 0; i < count; i++)
                    //{
                    //    thread[i].Join(60000);
                    //    Thread.Sleep(500);
                    //    thread[i].Abort();

                    //}

                    Thread.Sleep(sleep);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("시작스레드 에러");
                Console.WriteLine(e.Message);
            }
        }

        public void Linuxthread()
        {
            //try
            //{
            //    while (true)
            //    {
            //        DBCON.Class1 DBCON = new DBCON.Class1();
            //        MySqlConnection CON = new MySqlConnection(DBCON.DBCON);

            //        ///
            //        ///특정날짜 넘어가면 아웃
            //        ///
            //        string SQL2 = "select " +
            //            "" +
            //            " date_format(now(),'%Y%m%d') as time";
            //        MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL2, CON);
            //        DataSet DBSET2 = new DataSet();
            //        ADT2.Fill(DBSET2, "BD2");
            //        string time = "";
            //        foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
            //        {
            //            Console.WriteLine("시간 ================================================================" + row2["time"].ToString());
            //            time = row2["time"].ToString();
            //        }
            //        if (Convert.ToInt32(time) > Convert.ToInt32(DBCON.date))
            //        {
            //            //break;
            //        }


            //        int count = 0;


            //        string[] serverip = { };
            //        string tempip = "";
            //        MySqlDataAdapter ADT1 = new MySqlDataAdapter("Server_list_check", CON);
            //        ADT1.SelectCommand.CommandType = CommandType.StoredProcedure;
            //        ADT1.SelectCommand.Parameters.AddWithValue("@where1", "서버 장비");
            //        ADT1.SelectCommand.Parameters.AddWithValue("@liesence", DBCON.Liesence);
            //        DataSet DBSET1 = new DataSet();
            //        ADT1.Fill(DBSET1, "BD1");
            //        foreach (DataRow row in DBSET1.Tables["BD1"].Rows)
            //        {

            //            tempip += row["serverip"].ToString() + ",";
            //            count++;
            //        }

            //        serverip = tempip.Split(',');
            //        Thread[] thread = new Thread[count];
            //        //작업갯수
            //        int maxWorkingCount = count;
            //        int workingRangeSize = 1;
            //        if (maxWorkingCount > Environment.ProcessorCount)
            //        {
            //            workingRangeSize = maxWorkingCount / Environment.ProcessorCount;
            //        }
            //        //스레드 최대갯수
            //        var options = new ParallelOptions()
            //        {
            //            MaxDegreeOfParallelism = workingRangeSize
            //        };
            //        Parallel.For(0, count, options, i =>
            //       {
            //           //Linux(serverip[i]);
            //           var task1 = Task.Run(() => Linux(serverip[i]));

            //       });

            //        Thread.Sleep(sleep);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("리눅스 시작스레드 에러");
            //    Console.WriteLine(e.Message);
            //}
        }

        public async Task Secure(string serverip)
        {
            try
            {
                DBCON.Class1 DBCON = new DBCON.Class1();
                MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                string SQL = "";
                //while (true)
                //{
                SQL = "select DISTINCT a.serverip, os, serverid , log_time, trafficlimit , cpulimit, memorylimit, ifnull (Community, 'public') as community, a.vandor from service a , " +
                    "Log_Time_Config b, mail_info c  where a.flag = '1'  and category = N'네트워크/보안 장비' and status = 'Server Connect' " +
                    "AND a.serverip='" + serverip + "'";
                MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");
                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {
                    ///OS VERSION
                    try
                    {
                        SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                        Pdu pdu = new Pdu();
                        pdu.VbList.Add(".1.3.6.1.2.1.1.1.0");
                        Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                        if (result == null)
                        {
                            Console.WriteLine("-------------------------------- " + serverip + " Request failed.");
                            break;
                        }
                        else
                        {
                            foreach (KeyValuePair<Oid, AsnType> entry in result)
                            {

                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set os = @os where serverip = @serverip ";
                                cmd.Parameters.Add("@OS", MySqlDbType.VarChar, 100).Value = entry.Value.ToString().Replace(',', ' ');
                                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();

                                Console.WriteLine(entry.Value.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OS 에러");
                        Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                    }
                    ///hostname
                    try
                    {
                        SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                        Pdu pdu = new Pdu();
                        Pdu pdu1 = new Pdu();
                        pdu.VbList.Add(".1.3.6.1.2.1.1.5.0");

                        Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);


                        if (result == null)
                        {
                            Console.WriteLine("Request failed.");
                        }
                        else
                        {
                            string hostname = "";
                            foreach (KeyValuePair<Oid, AsnType> entry in result)
                            {
                                hostname = entry.Value.ToString();
                                Console.WriteLine("이름 : " + entry.Value.ToString());
                            }

                            if (hostname.Contains(" ") == true)
                            {
                                ///hexcode 한글로
                                hostname = hostname.Replace(" ", "");
                                byte[] raw = new byte[hostname.Length / 2];
                                for (int i = 0; i < raw.Length; i++)
                                {
                                    raw[i] = Convert.ToByte(hostname.Substring(i * 2, 2), 16);

                                }
                                hostname = Encoding.UTF8.GetString(raw);
                                if (hostname.ToString().Contains("?") == true)
                                {
                                    hostname = Encoding.Default.GetString(raw);
                                }
                                Console.WriteLine(hostname);
                            }



                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = CON;
                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandText = "update service set computer_name = @computer_name where serverip = @serverip ";
                            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                            cmd.Parameters.Add("@computer_name", MySqlDbType.VarChar, 100).Value = hostname.ToString();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cmd = null;
                            CON.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        if (CON.State != ConnectionState.Open)
                        {
                            CON.Open();
                        }
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = CON;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update service set computer_name = @computer_name where serverip = @serverip ";
                        cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                        cmd.Parameters.Add("@computer_name", MySqlDbType.VarChar, 100).Value = "No Access";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cmd = null;
                        CON.Close();
                    }
                    ///UP TIME
                    try
                    {
                        SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                        Pdu pdu = new Pdu();
                        pdu.VbList.Add(".1.3.6.1.2.1.1.3.0");
                        Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                        if (result == null)
                        {
                            Console.WriteLine("Request failed.");
                        }
                        else
                        {
                            foreach (KeyValuePair<Oid, AsnType> entry in result)
                            {
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set uptime = @uptime where serverip = @serverip ";
                                cmd.Parameters.Add("@uptime", MySqlDbType.VarChar, 100).Value = entry.Value.ToString();
                                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();

                                Console.WriteLine(entry.Value.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                    }

                    //엑스게이트 밑에 있음
                    ///Cisco
                    if (row["os"].ToString().Contains("Cisco") == true)
                    {
                        //포트 맥주소
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();

                            //유선 사용하는 맥주소
                            Dictionary<Oid, AsnType> result10 = snmp.Walk(SnmpVersion.Ver2, ".1.3.6.1.2.1.4.22.1.2");
                            //맥주소 찾기
                            Dictionary<Oid, AsnType> result11 = snmp.Walk(SnmpVersion.Ver2, ".1.3.6.1.2.1.17.4.3.1.1");
                            //각 result1찾은 주소의 포트번호들
                            Dictionary<Oid, AsnType> result12 = snmp.Walk(SnmpVersion.Ver2, ".1.3.6.1.2.1.17.4.3.1.2");
                            //각 포트이름
                            Dictionary<Oid, AsnType> result13 = snmp.Walk(SnmpVersion.Ver2, ".1.3.6.1.2.1.31.1.1.1.1");
                            if (result10 == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                List<string> list_portname = new List<string>();
                                foreach (var entry3 in result13)
                                {
                                    //Console.WriteLine(entry3.Value.ToString());
                                    list_portname.Add(entry3.Value.ToString());
                                }

                                foreach (KeyValuePair<Oid, AsnType> entry in result10)
                                {
                                    //Console.WriteLine(i+"번 : " + entry.Key.ToString() + " , " + entry.Value.ToString());
                                    //list.Add(entry.Value.ToString());
                                    //Console.WriteLine(entry.Value.ToString());

                                    foreach (KeyValuePair<Oid, AsnType> entry1 in result11)
                                    {
                                        //Console.WriteLine(entry.Key.ToString());
                                        //list2.Add(entry1.Value.ToString());
                                        if (entry.Value.ToString() == entry1.Value.ToString())
                                        {
                                            //Console.WriteLine(entry1.Key.ToString());
                                            foreach (KeyValuePair<Oid, AsnType> entry2 in result12)
                                            {
                                                //Console.WriteLine(entry1.Key.ToString().Substring(entry1.Key.ToString().LastIndexOf(".") + 1));
                                                if ((entry1.Key.ToString().Substring(entry1.Key.ToString().LastIndexOf(@".") + 1) == (entry2.Key.ToString().Substring(entry2.Key.ToString().LastIndexOf(@".") + 1))))
                                                {
                                                    //Console.WriteLine(entry2.Value.ToString()+ "번 포트 " + entry.Value.ToString());
                                                    //Console.WriteLine(list[Convert.ToInt32(entry2.Value.ToString())]);

                                                    MySqlCommand cmd = new MySqlCommand();
                                                    cmd.Connection = CON;
                                                    if (CON.State != ConnectionState.Open)
                                                    {
                                                        CON.Open();
                                                    }
                                                    cmd.CommandType = System.Data.CommandType.Text;
                                                    cmd.CommandText = "update secure_port_traffic set mac = @mac where portname = @portname and serverip = @serverip ";
                                                    cmd.Parameters.Add("@mac", MySqlDbType.VarChar, 100).Value = entry.Value.ToString();
                                                    cmd.Parameters.Add("@portname", MySqlDbType.VarChar, 100).Value = list_portname[Convert.ToInt32(entry2.Value.ToString())];
                                                    cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                                    cmd.ExecuteNonQuery();
                                                    cmd.Dispose();
                                                    cmd = null;
                                                    CON.Close();
                                                }

                                            }
                                        }


                                    }

                                }

                            }
                        }
                        catch (Exception E)
                        {
                            Console.WriteLine("시스코 에러" + E.Message);
                        }
                        double percen = 0;
                        //MEMORY
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            Pdu pdu1 = new Pdu();
                            pdu.VbList.Add("1.3.6.1.4.1.9.9.48.1.1.1.5.1");//총크기
                            pdu1.VbList.Add("1.3.6.1.4.1.9.9.48.1.1.1.6.1");//프리양
                            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                            Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);

                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                //사용
                                string total = "";
                                //프리
                                string free = "";
                                foreach (KeyValuePair<Oid, AsnType> entry in result)
                                {
                                    total = entry.Value.ToString();
                                    //Console.WriteLine(entry.Value.ToString());
                                }
                                foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
                                {
                                    free = entry1.Value.ToString();
                                    //Console.WriteLine(entry1.Value.ToString());
                                }

                                double used_memory = Convert.ToDouble(total) - Convert.ToDouble(free);
                                percen = (used_memory / Convert.ToDouble(total)) * 100;
                                Console.WriteLine(percen.ToString("#.#") + "%");

                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set memory = @memory where serverip = @serverip ";
                                cmd.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = percen.ToString("#.#");
                                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        }



                        ///CPU
                        double cpu = 0;
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            pdu.VbList.Add("1.3.6.1.4.1.9.2.1.57.0");
                            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                foreach (KeyValuePair<Oid, AsnType> entry in result)
                                {
                                    Console.WriteLine("CPU : " + entry.Value.ToString() + "%");
                                    cpu = Convert.ToDouble(entry.Value.ToString());
                                    if (CON.State != ConnectionState.Open)
                                    {
                                        CON.Open();
                                    }
                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.Connection = CON;
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.CommandText = "update service set cpu = @cpu where serverip = @serverip ";
                                    cmd.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = entry.Value.ToString();
                                    cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;
                                    CON.Close();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        }

                        double cpua = Convert.ToDouble(cpu);
                        try
                        {
                            ///메일
                            if (Convert.ToInt32(row["cpulimit"]) < cpua && Convert.ToInt32(row["cpulimit"]) > 0)
                            {
                                Cpu_Mail mail = new Cpu_Mail();
                                mail.cpu_sendmail(row["serverip"].ToString(), cpu.ToString());
                            }
                            if (Convert.ToInt32(row["memorylimit"]) < percen && Convert.ToInt32(row["memorylimit"]) > 0)
                            {
                                Memory_Mail mail = new Memory_Mail();
                                mail.memory_sendmail(row["serverip"].ToString(), percen.ToString());
                            }
                        }
                        catch
                        {
                            Console.WriteLine("cisco 메일에러");
                        }
                        try
                        {
                            //포트별 트래픽
                            //포트
                            //포트별 트래픽
                            //포트
                            string mib_uptime = "sysuptime"; // 업타임
                            string mib_interface = "ifHCInOctets"; // 포트트래픽
                            string mib_outterface = "ifHCOutOctets"; // 포트트래픽
                            string mib_portname = "ifdescr"; // 포트이름
                            string mib_portstatus = "ifoperstatus"; //포트 상태
                            string[] inresult = { };
                            string[] outresult = { };

                            //포트이름
                            var process_port = new Process()
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    FileName = "cmd.exe",
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifdescr",
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
                                string portName = inresult.Last();
                                // Null0과 null 필터링
                                if (!string.IsNullOrWhiteSpace(portName) && !portName.Contains("Null0"))
                                {
                                    list_port.Add(portName);
                                }
                            }

                            //포트사용여부
                            var process_live = new Process()
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    FileName = "cmd.exe",
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifoperstatus",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCInOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCOutOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCInOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCOutOctets",
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
                                Console.WriteLine(list_port[i] + "토탈 : " + (total_1[i] - total[i]) + " 죽살 : " + list_live[i]);
                            }


                            // 데이터베이스 연결을 비동기적으로 열기
                            if (CON.State != ConnectionState.Open)
                            {
                                await CON.OpenAsync();
                            }

                            // 비동기 Parallel.ForEach를 사용하여 동시에 실행
                            await Task.WhenAll(list_port.Select(async (port, i) =>
                            {
                                // MySqlCommand 객체 생성
                                using (MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    // 파라미터 설정
                                    cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                    cmd.Parameters.AddWithValue("@portname1", port);
                                    cmd.Parameters.AddWithValue("@portlive1", list_live[i]);

                                    // 합계 계산 예외 처리
                                    try
                                    {
                                        cmd.Parameters.AddWithValue("@sum1", (total_1[i] - total[i]));
                                    }
                                    catch (Exception E)
                                    {
                                        cmd.Parameters.AddWithValue("@sum1", DBNull.Value);
                                        Console.WriteLine("sum 에러: " + E.Message);
                                    }

                                    cmd.Parameters.AddWithValue("@cpu1", cpu);
                                    cmd.Parameters.AddWithValue("@memory1", percen);
                                    cmd.Parameters.AddWithValue("@hd1", DBNull.Value);

                                    // 비동기 데이터베이스 호출
                                    await cmd.ExecuteNonQueryAsync();

                                    // 조건을 만족하는 경우 트래픽 알림 전송
                                    if (Convert.ToInt64(row["trafficlimit"]) < (total_1[i] - total[i]) && Convert.ToInt64(row["trafficlimit"]) > 0)
                                    {
                                        double nowtraffic = (total_1[i] - total[i]);
                                        Traffic_Mail mail = new Traffic_Mail();
                                        mail.Traffic_sendmail(row["serverip"].ToString(), null, nowtraffic.ToString("N1"));
                                        Console.WriteLine("메일 알림 트래픽: " + nowtraffic.ToString("N1"));
                                    }
                                }
                            }));

                            await CON.CloseAsync(); // 연결을 비동기적으로 닫기


                            //if (CON.State != ConnectionState.Open)
                            //{
                            //    CON.Open();
                            //}
                            //for (int i = 0; i < list2.Count; i++)
                            //{
                            //    MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                            //    cmd.CommandType = CommandType.StoredProcedure;
                            //    cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                            //    cmd.Parameters.AddWithValue("@portname1", list_port[i]);
                            //    cmd.Parameters.AddWithValue("@portlive1", list_live[i]);
                            //    try
                            //    {
                            //        cmd.Parameters.AddWithValue("@sum1", (total_1[i] - total[i]));
                            //    }
                            //    catch (Exception E)
                            //    {
                            //        cmd.Parameters.AddWithValue("@sum1", "null");
                            //        Console.WriteLine("sum에러" + E.Message);
                            //    }
                            //    cmd.Parameters.AddWithValue("@cpu1", cpu);
                            //    cmd.Parameters.AddWithValue("@memory1", percen);
                            //    cmd.Parameters.AddWithValue("@hd1", "null");
                            //    cmd.ExecuteNonQuery();
                            //    cmd.Dispose();
                            //    cmd = null;

                            //    if (Convert.ToInt64(row["trafficlimit"]) < (total_1[i] - total[i]) && Convert.ToInt64(row["trafficlimit"]) > 0)
                            //    {
                            //        double nowtraffic = (total_1[i] - total[i]);
                            //        Traffic_Mail mail = new Traffic_Mail();
                            //        mail.Traffic_sendmail(row["serverip"].ToString(), null, nowtraffic.ToString("N1"));
                            //        Console.WriteLine("메일 알림 트래픽 " + nowtraffic.ToString());
                            //    }
                            //}

                            //CON.Close();
                        }
                        catch
                        {
                            Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                        }

                    }

                    ///안랩
                    if (row["os"].ToString().Contains("ANOS") == true)
                    {
                        double percen = 0;
                        //MEMORY
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            Pdu pdu1 = new Pdu();
                            pdu.VbList.Add("1.3.6.1.4.1.2021.4.6.0");
                            pdu1.VbList.Add("1.3.6.1.4.1.2021.4.5.0");

                            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                            Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);

                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                //사용
                                string used = "";
                                //프리
                                string free = "";
                                foreach (KeyValuePair<Oid, AsnType> entry in result)
                                {
                                    used = entry.Value.ToString();
                                    //Console.WriteLine(entry.Value.ToString());
                                }
                                foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
                                {
                                    free = entry1.Value.ToString();
                                    //Console.WriteLine(entry1.Value.ToString());
                                }

                                double total = Convert.ToDouble(used) + Convert.ToDouble(free);
                                percen = (Convert.ToDouble(used) / total) * 100;
                                Console.WriteLine(percen.ToString("#.#") + "%");

                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set memory = @memory where serverip = @serverip ";
                                cmd.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = percen.ToString("#.#");
                                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        }



                        ///CPU
                        double cpu = 0;
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            pdu.VbList.Add("1.3.6.1.4.1.2021.11.9.0");
                            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                foreach (KeyValuePair<Oid, AsnType> entry in result)
                                {
                                    Console.WriteLine("CPU : " + entry.Value.ToString() + "%");
                                    cpu = Convert.ToDouble(entry.Value.ToString());
                                    if (CON.State != ConnectionState.Open)
                                    {
                                        CON.Open();
                                    }
                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.Connection = CON;
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.CommandText = "update service set cpu = @cpu where serverip = @serverip ";
                                    cmd.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = entry.Value.ToString();
                                    cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;
                                    CON.Close();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        }

                        double cpua = Convert.ToDouble(cpu);
                        try
                        {
                            ///메일
                            if (Convert.ToInt32(row["cpulimit"]) < cpua && Convert.ToInt32(row["cpulimit"]) > 0)
                            {
                                Cpu_Mail mail = new Cpu_Mail();
                                mail.cpu_sendmail(row["serverip"].ToString(), cpu.ToString());
                            }
                            if (Convert.ToInt32(row["memorylimit"]) < percen && Convert.ToInt32(row["memorylimit"]) > 0)
                            {
                                Memory_Mail mail = new Memory_Mail();
                                mail.memory_sendmail(row["serverip"].ToString(), percen.ToString());
                            }
                        }
                        catch
                        {
                            Console.WriteLine("메일에러");
                        }
                        try
                        {
                            //포트별 트래픽
                            //포트
                            string mib_uptime = "sysuptime"; // 업타임
                            string mib_interface = "ifHCInOctets"; // 포트트래픽
                            string mib_outterface = "ifHCOutOctets"; // 포트트래픽
                            string mib_portname = "ifdescr"; // 포트이름
                            string mib_portstatus = "ifoperstatus"; //포트 상태
                            string[] inresult = { };
                            string[] outresult = { };

                            //포트이름
                            var process_port = new Process()
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    FileName = "cmd.exe",
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifdescr",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifoperstatus",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCInOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCOutOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCInOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCOutOctets",
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
                                Console.WriteLine(list_port[i] + "토탈 : " + (total_1[i] - total[i]) + " 죽살 : " + list_live[i]);
                            }



                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            for (int i = 0; i < list2.Count; i++)
                            {
                                MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                cmd.Parameters.AddWithValue("@portname1", list_port[i]);
                                cmd.Parameters.AddWithValue("@portlive1", list_live[i]);
                                try
                                {
                                    cmd.Parameters.AddWithValue("@sum1", (total_1[i] - total[i]));
                                }
                                catch (Exception E)
                                {
                                    cmd.Parameters.AddWithValue("@sum1", "null");
                                    Console.WriteLine("sum에러" + E.Message);
                                }
                                cmd.Parameters.AddWithValue("@cpu1", cpu);
                                cmd.Parameters.AddWithValue("@memory1", percen);
                                cmd.Parameters.AddWithValue("@hd1", "null");
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;

                                if (Convert.ToInt64(row["trafficlimit"]) < (total_1[i] - total[i]) && Convert.ToInt64(row["trafficlimit"]) > 0)
                                {
                                    double nowtraffic = (total_1[i] - total[i]);
                                    Traffic_Mail mail = new Traffic_Mail();
                                    mail.Traffic_sendmail(row["serverip"].ToString(), null, nowtraffic.ToString("N1"));
                                    Console.WriteLine("메일 알림 트래픽 " + nowtraffic.ToString());

                                }
                            }

                            CON.Close();
                        }
                        catch
                        {
                            Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                        }
                    }

                    //CECUI
                    if (row["os"].ToString().Contains("Linux SECUI") == true || row["vandor"].ToString() == "SECUI")
                    {
                        double percen = 0;
                        //MEMORY
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            Pdu pdu1 = new Pdu();

                            Dictionary<Oid, AsnType> result = snmp.Walk(SnmpVersion.Ver2, ".1.3.6.1.4.1.9560.1.10.4.3.2.4"); // 사용량
                            //ictionary<Oid, AsnType> result1 = snmp.Walk(SnmpVersion.Ver2, ".1.3.6.1.4.1.9560.1.10.4.3.2.4"); // 사용량

                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                //사용
                                string used = "";
                                //프리
                                string free = "";
                                foreach (KeyValuePair<Oid, AsnType> entry in result)
                                {
                                    used = entry.Value.ToString();
                                    //Console.WriteLine(entry.Value.ToString());
                                }
                                //foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
                                //{
                                //    free = entry1.Value.ToString();
                                //    //Console.WriteLine(entry1.Value.ToString());
                                //}

                                //double total = Convert.ToDouble(used) + Convert.ToDouble(free);
                                //percen = Convert.ToDouble(used) / Convert.ToDouble(free);
                                //percen = 100 - percen;
                                percen = Convert.ToDouble(used);
                                Console.WriteLine("메모리 : " + percen.ToString("#.#") + "%");

                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set memory = @memory where serverip = @serverip ";
                                cmd.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = percen.ToString("#.#");
                                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                        }

                        ///CPU
                        double cpu = 0;
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            Dictionary<Oid, AsnType> result = snmp.Walk(SnmpVersion.Ver2, ".1.3.6.1.4.1.9560.1.10.4.3.1.1.6");
                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {

                                int count = 0;
                                foreach (KeyValuePair<Oid, AsnType> entry in result)
                                {
                                    //Console.WriteLine("CPU : " + entry.Value.ToString() + "%");
                                    cpu += Convert.ToDouble(entry.Value.ToString());
                                    count++;
                                }
                                cpu = cpu / count;
                                Console.WriteLine("CPU : " + cpu.ToString("#.#"));
                                MySqlCommand cmd = new MySqlCommand();
                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                cmd.Connection = CON;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set cpu = @cpu where serverip = @serverip ";
                                cmd.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = cpu.ToString("#.#");
                                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                        }
                        double cpua = Convert.ToDouble(cpu);
                        try
                        {
                            ///메일
                            if (Convert.ToInt32(row["cpulimit"]) < cpua && Convert.ToInt32(row["cpulimit"]) > 0)
                            {
                                Cpu_Mail mail = new Cpu_Mail();
                                mail.cpu_sendmail(row["serverip"].ToString(), cpu.ToString());
                            }
                            if (Convert.ToInt32(row["memorylimit"]) < percen && Convert.ToInt32(row["memorylimit"]) > 0)
                            {
                                Memory_Mail mail = new Memory_Mail();
                                mail.memory_sendmail(row["serverip"].ToString(), percen.ToString());
                            }
                        }
                        catch
                        {
                            Console.WriteLine("메일에러");
                        }
                        try
                        {
                            //포트별 트래픽
                            //포트
                            //포트별 트래픽
                            //포트
                            string mib_uptime = "sysuptime"; // 업타임
                            string mib_interface = "ifHCInOctets"; // 포트트래픽
                            string mib_outterface = "ifHCOutOctets"; // 포트트래픽
                            string mib_portname = "ifdescr"; // 포트이름
                            string mib_portstatus = "ifoperstatus"; //포트 상태
                            string[] inresult = { };
                            string[] outresult = { };

                            //포트이름
                            var process_port = new Process()
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    FileName = "cmd.exe",
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifdescr",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifoperstatus",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCInOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCOutOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCInOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCOutOctets",
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
                                Console.WriteLine(list_port[i] + "토탈 : " + (total_1[i] - total[i]) + " 죽살 : " + list_live[i]);
                            }

                            // 데이터베이스 연결을 비동기적으로 열기
                            if (CON.State != ConnectionState.Open)
                            {
                                await CON.OpenAsync();
                            }

                            // 비동기 Parallel.ForEach를 사용하여 동시에 실행
                            await Task.WhenAll(list_port.Select(async (port, i) =>
                            {
                                // MySqlCommand 객체 생성
                                using (MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    // 파라미터 설정
                                    cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                    cmd.Parameters.AddWithValue("@portname1", port);
                                    cmd.Parameters.AddWithValue("@portlive1", list_live[i]);

                                    // 합계 계산 예외 처리
                                    try
                                    {
                                        cmd.Parameters.AddWithValue("@sum1", (total_1[i] - total[i]));
                                    }
                                    catch (Exception E)
                                    {
                                        cmd.Parameters.AddWithValue("@sum1", DBNull.Value);
                                        Console.WriteLine("sum 에러: " + E.Message);
                                    }

                                    cmd.Parameters.AddWithValue("@cpu1", cpu);
                                    cmd.Parameters.AddWithValue("@memory1", percen);
                                    cmd.Parameters.AddWithValue("@hd1", DBNull.Value);

                                    // 비동기 데이터베이스 호출
                                    await cmd.ExecuteNonQueryAsync();

                                    // 조건을 만족하는 경우 트래픽 알림 전송
                                    if (Convert.ToInt64(row["trafficlimit"]) < (total_1[i] - total[i]) && Convert.ToInt64(row["trafficlimit"]) > 0)
                                    {
                                        double nowtraffic = (total_1[i] - total[i]);
                                        Traffic_Mail mail = new Traffic_Mail();
                                        mail.Traffic_sendmail(row["serverip"].ToString(), null, nowtraffic.ToString("N1"));
                                        Console.WriteLine("메일 알림 트래픽: " + nowtraffic.ToString("N1"));
                                    }
                                }
                            }));

                            await CON.CloseAsync(); // 연결을 비동기적으로 닫기


                        }
                        catch
                        {
                            Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                        }
                    }

                    ///엑스게이트
                    if (row["os"].ToString().Contains("AIOS") == true || row["vandor"].ToString() == "AXGATE")
                    {
                        double cpu = 0;
                        ///CPU
                        try
                        {
                            Console.WriteLine("엑스게이트 시작");
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            pdu.VbList.Add("1.3.6.1.4.1.37288.1.1.3.1.1.0");
                            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                foreach (KeyValuePair<Oid, AsnType> entry in result)
                                {
                                    Console.WriteLine("엑스게이트 CPU : " + entry.Value.ToString() + "%");
                                    cpu = Convert.ToDouble(entry.Value.ToString());
                                    if (CON.State != ConnectionState.Open)
                                    {
                                        CON.Open();
                                    }
                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.Connection = CON;
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.CommandText = "update service set cpu = @cpu where serverip = @serverip ";
                                    cmd.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = entry.Value.ToString();
                                    cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;
                                    CON.Close();
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        }
                        double percen = 0;
                        //MEMORY
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            Pdu pdu1 = new Pdu();
                            pdu.VbList.Add(".1.3.6.1.2.1.25.2.3.1.5.1");//토탈
                            pdu1.VbList.Add(".1.3.6.1.2.1.25.2.3.1.6.1");//프리
                            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                            Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);

                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                //사용
                                double total = 0;
                                //프리
                                double used = 0;
                                foreach (KeyValuePair<Oid, AsnType> entry in result)
                                {
                                    total = Convert.ToDouble(entry.Value.ToString());
                                    //Console.WriteLine(entry.Value.ToString());
                                }
                                foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
                                {
                                    used = Convert.ToDouble(entry1.Value.ToString());
                                    //Console.WriteLine(entry1.Value.ToString());
                                }


                                percen = (used / total) * 100;
                                Console.WriteLine("엑스게이트 메모리 : " + percen.ToString("#.#") + "%");

                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set memory = @memory where serverip = @serverip ";
                                cmd.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = percen.ToString("#.#");
                                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        }
                        double cpua = Convert.ToDouble(cpu);
                        try
                        {
                            ///메일
                            if (Convert.ToInt32(row["cpulimit"]) < cpua && Convert.ToInt32(row["cpulimit"]) > 0)
                            {
                                Cpu_Mail mail = new Cpu_Mail();
                                mail.cpu_sendmail(row["serverip"].ToString(), cpu.ToString());
                            }
                            if (Convert.ToInt32(row["memorylimit"]) < percen && Convert.ToInt32(row["memorylimit"]) > 0)
                            {

                                Memory_Mail mail = new Memory_Mail();
                                mail.memory_sendmail(row["serverip"].ToString(), percen.ToString());
                            }
                        }
                        catch
                        {
                            Console.WriteLine("메일에러");
                        }

                        try
                        {
                            //포트별 트래픽
                            //포트
                            //포트별 트래픽
                            //포트
                            string mib_uptime = "sysuptime"; // 업타임
                            string mib_interface = "ifHCInOctets"; // 포트트래픽
                            string mib_outterface = "ifHCOutOctets"; // 포트트래픽
                            string mib_portname = "ifdescr"; // 포트이름
                            string mib_portstatus = "ifoperstatus"; //포트 상태
                            string[] inresult = { };
                            string[] outresult = { };

                            //포트이름
                            var process_port = new Process()
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    FileName = "cmd.exe",
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifdescr",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifoperstatus",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCInOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCOutOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCInOctets",
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
                                    Arguments = "/c snmpwalk -v 2c -c " + row["Community"].ToString() + " " + row["serverip"].ToString() + " " + "ifHCOutOctets",
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
                                Console.WriteLine(list_port[i] + "토탈 : " + (total_1[i] - total[i]) + " 죽살 : " + list_live[i]);
                            }

                            // 데이터베이스 연결을 비동기적으로 열기
                            if (CON.State != ConnectionState.Open)
                            {
                                await CON.OpenAsync();
                            }

                            // 비동기 Parallel.ForEach를 사용하여 동시에 실행
                            await Task.WhenAll(list_port.Select(async (port, i) =>
                            {
                                // MySqlCommand 객체 생성
                                using (MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    // 파라미터 설정
                                    cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                    cmd.Parameters.AddWithValue("@portname1", port);
                                    cmd.Parameters.AddWithValue("@portlive1", list_live[i]);

                                    // 합계 계산 예외 처리
                                    try
                                    {
                                        cmd.Parameters.AddWithValue("@sum1", (total_1[i] - total[i]));
                                    }
                                    catch (Exception E)
                                    {
                                        cmd.Parameters.AddWithValue("@sum1", DBNull.Value);
                                        Console.WriteLine("sum 에러: " + E.Message);
                                    }

                                    cmd.Parameters.AddWithValue("@cpu1", cpu);
                                    cmd.Parameters.AddWithValue("@memory1", percen);
                                    cmd.Parameters.AddWithValue("@hd1", DBNull.Value);

                                    // 비동기 데이터베이스 호출
                                    await cmd.ExecuteNonQueryAsync();

                                    // 조건을 만족하는 경우 트래픽 알림 전송
                                    if (Convert.ToInt64(row["trafficlimit"]) < (total_1[i] - total[i]) && Convert.ToInt64(row["trafficlimit"]) > 0)
                                    {
                                        double nowtraffic = (total_1[i] - total[i]);
                                        Traffic_Mail mail = new Traffic_Mail();
                                        mail.Traffic_sendmail(row["serverip"].ToString(), null, nowtraffic.ToString("N1"));
                                        Console.WriteLine("메일 알림 트래픽: " + nowtraffic.ToString("N1"));
                                    }
                                }
                            }));

                            await CON.CloseAsync(); // 연결을 비동기적으로 닫기

                            //if (CON.State != ConnectionState.Open)
                            //{
                            //    CON.Open();
                            //}
                            //for (int i = 0; i < list2.Count; i++)
                            //{
                            //    MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                            //    cmd.CommandType = CommandType.StoredProcedure;
                            //    cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                            //    cmd.Parameters.AddWithValue("@portname1", list_port[i]);
                            //    cmd.Parameters.AddWithValue("@portlive1", list_live[i]);
                            //    try
                            //    {
                            //        cmd.Parameters.AddWithValue("@sum1", (total_1[i] - total[i]));
                            //    }
                            //    catch (Exception E)
                            //    {
                            //        cmd.Parameters.AddWithValue("@sum1", "null");
                            //        Console.WriteLine("sum에러" + E.Message);
                            //    }
                            //    cmd.Parameters.AddWithValue("@cpu1", cpu);
                            //    cmd.Parameters.AddWithValue("@memory1", percen);
                            //    cmd.Parameters.AddWithValue("@hd1", "null");
                            //    cmd.ExecuteNonQuery();
                            //    cmd.Dispose();
                            //    cmd = null;

                            //    if (Convert.ToInt64(row["trafficlimit"]) < (total_1[i] - total[i]) && Convert.ToInt64(row["trafficlimit"]) > 0)
                            //    {
                            //        double nowtraffic = (total_1[i] - total[i]);
                            //        Traffic_Mail mail = new Traffic_Mail();
                            //        mail.Traffic_sendmail(row["serverip"].ToString(), null, nowtraffic.ToString("N1"));
                            //        Console.WriteLine("메일 알림 트래픽 " + nowtraffic.ToString());

                            //    }
                            //}

                            //CON.Close();
                        }
                        catch
                        {
                            Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                        }
                    }

                }

                //Thread.Sleep(sleep);
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }


        static MySqlConnection GetDatabaseConnection(string connectionString)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        static Dictionary<Oid, AsnType> ExecuteSnmpRequest(string serverIp, string community, string oid)
        {
            var snmp = new SimpleSnmp(serverIp, community);
            Pdu pdu = new Pdu();
            pdu.VbList.Add(oid);
            return snmp.Get(SnmpVersion.Ver2, pdu);
        }


        static void UpdateDatabase(MySqlConnection con, string serverIp, string column, string value)
        {
            MySqlCommand cmd = null;
            try
            {
                cmd = new MySqlCommand($"UPDATE service SET {column} = @value WHERE serverip = @serverip", con);
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@serverip", serverIp);
                cmd.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
        }
        // 트래픽 데이터를 system_log_traffic 테이블에 INSERT하는 함수
        static void InsertIntoTrafficLog(MySqlConnection con, string serverIp, string totalTraffic)
        {
            string query = "INSERT INTO system_log_traffic (serverip,  traffic, time) " +
                           "VALUES (@serverip,  @Traffic, NOW())";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@serverip", serverIp);
            cmd.Parameters.AddWithValue("@Traffic", totalTraffic);

            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }

            cmd.ExecuteNonQuery();
        }
        static void InsertSystemStats(MySqlConnection con, string serverIp, string cpu, string memory)
        {
            string query = "INSERT INTO system_log_cpu_memory (serverip,  cpu, memory, time) " +
                           "VALUES (@serverip,  @cpu, @memory , NOW())";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@serverip", serverIp);
            cmd.Parameters.AddWithValue("@cpu", cpu);
            cmd.Parameters.AddWithValue("@memory", memory);


            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }

            cmd.ExecuteNonQuery();
        }

        static string DecodeHostname(string hostname)
        {
            hostname = hostname.Replace(" ", "");
            byte[] raw = new byte[hostname.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hostname.Substring(i * 2, 2), 16);
            }
            string decoded = Encoding.UTF8.GetString(raw);
            return decoded.Contains("?") ? Encoding.Default.GetString(raw) : decoded;
        }


        static void ProcessSnmpData(MySqlConnection con, DataRow row, string serverIp, string community)
        {
            try
            {
                // OS 정보 가져오기
                var osResult = ExecuteSnmpRequest(serverIp, community, ".1.3.6.1.2.1.1.1.0");
                if (osResult != null && osResult.TryGetValue(new Oid(".1.3.6.1.2.1.1.1.0"), out AsnType osData))
                {
                    UpdateDatabase(con, serverIp, "os", osData.ToString().Replace(',', ' '));
                    Console.WriteLine($"OS: {osData}");
                }

                // 호스트 이름 가져오기
                var hostnameResult = ExecuteSnmpRequest(serverIp, community, ".1.3.6.1.2.1.1.5.0");
                if (hostnameResult != null && hostnameResult.TryGetValue(new Oid(".1.3.6.1.2.1.1.5.0"), out AsnType hostnameData))
                {
                    string hostname = hostnameData.ToString();
                    hostname = hostname.Contains(" ") ? DecodeHostname(hostname) : hostname;
                    UpdateDatabase(con, serverIp, "computer_name", hostname);
                    Console.WriteLine($"Computer_name: {hostname}");
                }

                // Uptime 정보 가져오기
                var uptimeResult = ExecuteSnmpRequest(serverIp, community, ".1.3.6.1.2.1.1.3.0");
                if (uptimeResult != null && uptimeResult.TryGetValue(new Oid(".1.3.6.1.2.1.1.3.0"), out AsnType uptimeData))
                {
                    UpdateDatabase(con, serverIp, "uptime", uptimeData.ToString());
                    Console.WriteLine($"Uptime: {uptimeData}");
                }

                // CPU 정보 가져오기
                var cpuResult = ExecuteSnmpRequest(serverIp, community, ".1.3.6.1.4.1.2021.11.10.0");
                if (cpuResult != null && cpuResult.TryGetValue(new Oid(".1.3.6.1.4.1.2021.11.10.0"), out AsnType cpuData))
                {
                    // CPU 데이터 가져옴
                    string cpuValue = cpuData.ToString();
                    Console.WriteLine($"CPU: {cpuValue}");

                    // 메모리 정보 가져오기
                    var totalMemoryResult = ExecuteSnmpRequest(serverIp, community, ".1.3.6.1.4.1.2021.4.5.0");
                    var usedMemoryResult = ExecuteSnmpRequest(serverIp, community, ".1.3.6.1.4.1.2021.4.6.0");

                    if (totalMemoryResult != null && totalMemoryResult.TryGetValue(new Oid(".1.3.6.1.4.1.2021.4.5.0"), out AsnType totalMemoryData) &&
                        usedMemoryResult != null && usedMemoryResult.TryGetValue(new Oid(".1.3.6.1.4.1.2021.4.6.0"), out AsnType usedMemoryData))
                    {
                        // 메모리 사용률 계산
                        long totalMemory = Convert.ToInt64(totalMemoryData.ToString());
                        long usedMemory = Convert.ToInt64(usedMemoryData.ToString());

                        double memoryUsagePercentage = 0;
                        if (totalMemory > 0)
                        {
                            memoryUsagePercentage = (double)usedMemory / totalMemory * 100;
                        }
                        Console.WriteLine($"Memory Usage for {serverIp}: {memoryUsagePercentage:F1}%");


                        UpdateDatabase(con, serverIp, "cpu", cpuData.ToString());
                        UpdateDatabase(con, serverIp, "memory", memoryUsagePercentage.ToString("F1"));

                        // 데이터베이스에 CPU와 메모리 사용률을 동시에 INSERT INTO
                        InsertSystemStats(con, serverIp, cpuValue, memoryUsagePercentage.ToString("F1"));
                    }
                }


                //트래픽 계산


                // OID 설정 (수신/전송 바이트)
                string oidReceive = "1.3.6.1.2.1.2.2.1.10.2";  // eth0 인터페이스 수신 바이트
                string oidTransmit = "1.3.6.1.2.1.2.2.1.16.2";  // eth0 인터페이스 전송 바이트

                // 첫 번째 SNMP 요청
                var resultReceive1 = ExecuteSnmpRequest(serverIp, community, oidReceive);
                var resultTransmit1 = ExecuteSnmpRequest(serverIp, community, oidTransmit);

                if (resultReceive1 != null && resultTransmit1 != null)
                {
                    // Oid 객체로 변환 후 값 추출
                    Oid oidReceiveObject = new Oid(oidReceive);
                    Oid oidTransmitObject = new Oid(oidTransmit);

                    long firstReceive = Convert.ToInt64(resultReceive1[oidReceiveObject].ToString());
                    long firstTransmit = Convert.ToInt64(resultTransmit1[oidTransmitObject].ToString());

                    Console.WriteLine($"First Request - Receive: {firstReceive} bytes, Transmit: {firstTransmit} bytes");

                    // 1초 대기
                    Thread.Sleep(2000);

                    // 두 번째 SNMP 요청 (1초 후)
                    var resultReceive2 = ExecuteSnmpRequest(serverIp, community, oidReceive);
                    var resultTransmit2 = ExecuteSnmpRequest(serverIp, community, oidTransmit);

                    if (resultReceive2 != null && resultTransmit2 != null)
                    {
                        long secondReceive = Convert.ToInt64(resultReceive2[oidReceiveObject].ToString());
                        long secondTransmit = Convert.ToInt64(resultTransmit2[oidTransmitObject].ToString());

                        Console.WriteLine($"Second Request - Receive: {secondReceive} bytes, Transmit: {secondTransmit} bytes");

                        // 차이 계산 (1초 동안의 트래픽 변화)
                        long receiveDifference = secondReceive - firstReceive;
                        long transmitDifference = secondTransmit - firstTransmit;
                        long totalTraffic = receiveDifference + transmitDifference;

                        Console.WriteLine($"Traffic difference in 1 second: Receive: {receiveDifference} bytes, Transmit: {transmitDifference} bytes");
                        Console.WriteLine($"Total Traffic in 1 second: {totalTraffic} bytes");
                    }
                    else
                    {
                        Console.WriteLine("Error: SNMP second request failed.");
                    }


                    // 트래픽을 DB에 업데이트
                    //InsertIntoTrafficLog(con, serverIp, trafficInMB.ToString("F1"));
                    //Console.WriteLine($"Total Traffic for {serverIp}: {trafficInMB:F1} MB");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing SNMP data for {serverIp}: {ex.Message}");
                UpdateDatabase(con, serverIp, "computer_name", "No Access");
            }
        }


        public async Task Linux(string serverip)
        {

            DBCON.Class1 DBCON = new DBCON.Class1();
            MySqlConnection con = new MySqlConnection(DBCON.DBCON);
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();  // 연결을 엽니다
                }
                string query = "SELECT DISTINCT a.serverip, ifnull(Community, 'public') as community FROM service a " +
                               "WHERE a.flag = '1' and category = N'서버 장비' AND status = 'Server Connect' " +
                               "AND a.serverip ='" + serverip + "'";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "ServiceData");

                foreach (DataRow row in dataSet.Tables["ServiceData"].Rows)
                {
                    Console.WriteLine("리눅스 들어옴");
                    string serverIp = row["serverip"].ToString();
                    string community = row["community"].ToString();
                    ProcessSnmpData(con, row, serverIp, community);
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
            finally
            {
                // MySqlConnection 리소스 해제
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
    }
}
