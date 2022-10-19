using MySql.Data.MySqlClient;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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


        int sleep = 20000;
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
                    Console.WriteLine("만료기간 " + DBCON.date);

                    if (Convert.ToInt32(time) > Convert.ToInt32(DBCON.date))
                    {
                        Process.Start("C:/NMS(maria)/alert.exe");
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
            try
            {
                while (true)
                {
                    DBCON.Class1 DBCON = new DBCON.Class1();
                    MySqlConnection CON = new MySqlConnection(DBCON.DBCON);

                    ///
                    ///특정날짜 넘어가면 아웃
                    ///
                    string SQL2 = "select " +
                        "" +
                        " date_format(now(),'%Y%m%d') as time";
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
                        //break;
                    }


                    int count = 0;


                    string[] serverip = { };
                    string tempip = "";
                    MySqlDataAdapter ADT1 = new MySqlDataAdapter("Server_list_check", CON);
                    ADT1.SelectCommand.CommandType = CommandType.StoredProcedure;
                    ADT1.SelectCommand.Parameters.AddWithValue("@where1", "서버 장비");
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
                       //Linux(serverip[i]);
                       var task1 = Task.Run(() => Linux(serverip[i]));

                   });
                    //for (int i = 0; i < count; i++)
                    //{
                    //    thread[i] = new Thread(delegate ()
                    //    {
                    //        Linux(serverip[i]);
                    //    });
                    //    thread[i].Start();
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
                Console.WriteLine("리눅스 시작스레드 에러");
                Console.WriteLine(e.Message);
            }
        }

        public void Secure(string serverip)
        {

            try
            {
                DBCON.Class1 DBCON = new DBCON.Class1();
                MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                string SQL = "";
                //while (true)
                //{
                SQL = "select DISTINCT a.serverip, os, serverid , log_time, trafficlimit , cpulimit, memorylimit, ifnull (Community, 'public') as community, d.Model from service a , " +
                    "Log_Time_Config b, mail_info c , server_oid_list d where a.flag = '1'  and category = N'네트워크/보안 장비' and status = 'Server Connect' " +
                    "AND a.serverip='" + serverip + "' AND a.serverip = d.serverip";
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

                    //Axgate 는 aos
                    /*   if (row["os"].ToString().Contains("aos") == true)
                       {
                           double percen = 0;
                           //MEMORY
                           try
                           {
                               SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                               Pdu pdu = new Pdu();
                               Pdu pdu1 = new Pdu();
                               pdu.VbList.Add(".1.3.6.1.2.1.25.2.3.1.5.1");
                               pdu1.VbList.Add(".1.3.6.1.2.1.25.2.3.1.6.1");
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

                                   CON.Open();
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



                           double hd = 0;
                           //HD
                           try
                           {
                               SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                               Pdu pdu = new Pdu();
                               Pdu pdu1 = new Pdu();
                               pdu.VbList.Add(".1.3.6.1.2.1.25.2.3.1.5.3");
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
                                   hd = (Convert.ToDouble(used) / Convert.ToDouble(total)) * 100;
                                   Console.WriteLine(percen.ToString("#.#") + "%");

                                   CON.Open();
                                   MySqlCommand cmd = new MySqlCommand();
                                   cmd.Connection = CON;
                                   cmd.CommandType = System.Data.CommandType.Text;
                                   cmd.CommandText = "update server_hd set hd = @hd where serverip = @serverip ";
                                   cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                   cmd.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = "SDB : " + hd.ToString("#.#") + "%";
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

                           double cpu = 0;
                           ///CPU
                           try
                           {
                               SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                               Pdu pdu = new Pdu();
                               pdu.VbList.Add("1.3.6.1.4.1.37288.1.1.3.5.1.0");
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
                                       CON.Open();
                                       MySqlCommand cmd = new MySqlCommand();
                                       cmd.Connection = CON;
                                       cmd.CommandType = System.Data.CommandType.Text;
                                       cmd.CommandText = "update service set cpu = @cpu where serverip = @serverip ";
                                       cmd.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = cpu;
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
                       Console.WriteLine("엑스게이트 메일에러");
                   }
                   //포트별 트래픽
                   //포트
                   try
                           {
                               string[] portname = { };
                               string temp = "";
                               SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port name'";
                               MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, CON);
                               DataSet DBSET1 = new DataSet();
                               ADT1.Fill(DBSET1, "BD");
                               foreach (DataRow row1 in DBSET1.Tables["BD"].Rows)
                               {
                                   SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                   Pdu pdu1 = new Pdu();
                                   //포트들 이름
                                   pdu1.VbList.Add(row1["oid"].ToString());
                                   Dictionary<Oid, AsnType> result1 = snmp1.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                   if (result1 == null)
                                   {
                                       Console.WriteLine("Request failed.");
                                   }
                                   else
                                   {

                                       foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                       {
                                           //Console.WriteLine("포트이름들 : " + entry.Value.ToString());
                                           temp += entry.Value.ToString() + ",";
                                       }

                                   }
                               }
                               portname = temp.Split(',');




                               string[] portlive = { };
                               temp = "";
                               SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port status'";
                               MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL, CON);
                               DataSet DBSET2 = new DataSet();
                               ADT2.Fill(DBSET2, "BD");
                               foreach (DataRow row1 in DBSET2.Tables["BD"].Rows)
                               {
                                   SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                   Pdu pdu1 = new Pdu();
                                   //살아있는지 1삼 2죽음
                                   pdu1.VbList.Add(row1["oid"].ToString());
                                   Dictionary<Oid, AsnType> result1 = snmp1.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                   if (result1 == null)
                                   {
                                       Console.WriteLine("Request failed.");
                                   }
                                   else
                                   {
                                       foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                       {
                                           //Console.WriteLine("살아있나 : " + entry.Value.ToString());
                                           temp += entry.Value.ToString() + ",";
                                       }

                                   }
                               }
                               portlive = temp.Split(',');

                               double total = 0;
                               double total1 = 0;
                               double traffic = 0;
                               double traffic1 = 0;

                               string[] sum = { };
                               string[] sum1 = { };
                               ///트래픽
                               SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-in'";
                               MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, CON);
                               DataSet DBSET3 = new DataSet();
                               ADT3.Fill(DBSET3, "BD");
                               temp = "";
                               foreach (DataRow row1 in DBSET3.Tables["BD"].Rows)
                               {

                                   ///트래픽
                                   SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                   Pdu pdu = new Pdu();
                                   pdu.VbList.Add(row1["oid"].ToString());
                                   Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                   if (result == null)
                                   {
                                       Console.WriteLine("Request failed.");
                                   }
                                   else
                                   {
                                       foreach (KeyValuePair<Oid, AsnType> entry in result)
                                       {
                                           //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                           try
                                           {
                                               //Console.WriteLine(Convert.ToDouble(entry.Value.ToString()));
                                               traffic = Convert.ToDouble(entry.Value.ToString());
                                           }
                                           catch (Exception e)
                                           {
                                                traffic = 0;
                                           }
                                       }
                                   }

                                   Thread.Sleep(1000);

                                   SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                   Pdu pdu1 = new Pdu();
                                   pdu1.VbList.Add(row1["oid"].ToString());
                                   Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                   if (result1 == null)
                                   {
                                       Console.WriteLine("Request failed.");
                                   }
                                   else
                                   {
                                       foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
                                       {
                                           //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                           try
                                           {
                                               //Console.WriteLine(Convert.ToDouble(entry1.Value.ToString()));
                                               traffic1 = Convert.ToDouble(entry1.Value.ToString());
                                           }
                                           catch (Exception e)
                                           {
                                               traffic1 = 0;
                                           }
                                       }

                                   }
                                   traffic1 = traffic1 - traffic;
                                   temp += traffic1.ToString() + ",";

                               }
                               sum = temp.Split(',');
                               Console.WriteLine("-------------------------");
                               SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-out'";
                               MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL, CON);
                               DataSet DBSET5 = new DataSet();
                               ADT5.Fill(DBSET5, "BD");
                               traffic = 0;
                               traffic1 = 0;
                               string temp2 = "";
                               foreach (DataRow row1 in DBSET5.Tables["BD"].Rows)
                               {

                                   ///트래픽
                                   SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                   Pdu pdu = new Pdu();
                                   pdu.VbList.Add(row1["oid"].ToString());
                                   Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                   if (result3 == null)
                                   {
                                       Console.WriteLine("Request failed.");
                                   }
                                   else
                                   {
                                       foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                       {
                                           //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                           try
                                           {

                                               traffic = Convert.ToDouble(entry.Value.ToString());
                                           }
                                           catch (Exception e)
                                           {
                                               //Console.WriteLine(e.ToString());
                                               traffic = 0;
                                           }

                                       }
                                   }
                                   Thread.Sleep(1000);

                                   SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                   Pdu pdu1 = new Pdu();
                                   pdu1.VbList.Add(row1["oid"].ToString());
                                   Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                   if (result1 == null)
                                   {
                                       Console.WriteLine("Request failed.");
                                   }
                                   else
                                   {
                                       foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                       {
                                           //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                           try
                                           {
                                               traffic1 = Convert.ToDouble(entry.Value.ToString());
                                           }
                                           catch (Exception e)
                                           {
                                               //Console.WriteLine(e.ToString());
                                               traffic = 0;
                                           }

                                       }

                                   }

                                   traffic1 = traffic1 - traffic;

                                   temp2 += traffic1.ToString() + ",";

                               }

                               sum1 = temp2.Split(',');
                               Console.WriteLine("-------------------------");
                               int portcount = 0;
                               SQL = "select count(*) as count from Server_oid_list where serverip ='" + serverip + "' and description = 'portname'";
                               MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, CON);
                               DataSet DBSET4 = new DataSet();
                               ADT4.Fill(DBSET4, "BD");
                               temp = "";
                               foreach (DataRow row1 in DBSET4.Tables["BD"].Rows)
                               {
                                   portcount = Convert.ToInt32(row1["count"].ToString());
                               }

                               CON.Open();
                               for (int j = 0; j < portcount; j++)
                               {
                                   string state = "";
                                   if (row["log_time"].ToString() == "1")//1시간
                                           {
                                               string SQL2 = "select date_format(time,'%I')  as time from Secure_Log" +
                                                   " where serverip = concat('" + row["serverip"].ToString() + "',' ','" + portname[j] + "') order by no desc limit 1 ";
                                               MySqlDataAdapter ADT6 = new MySqlDataAdapter(SQL2, CON);
                                               DataSet DBSET6 = new DataSet();
                                               ADT6.Fill(DBSET6, "BD2");

                                               foreach (DataRow row2 in DBSET6.Tables["BD2"].Rows)
                                               {
                                                   string time1 = DateTime.Now.ToString("HH");
                                                   string time2 = DateTime.Now.ToString(row2["time"].ToString());
                                                   time2 = Regex.Replace(time2, " ", ":");
                                                   //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                                                   if (time1 != time2)
                                                   {
                                                       //CON.Open();
                                                       MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                                       cmd.CommandType = CommandType.StoredProcedure;
                                                       cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                                       cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                                       cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                                       try
                                                       {
                                                           cmd.Parameters.AddWithValue("@sum1", sum[j]);
                                                       }
                                                       catch
                                                       {
                                                           cmd.Parameters.AddWithValue("@sum1", "null");
                                                       }

                                                       cmd.Parameters.AddWithValue("@cpu1", cpu);
                                                       cmd.Parameters.AddWithValue("@memory1", percen);
                                                       cmd.Parameters.AddWithValue("@hd1", hd.ToString("#.#"));
                                                       cmd.ExecuteNonQuery();
                                                       cmd.Dispose();
                                                       cmd = null;
                                                       //CON.Close();
                                                   }
                                                   state = "in";
                                               }
                                           }
                                   if (row["log_time"].ToString() == "2")//30분
                                   {
                                       string SQL2 = "select  date_format(DATE_ADD(time, INTERVAL 30 MINUTE), '%Y-%c-%d %H:%i') as time from Secure_Log " +
                                           "where serverip = concat('" + row["serverip"].ToString() + "',' ','" + portname[j] + "') order by no desc limit 1";
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
                                               //CON.Open();
                                               //MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                               //cmd.CommandType = CommandType.StoredProcedure;
                                               //cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                               //cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                               //cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                               //cmd.Parameters.AddWithValue("@sum1", sum[j]);
                                               //cmd.Parameters.AddWithValue("@cpu1", cpu);
                                               //cmd.Parameters.AddWithValue("@memory1", percen);
                                               //cmd.Parameters.AddWithValue("@hd1", hd.ToString("#.#"));
                                               //cmd.ExecuteNonQuery();
                                               //cmd.Dispose();
                                               //cmd = null;
                                               //CON.Close();
                                             }
                                           state = "in";
                                       }

                                       MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                       cmd.CommandType = CommandType.StoredProcedure;
                                       cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                       cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                       cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                       try
                                       {
                                           cmd.Parameters.AddWithValue("@sum1", sum[j]);
                                       }
                                       catch
                                       {
                                           cmd.Parameters.AddWithValue("@sum1", "null");
                                       }
                                       cmd.Parameters.AddWithValue("@cpu1", cpu);
                                       cmd.Parameters.AddWithValue("@memory1", percen);
                                       cmd.Parameters.AddWithValue("@hd1", hd.ToString("#.#"));
                                       cmd.ExecuteNonQuery();
                                       cmd.Dispose();
                                       cmd = null;

                                    }
                                   //Console.WriteLine("state:" + state);
                                   if (state == "")
                                   {
                                       //Console.WriteLine("들어와서:" + portname[j]);
                                       //CON.Open();
                                       MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                       cmd.CommandType = CommandType.StoredProcedure;
                                       cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                       cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                       cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                       try
                                       {
                                           cmd.Parameters.AddWithValue("@sum1", sum[j]);
                                       }
                                       catch
                                       {
                                           cmd.Parameters.AddWithValue("@sum1", "null");
                                       }
                                       cmd.Parameters.AddWithValue("@cpu1", cpu);
                                       cmd.Parameters.AddWithValue("@memory1", percen);
                                       cmd.Parameters.AddWithValue("@hd1", hd.ToString("#.#"));
                                       cmd.ExecuteNonQuery();
                                       cmd.Dispose();
                                       cmd = null;
                                       //CON.Close();
                                   }
                                   //CON.Open();
                                   //MySqlCommand cmd3 = new MySqlCommand();
                                   //cmd3.Connection = CON;
                                   //cmd3.CommandType = System.Data.CommandType.Text;
                                   //cmd3.CommandText = "insert into Temp_Secure_Port_Log_Traffic (serverip,traffic,time) values(@serverip,@traffic,now()) ";
                                   //cmd3.Parameters.Add("@traffic", MySqlDbType.VarChar, 100).Value = sum[j];
                                   //cmd3.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString() + " " + portname[j];
                                   //cmd3.ExecuteNonQuery();
                                   //cmd3.Dispose();
                                   //cmd3 = null;
                                   //CON.Close();
                                   Console.WriteLine(Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                               }
                                   CON.Close();
                               }

                           catch (Exception ex)
                           {
                               Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                           }

                       }
                       */
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
                                List<string> list = new List<string>();
                                foreach (var entry3 in result13)
                                {
                                    //Console.WriteLine(entry3.Value.ToString());
                                    list.Add(entry3.Value.ToString());
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
                                                    cmd.Parameters.Add("@portname", MySqlDbType.VarChar, 100).Value = list[Convert.ToInt32(entry2.Value.ToString())];
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
                            pdu.VbList.Add("1.3.6.1.4.1.9.9.48.1.1.1.5.1");//사용량
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

                        ////HD
                        //try
                        //{
                        //    SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), "public");
                        //    Pdu pdu = new Pdu();
                        //    Pdu pdu1 = new Pdu();
                        //    pdu.VbList.Add(".1.3.6.1.2.1.25.2.3.1.5.3");
                        //    pdu1.VbList.Add(".1.3.6.1.2.1.25.2.3.1.6.3");
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
                        //        double percen = (Convert.ToDouble(used) / Convert.ToDouble(total)) * 100;
                        //        Console.WriteLine(percen.ToString("#.#") + "%");


                        //        MySqlCommand cmd = new MySqlCommand();
                        //        cmd.Connection = CON;
                        //        cmd.CommandType = System.Data.CommandType.Text;
                        //        cmd.CommandText = "update server_hd set hd = @hd where serverip = @serverip ";
                        //        cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                        //        cmd.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = "SDB : " + percen.ToString("#.#") + "%";
                        //        cmd.ExecuteNonQuery();
                        //        cmd.Dispose();
                        //        cmd = null;

                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        //}

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
                        //포트별 트래픽
                        //포트
                        try
                        {
                            string[] portname = { };
                            string[] portlive = { };
                            string temp = "";
                            string temp2 = "";
                            ///포트이름 가져오기
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port name'";
                            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET1 = new DataSet();
                            ADT1.Fill(DBSET1, "BD");
                            foreach (DataRow row1 in DBSET1.Tables["BD"].Rows)
                            {
                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                //포트들 이름
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp1.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {

                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("포트이름들 : " + entry.Value.ToString());
                                        temp += entry.Value.ToString() + ",";
                                    }

                                }
                            }
                            portname = temp.Split(',');

                            ///포트사용여부
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port status'";
                            MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET2 = new DataSet();
                            ADT2.Fill(DBSET2, "BD");
                            string temp3 = "";
                            foreach (DataRow row1 in DBSET2.Tables["BD"].Rows)
                            {

                                //포트 사용 여부
                                SimpleSnmp snmp3 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu3 = new Pdu();
                                pdu3.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result3 = snmp3.Get(SnmpVersion.Ver2, pdu3); //.GetNext(pdu);
                                if (result3 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                    {
                                        //Console.WriteLine("살아있나 : " + entry.Value.ToString());
                                        temp3 += entry.Value.ToString() + ",";
                                    }

                                }
                            }
                            portlive = temp3.Split(',');



                            double total = 0;
                            double total1 = 0;
                            double traffic = 0;
                            double traffic1 = 0;

                            string[] sum = { };
                            string[] sum1 = { };
                            ///트래픽
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-in'";
                            MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET3 = new DataSet();
                            ADT3.Fill(DBSET3, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET3.Tables["BD"].Rows)
                            {

                                ///트래픽
                                SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu = new Pdu();
                                pdu.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                if (result == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic = Convert.ToDouble(entry.Value.ToString());

                                    }
                                }

                                Thread.Sleep(trafficsleep);

                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("traffic1 : " + entry.Value.ToString() + "bytes");
                                        traffic1 = Convert.ToDouble(entry.Value.ToString());
                                    }

                                }

                                traffic1 = traffic1 - traffic;
                                //로그확인용
                                //Console.WriteLine(traffic1.ToString());
                                temp += traffic1.ToString() + ",";
                            }
                            sum = temp.Split(',');
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-out'";
                            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET5 = new DataSet();
                            ADT5.Fill(DBSET5, "BD");
                            traffic = 0;
                            traffic1 = 0;
                            temp = "";
                            foreach (DataRow row1 in DBSET5.Tables["BD"].Rows)
                            {

                                ///트래픽
                                SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu = new Pdu();
                                pdu.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                if (result3 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic = Convert.ToDouble(entry.Value.ToString());
                                    }
                                }

                                Thread.Sleep(trafficsleep);

                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic1 = Convert.ToDouble(entry.Value.ToString());
                                    }

                                }

                                traffic1 = traffic1 - traffic;

                                temp2 += traffic1.ToString() + ",";

                            }


                            sum1 = temp2.Split(',');




                            int portcount = 0;
                            double totaltraffic = 0;
                            SQL = "select count(*) as count from Server_oid_list where serverip ='" + serverip + "' and description = 'Port Name'";
                            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET4 = new DataSet();
                            ADT4.Fill(DBSET4, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET4.Tables["BD"].Rows)
                            {
                                portcount = Convert.ToInt32(row1["count"].ToString());
                            }

                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            for (int j = 0; j < portcount; j++)
                            {
                                MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                try
                                {
                                    cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
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

                                try
                                {
                                    //로그확인용
                                    //Console.WriteLine(Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                }
                                catch (Exception E)
                                {
                                    Console.WriteLine("total에러" + E.Message);
                                }

                                double eachtraffic = Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]);
                                totaltraffic += eachtraffic;


                            }
                            CON.Close();

                            try
                            {
                                totaltraffic = totaltraffic;

                                //cisco 메일
                                if (Convert.ToInt64(row["trafficlimit"]) < totaltraffic && Convert.ToInt64(row["trafficlimit"]) > 0)
                                {
                                    double nowtraffic = (totaltraffic);
                                    Traffic_Mail mail = new Traffic_Mail();
                                    mail.Traffic_sendmail(row["serverip"].ToString(), null, nowtraffic.ToString("N1"));
                                    Console.WriteLine("메일 알림 트래픽 " + nowtraffic.ToString());

                                }
                                //Console.WriteLine("트래픽 메일 보내기 성공");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                            }




                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                        }
                    }

                    ///안랩
                    ///
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

                        ////HD
                        //try
                        //{
                        //    SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), "public");
                        //    Pdu pdu = new Pdu();
                        //    Pdu pdu1 = new Pdu();
                        //    pdu.VbList.Add(".1.3.6.1.2.1.25.2.3.1.5.3");
                        //    pdu1.VbList.Add(".1.3.6.1.2.1.25.2.3.1.6.3");
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
                        //        double percen = (Convert.ToDouble(used) / Convert.ToDouble(total)) * 100;
                        //        Console.WriteLine(percen.ToString("#.#") + "%");


                        //        MySqlCommand cmd = new MySqlCommand();
                        //        cmd.Connection = CON;
                        //        cmd.CommandType = System.Data.CommandType.Text;
                        //        cmd.CommandText = "update server_hd set hd = @hd where serverip = @serverip ";
                        //        cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                        //        cmd.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = "SDB : " + percen.ToString("#.#") + "%";
                        //        cmd.ExecuteNonQuery();
                        //        cmd.Dispose();
                        //        cmd = null;

                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        //}

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
                        //포트별 트래픽
                        //포트
                        try
                        {


                            string[] portname = { };
                            string[] portlive = { };
                            string temp = "";
                            string temp2 = "";
                            ///포트이름 가져오기
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port name'";
                            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET1 = new DataSet();
                            ADT1.Fill(DBSET1, "BD");
                            foreach (DataRow row1 in DBSET1.Tables["BD"].Rows)
                            {
                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                //포트들 이름
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp1.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {

                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("포트이름들 : " + entry.Value.ToString());
                                        temp += entry.Value.ToString() + ",";
                                    }

                                }
                            }
                            portname = temp.Split(',');

                            ///포트사용여부
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port status'";
                            MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET2 = new DataSet();
                            ADT2.Fill(DBSET2, "BD");
                            string temp3 = "";
                            foreach (DataRow row1 in DBSET2.Tables["BD"].Rows)
                            {

                                //포트 사용 여부
                                SimpleSnmp snmp3 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu3 = new Pdu();
                                pdu3.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result3 = snmp3.Get(SnmpVersion.Ver2, pdu3); //.GetNext(pdu);
                                if (result3 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                    {
                                        //Console.WriteLine("살아있나 : " + entry.Value.ToString());
                                        temp3 += entry.Value.ToString() + ",";
                                    }

                                }
                            }
                            portlive = temp3.Split(',');



                            double total = 0;
                            double total1 = 0;
                            double traffic = 0;
                            double traffic1 = 0;

                            string[] sum = { };
                            string[] sum1 = { };
                            ///트래픽
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-in'";
                            MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET3 = new DataSet();
                            ADT3.Fill(DBSET3, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET3.Tables["BD"].Rows)
                            {

                                ///트래픽
                                SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu = new Pdu();
                                pdu.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                if (result == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic = Convert.ToDouble(entry.Value.ToString());

                                    }
                                }

                                Thread.Sleep(trafficsleep);

                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic1 = Convert.ToDouble(entry.Value.ToString());
                                    }

                                }
                                traffic1 = traffic1 - traffic;
                                temp += traffic1.ToString() + ",";
                            }
                            sum = temp.Split(',');
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-out'";
                            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET5 = new DataSet();
                            ADT5.Fill(DBSET5, "BD");
                            traffic = 0;
                            traffic1 = 0;
                            temp = "";
                            foreach (DataRow row1 in DBSET5.Tables["BD"].Rows)
                            {

                                ///트래픽
                                SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu = new Pdu();
                                pdu.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                if (result3 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic = Convert.ToDouble(entry.Value.ToString());
                                    }
                                }

                                Thread.Sleep(trafficsleep);

                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic1 = Convert.ToDouble(entry.Value.ToString());
                                    }

                                }

                                traffic1 = traffic1 - traffic;

                                temp2 += traffic1.ToString() + ",";

                            }

                            sum1 = temp2.Split(',');

                            int portcount = 0;
                            SQL = "select count(*) as count from Server_oid_list where serverip ='" + serverip + "' and description = 'port name'";
                            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET4 = new DataSet();
                            ADT4.Fill(DBSET4, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET4.Tables["BD"].Rows)
                            {
                                portcount = Convert.ToInt32(row1["count"].ToString());
                            }

                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            for (int j = 0; j < portcount; j++)
                            {
                                //Console.WriteLine("포트이름==========================  : " + portname[j]);
                                //Console.WriteLine("포트사용==========================  : " + portlive[j]);
                                string state = "";
                                /* if (row["log_time"].ToString() == "1")//1시간
                                 {
                                     string SQL2 = "select  date_format(time,'%I')  as time from Secure_Log" +
                                         " where serverip = concat('" + row["serverip"].ToString() + "',' ','" + portname[j] + "') order by no desc limit 1";
                                     MySqlDataAdapter ADT6 = new MySqlDataAdapter(SQL2, CON);
                                     DataSet DBSET6 = new DataSet();
                                     ADT6.Fill(DBSET6, "BD2");

                                     foreach (DataRow row2 in DBSET6.Tables["BD2"].Rows)
                                     {
                                         string time1 = DateTime.Now.ToString("HH");
                                         string time2 = DateTime.Now.ToString(row2["time"].ToString());
                                         time2 = Regex.Replace(time2, " ", ":");
                                         //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                                         if (time1 != time2)
                                         {

                                             //CON.Open();
                                             //MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                             //cmd.CommandType = CommandType.StoredProcedure;
                                             //cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                             //cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                             //cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                             //cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                             //cmd.Parameters.AddWithValue("@cpu1", cpu);
                                             //cmd.Parameters.AddWithValue("@memory1", percen);
                                             //cmd.Parameters.AddWithValue("@hd1", "null");
                                             //cmd.ExecuteNonQuery();
                                             //cmd.Dispose();
                                             //cmd = null;
                                             //CON.Close();
                                         }
                                         state = "in";
                                     }


                                 }
                                 if (row["log_time"].ToString() == "2")//30분
                                 {
                                     string SQL2 = "select date_format(DATE_ADD(time, INTERVAL 30 MINUTE), '%Y-%c-%d %H:%i') as time from Secure_Log " +
                                         "where serverip = concat('" + row["serverip"].ToString() + "',' ','" + portname[j] + "') order by no desc limit 1";
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
                                             //CON.Open();
                                             //MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                             //cmd.CommandType = CommandType.StoredProcedure;
                                             //cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                             //cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                             //cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                             //cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                             //cmd.Parameters.AddWithValue("@cpu1", cpu);
                                             //cmd.Parameters.AddWithValue("@memory1", percen);
                                             //cmd.Parameters.AddWithValue("@hd1", "null");
                                             //cmd.ExecuteNonQuery();
                                             //cmd.Dispose();
                                             //cmd = null;
                                             //CON.Close();
                                         }
                                         state = "in";
                                     }

                                     MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                     cmd.CommandType = CommandType.StoredProcedure;
                                     cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                     cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                     cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                     try
                                     {
                                         cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                     }
                                     catch
                                     {
                                         cmd.Parameters.AddWithValue("@sum1", "null");
                                     }

                                     cmd.Parameters.AddWithValue("@cpu1", cpu);
                                     cmd.Parameters.AddWithValue("@memory1", percen);
                                     cmd.Parameters.AddWithValue("@hd1", "null");
                                     cmd.ExecuteNonQuery();
                                     cmd.Dispose();
                                     cmd = null;

                                 }*/
                                //if (state == "")
                                //{
                                // CON.Open();
                                MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                try
                                {
                                    cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                }
                                catch (Exception E)
                                {
                                    Console.WriteLine("sum1에러" + E.Message);
                                    cmd.Parameters.AddWithValue("@sum1", "null");
                                }
                                cmd.Parameters.AddWithValue("@cpu1", cpu);
                                cmd.Parameters.AddWithValue("@memory1", percen);
                                cmd.Parameters.AddWithValue("@hd1", "null");
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                //  CON.Close();
                                //}
                                // CON.Open();
                                //MySqlCommand cmd3 = new MySqlCommand();
                                //cmd3.Connection = CON;
                                //cmd3.CommandType = System.Data.CommandType.Text;
                                //cmd3.CommandText = "insert into Temp_Secure_Port_Log_Traffic (serverip,traffic,time) values(@serverip,@traffic,now()) ";
                                //cmd3.Parameters.Add("@traffic", MySqlDbType.VarChar, 100).Value = Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]);
                                //cmd3.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString() + " " + portname[j];
                                //cmd3.ExecuteNonQuery();
                                //cmd3.Dispose();
                                //cmd3 = null;
                                //CON.Close();
                                Console.WriteLine(Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                            }

                            CON.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                        }
                    }

                    ///어울림
                    if (row["os"].ToString().Contains("Linux mainvpn") == true)
                    {
                        double percen = 0;
                        //MEMORY
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            Pdu pdu1 = new Pdu();
                            pdu.VbList.Add("1.3.6.1.4.1.2021.4.6.0");//사용량
                            pdu1.VbList.Add("1.3.6.1.4.1.2021.4.5.0");//프리양
                                                                      //pdu.VbList.Add("1.3.6.1.4.1.2021.4.5.0");//사용량
                                                                      //pdu1.VbList.Add("1.3.6.1.4.1.2021.4.11.0");//프리양
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
                        //포트별 트래픽
                        //포트
                        try
                        {


                            string[] portname = { };
                            string[] portlive = { };
                            string temp = "";
                            string temp2 = "";
                            ///포트이름 가져오기
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port name'";
                            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET1 = new DataSet();
                            ADT1.Fill(DBSET1, "BD");
                            foreach (DataRow row1 in DBSET1.Tables["BD"].Rows)
                            {
                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                //포트들 이름
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp1.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {

                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("포트이름들 : " + entry.Value.ToString());
                                        temp += entry.Value.ToString() + ",";
                                    }

                                }
                            }
                            portname = temp.Split(',');

                            ///포트사용여부
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port status'";
                            MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET2 = new DataSet();
                            ADT2.Fill(DBSET2, "BD");
                            string temp3 = "";
                            foreach (DataRow row1 in DBSET2.Tables["BD"].Rows)
                            {

                                //포트 사용 여부
                                SimpleSnmp snmp3 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu3 = new Pdu();
                                pdu3.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result3 = snmp3.Get(SnmpVersion.Ver2, pdu3); //.GetNext(pdu);
                                if (result3 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                    {
                                        //Console.WriteLine("살아있나 : " + entry.Value.ToString());
                                        temp3 += entry.Value.ToString() + ",";
                                    }

                                }
                            }
                            portlive = temp3.Split(',');



                            double total = 0;
                            double total1 = 0;
                            double traffic = 0;
                            double traffic1 = 0;

                            string[] sum = { };
                            string[] sum1 = { };
                            ///트래픽
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-in'";
                            MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET3 = new DataSet();
                            ADT3.Fill(DBSET3, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET3.Tables["BD"].Rows)
                            {

                                ///트래픽
                                SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu = new Pdu();
                                pdu.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                if (result == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic = Convert.ToDouble(entry.Value.ToString());

                                    }
                                }

                                Thread.Sleep(trafficsleep);

                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic1 = Convert.ToDouble(entry.Value.ToString());
                                    }

                                }
                                traffic1 = traffic1 - traffic;
                                temp += traffic1.ToString() + ",";
                            }
                            sum = temp.Split(',');
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-out'";
                            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET5 = new DataSet();
                            ADT5.Fill(DBSET5, "BD");
                            traffic = 0;
                            traffic1 = 0;
                            temp = "";
                            foreach (DataRow row1 in DBSET5.Tables["BD"].Rows)
                            {

                                ///트래픽
                                SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu = new Pdu();
                                pdu.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                if (result3 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic = Convert.ToDouble(entry.Value.ToString());
                                    }
                                }

                                Thread.Sleep(trafficsleep);

                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic1 = Convert.ToDouble(entry.Value.ToString());
                                    }

                                }

                                traffic1 = traffic1 - traffic;

                                temp2 += traffic1.ToString() + ",";

                            }

                            sum1 = temp2.Split(',');

                            int portcount = 0;
                            SQL = "select count(*) as count from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-in'";
                            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET4 = new DataSet();
                            ADT4.Fill(DBSET4, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET4.Tables["BD"].Rows)
                            {
                                portcount = Convert.ToInt32(row1["count"].ToString());
                            }

                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            for (int j = 0; j < portcount; j++)
                            {
                                //Console.WriteLine("포트이름==========================  : " + portname[j]);
                                //Console.WriteLine("포트사용==========================  : " + portlive[j]);
                                string state = "";
                                /*if (row["log_time"].ToString() == "1")//1시간
                                {
                                    string SQL2 = "select date_format(time,'%I')  as time from Secure_Log" +
                                        " where serverip = concat('" + row["serverip"].ToString() + "',' ','" + portname[j] + "') order by no desc limit 1";
                                    MySqlDataAdapter ADT6 = new MySqlDataAdapter(SQL2, CON);
                                    DataSet DBSET6 = new DataSet();
                                    ADT6.Fill(DBSET6, "BD2");

                                    foreach (DataRow row2 in DBSET6.Tables["BD2"].Rows)
                                    {
                                        string time1 = DateTime.Now.ToString("HH");
                                        string time2 = DateTime.Now.ToString(row2["time"].ToString());
                                        time2 = Regex.Replace(time2, " ", ":");
                                        //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                                        if (time1 != time2)
                                        {

                                            //CON.Open();
                                            MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                            cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                            cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                            try
                                            {
                                                cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                            }
                                            catch
                                            {
                                                cmd.Parameters.AddWithValue("@sum1", "null");
                                            }
                                            cmd.Parameters.AddWithValue("@cpu1", cpu);
                                            cmd.Parameters.AddWithValue("@memory1", percen);
                                            cmd.Parameters.AddWithValue("@hd1", "null");
                                            cmd.ExecuteNonQuery();
                                            cmd.Dispose();
                                            cmd = null;
                                            //CON.Close();
                                        }
                                        state = "in";
                                    }
                                }
                                if (row["log_time"].ToString() == "2")//30분
                                {
                                    string SQL2 = "select date_format(DATE_ADD(time, INTERVAL 30 MINUTE), '%Y-%c-%d %H:%i') as time from Secure_Log " +
                                        "where serverip = concat('" + row["serverip"].ToString() + "',' ','" + portname[j] + "') order by no desc limit 1";
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
                                            //CON.Open();
                                            //MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                            //cmd.CommandType = CommandType.StoredProcedure;
                                            //cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                            //cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                            //cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                            //cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                            //cmd.Parameters.AddWithValue("@cpu1", cpu);
                                            //cmd.Parameters.AddWithValue("@memory1", percen);
                                            //cmd.Parameters.AddWithValue("@hd1", "null");
                                            //cmd.ExecuteNonQuery();
                                            //cmd.Dispose();
                                            //cmd = null;
                                            //CON.Close();
                                        }
                                        state = "in";
                                    }

                                    MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                    cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                    cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                    try
                                    {
                                        cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                    }
                                    catch
                                    {
                                        cmd.Parameters.AddWithValue("@sum1", "null");
                                    }
                                    cmd.Parameters.AddWithValue("@cpu1", cpu);
                                    cmd.Parameters.AddWithValue("@memory1", percen);
                                    cmd.Parameters.AddWithValue("@hd1", "null");
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;

                                }
                               */
                                //if (state == "")
                                //{
                                //CON.Open();
                                MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                try
                                {
                                    cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                }
                                catch
                                {
                                    cmd.Parameters.AddWithValue("@sum1", "null");
                                }
                                cmd.Parameters.AddWithValue("@cpu1", cpu);
                                cmd.Parameters.AddWithValue("@memory1", percen);
                                cmd.Parameters.AddWithValue("@hd1", "null");
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                //CON.Close();
                                //}
                                //CON.Open();
                                //MySqlCommand cmd3 = new MySqlCommand();
                                //cmd3.Connection = CON;
                                //cmd3.CommandType = System.Data.CommandType.Text;
                                //cmd3.CommandText = "insert into Temp_Secure_Port_Log_Traffic (serverip,traffic,time) values(@serverip,@traffic,now()) ";
                                //cmd3.Parameters.Add("@traffic", MySqlDbType.VarChar, 100).Value = Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]);
                                //cmd3.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString() + " " + portname[j];
                                //cmd3.ExecuteNonQuery();
                                //cmd3.Dispose();
                                //cmd3 = null;
                                //CON.Close();
                                Console.WriteLine(Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                            }

                            CON.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        }
                    }

                    //CECUI
                    if (row["os"].ToString().Contains("Linux SECUI") == true || row["model"].ToString().Contains("SECUI"))
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

                        //포트별 트래픽
                        //포트
                        try
                        {
                            string[] portname = { };
                            string[] portlive = { };
                            string temp = "";
                            string temp2 = "";
                            ///포트이름 가져오기
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port name'";
                            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET1 = new DataSet();
                            ADT1.Fill(DBSET1, "BD");
                            foreach (DataRow row1 in DBSET1.Tables["BD"].Rows)
                            {
                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                //포트들 이름
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp1.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {

                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("포트이름들 : " + entry.Value.ToString());
                                        temp += entry.Value.ToString() + ",";
                                    }

                                }
                            }
                            portname = temp.Split(',');

                            ///포트사용여부
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port status'";
                            MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET2 = new DataSet();
                            ADT2.Fill(DBSET2, "BD");
                            string temp3 = "";
                            foreach (DataRow row1 in DBSET2.Tables["BD"].Rows)
                            {

                                //포트 사용 여부
                                SimpleSnmp snmp3 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu3 = new Pdu();
                                pdu3.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result3 = snmp3.Get(SnmpVersion.Ver2, pdu3); //.GetNext(pdu);
                                if (result3 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                    {
                                        //Console.WriteLine("살아있나 : " + entry.Value.ToString());
                                        temp3 += entry.Value.ToString() + ",";
                                    }

                                }
                            }
                            portlive = temp3.Split(',');



                            double total = 0;
                            double total1 = 0;
                            double traffic = 0;
                            double traffic1 = 0;

                            string[] sum = { };
                            string[] sum1 = { };
                            ///트래픽
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-in'";
                            MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET3 = new DataSet();
                            ADT3.Fill(DBSET3, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET3.Tables["BD"].Rows)
                            {

                                ///트래픽
                                SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu = new Pdu();
                                pdu.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                if (result == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic = Convert.ToDouble(entry.Value.ToString());

                                    }
                                }

                                Thread.Sleep(trafficsleep);

                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("traffic1 : " + entry.Value.ToString() + "bytes");
                                        traffic1 = Convert.ToDouble(entry.Value.ToString());
                                    }

                                }

                                traffic1 = traffic1 - traffic;
                                //로그확인용
                                //Console.WriteLine(traffic1.ToString());
                                temp += traffic1.ToString() + ",";
                            }
                            sum = temp.Split(',');
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-out'";
                            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET5 = new DataSet();
                            ADT5.Fill(DBSET5, "BD");
                            traffic = 0;
                            traffic1 = 0;
                            temp = "";
                            foreach (DataRow row1 in DBSET5.Tables["BD"].Rows)
                            {

                                ///트래픽
                                SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu = new Pdu();
                                pdu.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                if (result3 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic = Convert.ToDouble(entry.Value.ToString());
                                    }
                                }

                                Thread.Sleep(trafficsleep);

                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic1 = Convert.ToDouble(entry.Value.ToString());
                                    }

                                }

                                traffic1 = traffic1 - traffic;

                                temp2 += traffic1.ToString() + ",";

                            }


                            sum1 = temp2.Split(',');




                            int portcount = 0;
                            double totaltraffic = 0;
                            SQL = "select count(*) as count from Server_oid_list where serverip ='" + serverip + "' and description = 'Port Name'";
                            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET4 = new DataSet();
                            ADT4.Fill(DBSET4, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET4.Tables["BD"].Rows)
                            {
                                portcount = Convert.ToInt32(row1["count"].ToString());
                            }

                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            for (int j = 0; j < portcount; j++)
                            {
                                MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                try
                                {
                                    cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
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

                                try
                                {
                                    //로그확인용
                                    //Console.WriteLine(Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                }
                                catch (Exception E)
                                {
                                    Console.WriteLine("total에러" + E.Message);
                                }

                                double eachtraffic = Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]);
                                totaltraffic += eachtraffic;


                            }
                            CON.Close();

                            try
                            {
                                totaltraffic = totaltraffic;

                                //SECUI 메일
                                if (Convert.ToInt64(row["trafficlimit"]) < totaltraffic && Convert.ToInt64(row["trafficlimit"]) > 0)
                                {
                                    double nowtraffic = (totaltraffic);
                                    Traffic_Mail mail = new Traffic_Mail();
                                    mail.Traffic_sendmail(row["serverip"].ToString(), null, nowtraffic.ToString("N1"));
                                    Console.WriteLine("메일 알림 트래픽 " + nowtraffic.ToString());

                                }
                                //Console.WriteLine("트래픽 메일 보내기 성공");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                            }




                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine("★★★★★★★SNMP 에러 :  " + row["serverip"].ToString() + " 확인!!");
                        }
                    }

                    ///엑스게이트
                    if (row["os"].ToString().Contains("AIOS") == true)
                    {
                        double cpu = 0;
                        ///CPU
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
                        double percen = 0;
                        //MEMORY
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            Pdu pdu1 = new Pdu();
                            pdu.VbList.Add(".1.3.6.1.4.1.2021.4.5.0");//토탈
                            pdu1.VbList.Add(".1.3.6.1.4.1.2021.4.6.0");//프리
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

                        //HD
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            Pdu pdu1 = new Pdu();
                            pdu.VbList.Add(".1.3.6.1.2.1.25.2.3.1.5.3");//토탈
                            pdu1.VbList.Add(".1.3.6.1.2.1.25.2.3.1.6.3");//유즈드
                            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                            Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);

                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                double total = 0;
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
                                percen = (Convert.ToDouble(used) / Convert.ToDouble(total)) * 100;
                                Console.WriteLine(percen.ToString("#.#") + "%");

                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                MySqlCommand cmd5 = new MySqlCommand();
                                cmd5.Connection = CON;
                                cmd5.CommandType = System.Data.CommandType.Text;
                                cmd5.CommandText = "update server_hd set hd = @hd where serverip = @serverip ";
                                cmd5.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd5.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = "C : " + percen.ToString("#.#") + "%";
                                cmd5.ExecuteNonQuery();
                                cmd5.Dispose();
                                cmd5 = null;
                                CON.Close();

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        }





                        //포트별 트래픽
                        //포트
                        try
                        {
                            string[] portname = { };
                            string[] portlive = { };
                            string temp = "";
                            string temp2 = "";
                            ///포트이름 가져오기
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port name'";
                            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET1 = new DataSet();
                            ADT1.Fill(DBSET1, "BD");
                            foreach (DataRow row1 in DBSET1.Tables["BD"].Rows)
                            {
                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                //포트들 이름
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp1.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {

                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("포트이름들 : " + entry.Value.ToString());
                                        temp += entry.Value.ToString() + ",";
                                    }

                                }
                            }
                            portname = temp.Split(',');

                            ///포트사용여부
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'port status'";
                            MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET2 = new DataSet();
                            ADT2.Fill(DBSET2, "BD");
                            string temp3 = "";
                            foreach (DataRow row1 in DBSET2.Tables["BD"].Rows)
                            {

                                //포트 사용 여부
                                SimpleSnmp snmp3 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu3 = new Pdu();
                                pdu3.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result3 = snmp3.Get(SnmpVersion.Ver2, pdu3); //.GetNext(pdu);
                                if (result3 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                    {
                                        //Console.WriteLine("살아있나 : " + entry.Value.ToString());
                                        temp3 += entry.Value.ToString() + ",";
                                    }

                                }
                            }
                            portlive = temp3.Split(',');



                            double total = 0;
                            double total1 = 0;
                            double traffic = 0;
                            double traffic1 = 0;

                            string[] sum = { };
                            string[] sum1 = { };
                            ///트래픽
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-in'";
                            MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET3 = new DataSet();
                            ADT3.Fill(DBSET3, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET3.Tables["BD"].Rows)
                            {

                                ///트래픽
                                SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu = new Pdu();
                                pdu.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                if (result == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic = Convert.ToDouble(entry.Value.ToString());

                                    }
                                }

                                Thread.Sleep(trafficsleep);

                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic1 = Convert.ToDouble(entry.Value.ToString());
                                    }

                                }
                                traffic1 = traffic1 - traffic;
                                temp += traffic1.ToString() + ",";
                            }
                            sum = temp.Split(',');
                            SQL = "select distinct serverip,  oid from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-out'";
                            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET5 = new DataSet();
                            ADT5.Fill(DBSET5, "BD");
                            traffic = 0;
                            traffic1 = 0;
                            temp = "";
                            foreach (DataRow row1 in DBSET5.Tables["BD"].Rows)
                            {

                                ///트래픽
                                SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu = new Pdu();
                                pdu.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                                if (result3 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result3)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic = Convert.ToDouble(entry.Value.ToString());
                                    }
                                }

                                Thread.Sleep(trafficsleep);

                                SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                                Pdu pdu1 = new Pdu();
                                pdu1.VbList.Add(row1["oid"].ToString());
                                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                                if (result1 == null)
                                {
                                    Console.WriteLine("Request failed.");
                                }
                                else
                                {
                                    foreach (KeyValuePair<Oid, AsnType> entry in result1)
                                    {
                                        //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                                        traffic1 = Convert.ToDouble(entry.Value.ToString());
                                    }

                                }

                                traffic1 = traffic1 - traffic;

                                temp2 += traffic1.ToString() + ",";

                            }

                            sum1 = temp2.Split(',');

                            int portcount = 0;
                            SQL = "select count(*) as count from Server_oid_list where serverip ='" + serverip + "' and description = 'port name'";
                            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET4 = new DataSet();
                            ADT4.Fill(DBSET4, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET4.Tables["BD"].Rows)
                            {
                                portcount = Convert.ToInt32(row1["count"].ToString());
                            }

                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            for (int j = 0; j < portcount; j++)
                            {
                                string state = "";
                                //Console.WriteLine("포트이름==========================  : " + portname[j]);
                                //Console.WriteLine("포트사용==========================  : " + portlive[j]);

                                /* if (row["log_time"].ToString() == "1")//1시간
                                 {
                                     string SQL2 = "select date_format(time,'%I')  as time from Secure_Log" +
                                         " where serverip = concat('" + row["serverip"].ToString() + "',' ','" + portname[j] + "') order by no desc limit 1";
                                     MySqlDataAdapter ADT6 = new MySqlDataAdapter(SQL2, CON);
                                     DataSet DBSET6 = new DataSet();
                                     ADT6.Fill(DBSET6, "BD2");

                                     foreach (DataRow row2 in DBSET6.Tables["BD2"].Rows)
                                     {
                                         string time1 = DateTime.Now.ToString("HH");
                                         string time2 = DateTime.Now.ToString(row2["time"].ToString());
                                         time2 = Regex.Replace(time2, " ", ":");
                                         //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                                         if (time1 != time2)
                                         {

                                             //CON.Open();
                                             MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                             cmd.CommandType = CommandType.StoredProcedure;
                                             cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                             cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                             cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                             try
                                             {
                                                 cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                             }
                                             catch
                                             {
                                                 cmd.Parameters.AddWithValue("@sum1", "null");
                                             }
                                             cmd.Parameters.AddWithValue("@cpu1", cpu);
                                             cmd.Parameters.AddWithValue("@memory1", percen);
                                             cmd.Parameters.AddWithValue("@hd1", "null");
                                             cmd.ExecuteNonQuery();
                                             cmd.Dispose();
                                             cmd = null;
                                             //CON.Close();
                                         }
                                         state = "in";
                                     }
                                 }
                                 if (row["log_time"].ToString() == "2")//30분
                                 {
                                     string SQL2 = "select date_format(DATE_ADD(time, INTERVAL 30 MINUTE), '%Y-%c-%d %H:%i') as time from Secure_Log " +
                                         "where serverip = concat('" + row["serverip"].ToString() + "',' ','" + portname[j] + "') order by no desc limit 1";
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
                                             //CON.Open();
                                             //MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                             //cmd.CommandType = CommandType.StoredProcedure;
                                             //cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                             //cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                             //cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                             //cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                             //cmd.Parameters.AddWithValue("@cpu1", cpu);
                                             //cmd.Parameters.AddWithValue("@memory1", percen);
                                             //cmd.Parameters.AddWithValue("@hd1", "null");
                                             //cmd.ExecuteNonQuery();
                                             //cmd.Dispose();
                                             //cmd = null;
                                             //CON.Close();
                                         }
                                         state = "in";
                                     }

                                     MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                     cmd.CommandType = CommandType.StoredProcedure;
                                     cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                     cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                     cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                     try
                                     {
                                         cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                     }
                                     catch
                                     {
                                         cmd.Parameters.AddWithValue("@sum1", "null");
                                     }
                                     cmd.Parameters.AddWithValue("@cpu1", cpu);
                                     cmd.Parameters.AddWithValue("@memory1", percen);
                                     cmd.Parameters.AddWithValue("@hd1", "null");
                                     cmd.ExecuteNonQuery();
                                     cmd.Dispose();
                                     cmd = null;


                                 }*/
                                //if (state == "")
                                //{
                                //CON.Open();
                                MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                try
                                {
                                    cmd.Parameters.AddWithValue("@sum1", Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                }
                                catch
                                {
                                    cmd.Parameters.AddWithValue("@sum1", "null");
                                }
                                cmd.Parameters.AddWithValue("@cpu1", cpu);
                                cmd.Parameters.AddWithValue("@memory1", percen);
                                cmd.Parameters.AddWithValue("@hd1", "null");
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                //CON.Close();
                                //}
                                //CON.Open();
                                //MySqlCommand cmd3 = new MySqlCommand();
                                //cmd3.Connection = CON;
                                //cmd3.CommandType = System.Data.CommandType.Text;
                                //cmd3.CommandText = "insert into Temp_Secure_Port_Log_Traffic (serverip,traffic,time) values(@serverip,@traffic,now()) ";
                                //cmd3.Parameters.Add("@traffic", MySqlDbType.VarChar, 100).Value = Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]);
                                //cmd3.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString() + " " + portname[j];
                                //cmd3.ExecuteNonQuery();
                                //cmd3.Dispose();
                                //cmd3 = null;
                                //CON.Close();
                                try
                                {
                                    Console.WriteLine(Convert.ToDouble(sum1[j]) + Convert.ToDouble(sum[j]));
                                }
                                catch (Exception E)
                                {
                                    Console.WriteLine("리눅" + E.Message);
                                }

                            }
                            CON.Close();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
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

        public void Linux(string serverip)
        {
            try
            {

                DBCON.Class1 DBCON = new DBCON.Class1();
                MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                string SQL = "";
                //while (true)
                //{
                SQL = "select distinct serverip, os, serverid, log_time , trafficlimit , cpulimit, memorylimit, ifnull (Community, 'public') as community from service a , log_time_config b, mail_info c where a.flag = '1' and category = N'서버 장비'  and status = 'Server Connect'" +
                    " and serverip = '" + serverip + "' ";
                MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");

                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {

                    Console.WriteLine("들어옴" + serverip);
                    //OS VERSION
                    try
                    {
                        Console.WriteLine(row["serverip"].ToString() + "Connecting...");
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
                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set os = @os where serverip = @serverip ";
                                cmd.Parameters.Add("@OS", MySqlDbType.VarChar, 100).Value = entry.Value.ToString();
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
                    //hostname
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
                                Console.WriteLine(entry.Value.ToString());
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

                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = CON;
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

                    if (row["os"].ToString().Contains("Linux") == true)
                    {

                        double cpu = 0;
                        ///CPU
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
                                    MySqlCommand cmd5 = new MySqlCommand();
                                    cmd5.Connection = CON;
                                    cmd5.CommandType = System.Data.CommandType.Text;
                                    cmd5.CommandText = "update service set cpu = @cpu where serverip = @serverip ";
                                    cmd5.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = entry.Value.ToString();
                                    cmd5.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                    cmd5.ExecuteNonQuery();
                                    cmd5.Dispose();
                                    cmd5 = null;
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
                            Pdu pdu2 = new Pdu();
                            Pdu pdu3 = new Pdu();

                            pdu.VbList.Add(".1.3.6.1.4.1.2021.4.5.0");//토탈
                            pdu1.VbList.Add(".1.3.6.1.4.1.2021.4.6.0");//유즈
                            pdu2.VbList.Add(".1.3.6.1.4.1.2021.4.14.0");//버퍼
                            pdu3.VbList.Add(".1.3.6.1.4.1.2021.4.15.0");//캐시
                            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                            Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                            Dictionary<Oid, AsnType> result2 = snmp.Get(SnmpVersion.Ver2, pdu2); //.GetNext(pdu);
                            Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu3); //.GetNext(pdu);

                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                double total = 0;
                                double use = 0;
                                double buffer = 0;
                                double cache = 0;
                                foreach (KeyValuePair<Oid, AsnType> entry in result)
                                {
                                    total = Convert.ToDouble(entry.Value.ToString());
                                    //Console.WriteLine(entry.Value.ToString());
                                }
                                foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
                                {
                                    use = Convert.ToDouble(entry1.Value.ToString());
                                    //Console.WriteLine(entry1.Value.ToString());
                                }
                                foreach (KeyValuePair<Oid, AsnType> entry1 in result2)
                                {
                                    buffer = Convert.ToDouble(entry1.Value.ToString());
                                    //Console.WriteLine(entry1.Value.ToString());
                                }
                                foreach (KeyValuePair<Oid, AsnType> entry1 in result3)
                                {
                                    cache = Convert.ToDouble(entry1.Value.ToString());
                                    //Console.WriteLine(entry1.Value.ToString());
                                }
                                //use = total - (use - (buffer + cache));
                                //percen = (Convert.ToDouble(use) / Convert.ToDouble(total)) * 100;
                                percen = total / (total - cache);
                                Console.WriteLine(percen.ToString("#.#") + "%");

                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                MySqlCommand cmd5 = new MySqlCommand();
                                cmd5.Connection = CON;
                                cmd5.CommandType = System.Data.CommandType.Text;
                                cmd5.CommandText = "update service set memory = @memory where serverip = @serverip ";
                                cmd5.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = percen.ToString("#.#");
                                cmd5.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd5.ExecuteNonQuery();
                                cmd5.Dispose();
                                cmd5 = null;
                                CON.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        }

                        //HD
                        try
                        {
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            Pdu pdu1 = new Pdu();
                            pdu.VbList.Add(".1.3.6.1.2.1.25.2.3.1.5.3");//토탈
                            pdu1.VbList.Add(".1.3.6.1.2.1.25.2.3.1.6.3");//유즈드
                            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                            Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);

                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {
                                double total = 0;
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
                                percen = (Convert.ToDouble(used) / Convert.ToDouble(total)) * 100;
                                Console.WriteLine(percen.ToString("#.#") + "%");

                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                MySqlCommand cmd5 = new MySqlCommand();
                                cmd5.Connection = CON;
                                cmd5.CommandType = System.Data.CommandType.Text;
                                cmd5.CommandText = "update server_hd set hd = @hd where serverip = @serverip ";
                                cmd5.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd5.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = "C : " + percen.ToString("#.#") + "%";
                                cmd5.ExecuteNonQuery();
                                cmd5.Dispose();
                                cmd5 = null;
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

                        ///로그
                        //string state = "";
                        //if (row["log_time"].ToString() == "1")//1시간
                        //{
                        //    string SQL2 = "select date_format(time,'%I')  as time from system_log_cpu_memory where serverip = '" + row["serverip"].ToString() + "' order by no desc limit 1";
                        //    MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL2, CON);
                        //    DataSet DBSET2 = new DataSet();
                        //    ADT2.Fill(DBSET2, "BD2");

                        //    foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                        //    {
                        //        string time1 = DateTime.Now.ToString("HH");
                        //        string time2 = DateTime.Now.ToString(row2["time"].ToString());
                        //        //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                        //        if (time1 != time2)
                        //        {
                        //            CON.Open();
                        //            MySqlCommand cmd3 = new MySqlCommand("Add_System_Log_Cpu_Memory", CON);
                        //            cmd3.CommandType = CommandType.StoredProcedure;
                        //            cmd3.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                        //            cmd3.ExecuteNonQuery();
                        //            CON.Close();

                        //            CON.Open();
                        //            MySqlCommand cmd4 = new MySqlCommand("Add_System_Log_Traffic", CON);
                        //            cmd4.CommandType = CommandType.StoredProcedure;
                        //            cmd4.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                        //            cmd4.ExecuteNonQuery();
                        //            CON.Close();

                        //        }
                        //        state = "IN";
                        //    }
                        //}

                        //if (row["log_time"].ToString() == "2")//30분
                        //{
                        //    string SQL2 = "select date_format(DATE_ADD(time, INTERVAL 30 MINUTE), '%Y-%c-%d %H:%i') as time from system_log_cpu_memory where serverip = '" + row["serverip"].ToString() + "' order by no desc limit 1";
                        //    MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL2, CON);
                        //    DataSet DBSET2 = new DataSet();
                        //    ADT2.Fill(DBSET2, "BD2");
                        //    foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                        //    {
                        //        string time1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                        //        string time2 = DateTime.Now.ToString(row2["time"].ToString());
                        //        //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                        //        //time2 = Regex.Replace(time2, " ", ":");
                        //        if (Convert.ToDateTime(time1) > Convert.ToDateTime(time2))
                        //        {
                        //            CON.Open();
                        //            MySqlCommand cmd3 = new MySqlCommand("Add_System_Log_Cpu_Memory", CON);
                        //            cmd3.CommandType = CommandType.StoredProcedure;
                        //            cmd3.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                        //            cmd3.ExecuteNonQuery();
                        //            CON.Close();

                        //            CON.Open();
                        //            MySqlCommand cmd4 = new MySqlCommand("Add_System_Log_Traffic", CON);
                        //            cmd4.CommandType = CommandType.StoredProcedure;
                        //            cmd4.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                        //            cmd4.ExecuteNonQuery();
                        //            CON.Close();
                        //        }
                        //        state = "IN";
                        //    }
                        //}

                        //if (state == "")
                        //{
                        //    CON.Open();
                        //    MySqlCommand cmd3 = new MySqlCommand();
                        //    cmd3.Connection = CON;
                        //    cmd3.CommandType = System.Data.CommandType.Text;
                        //    cmd3.CommandText = "insert into system_log_cpu_memory (serverip,cpu,memory) values(@serverip,@cpu,@memory) ";
                        //    cmd3.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = cpu.ToString();
                        //    cmd3.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = percen.ToString("#.#");
                        //    cmd3.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                        //    cmd3.ExecuteNonQuery();
                        //    cmd3.Dispose();
                        //    cmd3 = null;
                        //    CON.Close();
                        //}

                        //CON.Open();
                        //MySqlCommand cmd = new MySqlCommand();
                        //cmd.Connection = CON;
                        //cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.CommandText = "update service set cpu = @cpu ,  memory = @memory where serverip = @serverip ";
                        //cmd.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = cpu.ToString();
                        //cmd.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = percen.ToString("#.#");
                        //cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                        //cmd.ExecuteNonQuery();
                        //cmd.Dispose();
                        //cmd = null;
                        //CON.Close();

                        //CON.Open();
                        //MySqlCommand cmd1 = new MySqlCommand();
                        //cmd1.Connection = CON;
                        //cmd1.CommandType = System.Data.CommandType.Text;
                        //cmd1.CommandText = "insert into temp_system_log_cpu_memory (serverip,cpu,memory,hd) values(@serverip,@cpu,@memory,@hd) ";
                        //cmd1.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = cpu.ToString();
                        //cmd1.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = percen.ToString("#.#");
                        //cmd1.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = "C: " + percen.ToString("#.#") + "%";
                        //cmd1.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                        //cmd1.ExecuteNonQuery();
                        //cmd1.Dispose();
                        //cmd1 = null;
                        //CON.Close();

                        //트래픽
                        try
                        {

                            //////    double total = 0;
                            //////    double total1 = 0;
                            //////    double traffic = 0;
                            //////    double traffic1 = 0;
                            //////    double sum = 0;
                            //////    double sum1 = 0;
                            //////    SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            //////    Pdu pdu = new Pdu();
                            //////    pdu.VbList.Add("1.3.6.1.2.1.2.2.1.10.1");
                            //////    Dictionary <Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                            //////    if (result == null)
                            //////    {
                            //////        Console.WriteLine("Request failed.");
                            //////    }
                            //////    else
                            //////    {
                            //////        foreach (KeyValuePair<Oid, AsnType> entry in result)
                            //////        {
                            //////            //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                            //////            traffic = Convert.ToDouble(entry.Value.ToString());
                            //////        }
                            //////    }

                            //////    Thread.Sleep(trafficsleep);

                            //////    SimpleSnmp snmp1 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            //////    Pdu pdu1 = new Pdu();
                            //////    pdu1.VbList.Add("1.3.6.1.2.1.2.2.1.10.1");
                            //////    Dictionary <Oid, AsnType> result1 = snmp1.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                            //////    if (result1 == null)
                            //////    {
                            //////        Console.WriteLine("Request failed.");
                            //////    }
                            //////    else
                            //////    {
                            //////        foreach (KeyValuePair<Oid, AsnType> entry in result1)
                            //////        {
                            //////            //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                            //////            traffic1 = Convert.ToDouble(entry.Value.ToString());
                            //////        }

                            //////    }

                            //////    traffic1 = traffic1 - traffic;
                            //////    sum = traffic1;


                            //////    ///트래픽
                            //////    SimpleSnmp snmp2 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            //////    Pdu pdu2 = new Pdu();
                            //////    pdu2.VbList.Add("1.3.6.1.2.1.2.2.1.16.1");
                            //////    Dictionary<Oid, AsnType> result3 = snmp2.Get(SnmpVersion.Ver2, pdu2); //.GetNext(pdu);
                            //////    if (result3 == null)
                            //////    {
                            //////        Console.WriteLine("Request failed.");
                            //////    }
                            //////    else
                            //////    {
                            //////        foreach (KeyValuePair<Oid, AsnType> entry in result3)
                            //////        {
                            //////            //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                            //////            traffic = Convert.ToDouble(entry.Value.ToString());
                            //////        }
                            //////    }

                            //////    Thread.Sleep(trafficsleep);

                            //////    SimpleSnmp snmp3 = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            //////    Pdu pdu3 = new Pdu();
                            //////    pdu3.VbList.Add("1.3.6.1.2.1.2.2.1.16.1");
                            //////    Dictionary<Oid, AsnType> result4 = snmp3.Get(SnmpVersion.Ver2, pdu3); //.GetNext(pdu);
                            //////    if (result4 == null)
                            //////    {
                            //////        Console.WriteLine("Request failed.");
                            //////    }
                            //////    else
                            //////    {
                            //////        foreach (KeyValuePair<Oid, AsnType> entry in result4)
                            //////        {
                            //////            //Console.WriteLine("traffic : " + entry.Value.ToString() + "bytes");
                            //////            traffic1 = Convert.ToDouble(entry.Value.ToString());
                            //////        }

                            //////    }

                            //////    traffic1 = traffic1 - traffic;

                            //////    sum1 = traffic1;

                            //////    sum1 = sum + sum1;

                            //////    Console.WriteLine("=====================트래픽 : " + +sum1);

                            //////    if (Convert.ToInt32(row["trafficlimit"]) < sum1 && Convert.ToInt32(row["trafficlimit"]) > 0)
                            //////    {
                            //////        double nowtraffic = sum1;
                            //////        Traffic_Mail mail = new Traffic_Mail();
                            //////        mail.Traffic_sendmail(row["serverip"].ToString(), null, nowtraffic.ToString("N1"));
                            //////    }



                            string state = "";
                            //수정본
                            //////CON.Open();
                            //////MySqlCommand cmd = new MySqlCommand();
                            //////cmd.Connection = CON;
                            //////cmd.CommandType = System.Data.CommandType.Text;
                            //////cmd.CommandText = "update server_traffic set traffic = @traffic  where serverip = @serverip ";
                            //////cmd.Parameters.Add("@traffic", MySqlDbType.LongText).Value = sum1;
                            //////cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                            //////cmd.ExecuteNonQuery();
                            //////cmd.Dispose();
                            //////cmd = null;
                            //////CON.Close();

                            //////CON.Open();
                            //////MySqlCommand cmd3 = new MySqlCommand();
                            //////cmd3.Connection = CON;
                            //////cmd3.CommandType = System.Data.CommandType.Text;
                            //////cmd3.CommandText = "insert into temp_system_log_traffic (serverip,traffic) values(@serverip,@traffic) ";
                            //////cmd3.Parameters.Add("@traffic", MySqlDbType.LongText).Value = sum1;
                            //////cmd3.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                            //////cmd3.ExecuteNonQuery();
                            //////cmd3.Dispose();
                            //////cmd3 = null;
                            //////CON.Close();

                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            MySqlCommand cmd4 = new MySqlCommand();
                            cmd4.Connection = CON;
                            cmd4.CommandType = System.Data.CommandType.Text;
                            cmd4.CommandText = "insert into temp_system_log_cpu_memory (serverip,cpu,memory,hd) values(@serverip,@cpu,@memory,@hd) ";
                            cmd4.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = cpu.ToString();
                            cmd4.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = percen.ToString("#.#");
                            cmd4.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = "C: " + percen.ToString("#.#") + "%";
                            cmd4.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                            cmd4.ExecuteNonQuery();
                            cmd4.Dispose();
                            cmd4 = null;
                            CON.Close();




                            string SQL2 = "select count(*) as count from system_log_traffic where serverip = '" + row["serverip"].ToString() + "'  ";
                            MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL2, CON);
                            DataSet DBSET2 = new DataSet();
                            ADT2.Fill(DBSET2, "BD2");

                            foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                            {
                                state = row2["count"].ToString();

                            }
                            if (state == "0")
                            {
                                if (CON.State != ConnectionState.Open)
                                {
                                    CON.Open();
                                }
                                MySqlCommand cmd2 = new MySqlCommand();
                                cmd2.Connection = CON;
                                cmd2.CommandType = System.Data.CommandType.Text;
                                cmd2.CommandText = "insert into system_log_traffic (serverip,traffic) values(@serverip,@traffic) ";
                                cmd2.Parameters.Add("@traffic", MySqlDbType.LongText).Value = "1";
                                cmd2.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                                cmd2.ExecuteNonQuery();
                                cmd2.Dispose();
                                cmd2 = null;
                                CON.Close();
                            }

                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            MySqlCommand cmd33 = new MySqlCommand("Add_System_Log_Cpu_Memory", CON);
                            cmd33.CommandType = CommandType.StoredProcedure;
                            cmd33.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                            cmd33.ExecuteNonQuery();
                            CON.Close();

                            if (CON.State != ConnectionState.Open)
                            {
                                CON.Open();
                            }
                            MySqlCommand cmd44 = new MySqlCommand("Add_System_Log_Traffic", CON);
                            cmd44.CommandType = CommandType.StoredProcedure;
                            cmd44.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                            cmd44.ExecuteNonQuery();
                            CON.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
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
    }


}
