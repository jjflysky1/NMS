using MySql.Data.MySqlClient;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AP
{
    public class Class1
    {
        int sleep = 10000;

        public void APThread()
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
                        //break;
                    }


                    string SQL1 = "";
                    string[] serverip = { };
                    string tempip = "";
                    int count = 0;
                    MySqlDataAdapter ADT1 = new MySqlDataAdapter("Server_list_check", CON);
                    ADT1.SelectCommand.CommandType = CommandType.StoredProcedure;
                    ADT1.SelectCommand.Parameters.AddWithValue("@where1", "AP");
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
                        //AP(serverip[i]);
                        var task1 = Task.Run(() => AP(serverip[i]));

                    });
                    //for (int i = 0; i < count; i++)
                    //{
                    //    thread[i] = new Thread(delegate ()
                    //    {
                    //        AP(serverip[i]);
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
                Console.WriteLine("AP에러");
                Console.WriteLine(e.Message);
            }
        }

        public void AP(string serverip)
        {

            try
            {
                DBCON.Class1 DBCON = new DBCON.Class1();
                MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                string SQL = "";
                //while (true)
                //{
                SQL = "select distinct serverip, os, serverid , log_time, trafficlimit , cpulimit, memorylimit, ifnull (Community, 'public') as community from service a , Log_Time_Config b, mail_info c where a.flag = '1' and category = N'AP' and status = 'Server Connect' " +
                    " and serverip='" + serverip + "'";
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
                                CON.Open();
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
                            CON.Open();
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
                        CON.Open();
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
                                CON.Open();
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

                    //AP 는 AP
                    if (row["os"].ToString().Contains("AP") == true)
                    {
                        //포트 맥주소
                        try
                        {
                            //유선 사용하는 맥주소
                            SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                            Pdu pdu = new Pdu();
                            Dictionary<Oid, AsnType> result = snmp.Walk(SnmpVersion.Ver2, "1.3.6.1.4.1.11898.2.1.33.1.1.2");
                            if (result == null)
                            {
                                Console.WriteLine("Request failed.");
                            }
                            else
                            {


                                foreach (KeyValuePair<Oid, AsnType> entry in result)
                                {
                                    //Console.WriteLine( entry.Key.ToString() + " , " + entry.Value.ToString());
                                    //list.Add(entry.Value.ToString());
                                    //Console.WriteLine(entry.Value.ToString());

                                    CON.Open();
                                    MySqlCommand cmd = new MySqlCommand("ap_mac_add", CON);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                    cmd.Parameters.AddWithValue("@mac1", entry.Value.ToString());
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;
                                    CON.Close();


                                }
                            }

                        }
                        catch
                        {

                        }


                        double percen = 0;
                        ////MEMORY
                        //try
                        //{
                        //    SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                        //    Pdu pdu = new Pdu();
                        //    Pdu pdu1 = new Pdu();
                        //    pdu.VbList.Add(".1.3.6.1.2.1.25.2.3.1.5.1");
                        //    pdu1.VbList.Add(".1.3.6.1.2.1.25.2.3.1.6.1");
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
                        //        Console.WriteLine(percen.ToString("#.#") + "%");

                        //        CON.Open();
                        //        MySqlCommand cmd = new MySqlCommand();
                        //        cmd.Connection = CON;
                        //        cmd.CommandType = System.Data.CommandType.Text;
                        //        cmd.CommandText = "update service set memory = @memory where serverip = @serverip ";
                        //        cmd.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = percen.ToString("#.#");
                        //        cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                        //        cmd.ExecuteNonQuery();
                        //        cmd.Dispose();
                        //        cmd = null;
                        //        CON.Close();
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        //}



                        double hd = 0;
                        ////HD
                        //try
                        //{
                        //    SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
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
                        //        hd = (Convert.ToDouble(used) / Convert.ToDouble(total)) * 100;
                        //        Console.WriteLine(percen.ToString("#.#") + "%");

                        //        CON.Open();
                        //        MySqlCommand cmd = new MySqlCommand();
                        //        cmd.Connection = CON;
                        //        cmd.CommandType = System.Data.CommandType.Text;
                        //        cmd.CommandText = "update server_hd set hd = @hd where serverip = @serverip ";
                        //        cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                        //        cmd.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = "SDB : " + hd.ToString("#.#") + "%";
                        //        cmd.ExecuteNonQuery();
                        //        cmd.Dispose();
                        //        cmd = null;
                        //        CON.Close();

                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        //}

                        double cpu = 0;
                        /////CPU
                        //try
                        //{
                        //    SimpleSnmp snmp = new SimpleSnmp(row["serverip"].ToString(), row["Community"].ToString());
                        //    Pdu pdu = new Pdu();
                        //    pdu.VbList.Add("1.3.6.1.4.1.37288.1.1.3.5.1.0");
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
                        //            cpu = Convert.ToDouble(entry.Value.ToString());
                        //            CON.Open();
                        //            MySqlCommand cmd = new MySqlCommand();
                        //            cmd.Connection = CON;
                        //            cmd.CommandType = System.Data.CommandType.Text;
                        //            cmd.CommandText = "update service set cpu = @cpu where serverip = @serverip ";
                        //            cmd.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = cpu;
                        //            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                        //            cmd.ExecuteNonQuery();
                        //            cmd.Dispose();
                        //            cmd = null;
                        //            CON.Close();
                        //        }
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
                        //}
                        //double cpua = Convert.ToDouble(cpu);
                        //try
                        //{
                        //    ///메일
                        //    if (Convert.ToInt32(row["cpulimit"]) < cpua && Convert.ToInt32(row["cpulimit"]) > 0)
                        //    {
                        //        Cpu_Mail mail = new Cpu_Mail();
                        //        mail.cpu_sendmail(row["serverip"].ToString(), cpu.ToString());
                        //    }
                        //    if (Convert.ToInt32(row["memorylimit"]) < percen && Convert.ToInt32(row["memorylimit"]) > 0)
                        //    {

                        //        Memory_Mail mail = new Memory_Mail();
                        //        mail.memory_sendmail(row["serverip"].ToString(), percen.ToString());
                        //    }
                        //}
                        //catch
                        //{
                        //    Console.WriteLine("메일에러");
                        //}

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
                                            Console.WriteLine(e.Message);
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
                                            Console.WriteLine(e.Message);
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
                            SQL = "select count(*) as count from Server_oid_list where serverip ='" + serverip + "' and description = 'interface-in'";
                            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, CON);
                            DataSet DBSET4 = new DataSet();
                            ADT4.Fill(DBSET4, "BD");
                            temp = "";
                            foreach (DataRow row1 in DBSET4.Tables["BD"].Rows)
                            {
                                portcount = Convert.ToInt32(row1["count"].ToString());
                            }

                            string state = "";
                            CON.Open();
                            for (int j = 0; j < portcount; j++)
                            {
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
                                            cmd.Parameters.AddWithValue("@sum1", sum[j]);
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
                                    cmd.Parameters.AddWithValue("@sum1", sum[j]);
                                    cmd.Parameters.AddWithValue("@cpu1", cpu);
                                    cmd.Parameters.AddWithValue("@memory1", percen);
                                    cmd.Parameters.AddWithValue("@hd1", hd.ToString("#.#"));
                                    cmd.ExecuteNonQuery();
                                    
                                   

                                }
                                if (state == "")
                                {
                                    //CON.Open();
                                    MySqlCommand cmd = new MySqlCommand("Secure_Port_Traffic_Add", CON);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                                    cmd.Parameters.AddWithValue("@portname1", portname[j]);
                                    cmd.Parameters.AddWithValue("@portlive1", portlive[j]);
                                    cmd.Parameters.AddWithValue("@sum1", sum[j]);
                                    cmd.Parameters.AddWithValue("@cpu1", cpu);
                                    cmd.Parameters.AddWithValue("@memory1", percen);
                                    cmd.Parameters.AddWithValue("@hd1", hd.ToString("#.#"));
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;
                                    //CON.Close();
                                }

                                //CON.Open();
                                MySqlCommand cmd3 = new MySqlCommand();
                                cmd3.Connection = CON;
                                cmd3.CommandType = System.Data.CommandType.Text;
                                cmd3.CommandText = "insert into Temp_Secure_Port_Log_Traffic (serverip,traffic,time) values(@serverip,@traffic,now()) ";
                                cmd3.Parameters.Add("@traffic", MySqlDbType.VarChar, 100).Value = sum[j];
                                cmd3.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString() + " " + portname[j];
                                cmd3.ExecuteNonQuery();
                                cmd3.Dispose();
                                cmd3 = null;
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
