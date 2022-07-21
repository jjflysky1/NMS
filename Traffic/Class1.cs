using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace Traffic
{
    public class Class1
    {
        /// <summary>
        /// 라이센스 갯수
        /// </summary>
        int sleep = 10000;
        
        public void networkinfo_thread()
        {
            try
            {
                while (true)
                {
                    DBCON.Class1 DBCON = new DBCON.Class1();
                    SqlConnection CON = new SqlConnection(DBCON.DBCON);


                    ///
                    ///특정날짜 넘어가면 아웃
                    ///
                    string SQL2 = "select  convert(varchar(8), getdate(), 112) as time";
                    SqlDataAdapter ADT2 = new SqlDataAdapter(SQL2, CON);
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
                        break;
                    }


                    string SQL = "";
                    int count = 0;

                    string[] serverip = { };
                    string tempip = "";
                    SqlDataAdapter ADT1 = new SqlDataAdapter("Server_list_check", CON);
                    ADT1.SelectCommand.CommandType = CommandType.StoredProcedure;
                    ADT1.SelectCommand.Parameters.AddWithValue("@where", " ");
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
                    for (int i = 0; i < count; i++)
                    {
                        //Console.WriteLine(serverip[i]);
                        thread[i] = new Thread(delegate ()
                        {
                            networkinfo(serverip[i]);
                        });
                        thread[i].Start();
                        Thread.Sleep(500);


                    }
                    for (int i = 0; i < count; i++)
                    {
                        thread[i].Join(60000);
                        Thread.Sleep(500);
                        thread[i].Abort();
                    }
                    Thread.Sleep(sleep);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            

        }
        public void networkinfo(string serverip)
        {
            try
            {
                DBCON.Class1 DBCON = new DBCON.Class1();
                SqlConnection CON = new SqlConnection(DBCON.DBCON);
                string SQL = "";
                //while (true)
                //{
                    SQL = "select distinct  os, a.serverip, serverid, serverpwd, b.trafficlimit , c.log_time  from service a , mail_info b , Log_Time_Config c" +
                        " where a.flag = '1' and a.status = 'Server Connect' and a.serverip = '" + serverip + "'  ";
                    SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
                    DataSet DBSET = new DataSet();
                    ADT.Fill(DBSET, "BD");
                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {
                    ///윈도우
                    ///
                    if (row["os"].ToString().Contains("Windows") == true)
                    {
                      
                            try
                            {
                                Console.WriteLine(row["serverip"].ToString() + " traffic -Connecting...");
                                ConnectionOptions con = new ConnectionOptions();
                                con.Username = row["serverid"].ToString();
                                con.Password = row["serverpwd"].ToString();
                                ManagementScope servercon = new ManagementScope(@"\\" + row["serverip"].ToString() + @"\root\cimv2", con);
                                servercon.Connect();


                                ObjectQuery getnetwork = new ObjectQuery("SELECT * FROM Win32_PerfFormattedData_TCPIP_NetworkInterface");
                                ManagementObjectSearcher getnetwork2 = new ManagementObjectSearcher(servercon, getnetwork);
                                // Loop through the drives retrieved, although it should normally be only one loop going on here
                                ManagementObjectCollection loResult = getnetwork2.Get();

                                foreach (ManagementObject network in loResult)
                                {
                                    if (Convert.ToDecimal(network["BytesTotalPersec"]) > 0)
                                    {

                                        Console.WriteLine("Name: " + network["Name"]);
                                        Console.WriteLine("Traffic: " + Convert.ToDecimal(network["BytesTotalPersec"]));
                                        //Console.WriteLine("send: " + Convert.ToDecimal(network["BytesSentPersec"]));
                                        //Console.WriteLine("recive: " + Convert.ToDecimal(network["BytesReceivedPersec"]));


                                        if (Convert.ToInt32(row["trafficlimit"]) < (Convert.ToInt32(network["BytesTotalPersec"])) && Convert.ToInt32(row["trafficlimit"]) > 0)
                                        {
                                            decimal nowtraffic = (Convert.ToDecimal(network["BytesTotalPersec"]));
                                            Traffic_Mail mail = new Traffic_Mail();
                                            mail.Traffic_sendmail(row["serverip"].ToString(), nowtraffic.ToString("N1"));
                                        }

                                        string state = "";
                                        if (row["log_time"].ToString() == "1")//1시간
                                        {
                                            string SQL2 = "select top 1 SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2)  as time from system_log_traffic where serverip = '" + row["serverip"].ToString() + "' order by no desc ";
                                            SqlDataAdapter ADT2 = new SqlDataAdapter(SQL2, CON);
                                            DataSet DBSET2 = new DataSet();
                                            ADT2.Fill(DBSET2, "BD2");

                                            foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                                            {
                                                string time1 = DateTime.Now.ToString("HH");
                                                string time2 = DateTime.Now.ToString(row2["time"].ToString());
                                                //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));

                                                if (time1 != time2)
                                                {
                                                    //systeminfo에서 cpu 넣을때 같이 넣는걸로 변경
                                                    //CON.Open();
                                                    //SqlCommand cmd2 = new SqlCommand("Add_System_Log_Traffic", CON);
                                                    //cmd2.CommandType = CommandType.StoredProcedure;
                                                    //cmd2.Parameters.AddWithValue("@serverip", row["serverip"].ToString());
                                                    //cmd2.ExecuteNonQuery();
                                                    //CON.Close();
                                                }
                                                state = "in";
                                            }
                                        }
                                        if (row["log_time"].ToString() == "2")//30분
                                        {
                                            string SQL2 = "select top 1 SUBSTRING(Convert(nvarchar,dateadd(mi, 30,time),121),0,17)  as time from system_log_traffic where serverip = '" + row["serverip"].ToString() + "' order by no desc ";
                                            SqlDataAdapter ADT2 = new SqlDataAdapter(SQL2, CON);
                                            DataSet DBSET2 = new DataSet();
                                            ADT2.Fill(DBSET2, "BD2");

                                            foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                                            {
                                                string time1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                                                string time2 = DateTime.Now.ToString(row2["time"].ToString());
                                                //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                                                //time2 = Regex.Replace(time2, " ", ":");
                                                if (Convert.ToDateTime(time1) > Convert.ToDateTime(time2))
                                                {
                                                    //CON.Open();
                                                    //SqlCommand cmd2 = new SqlCommand("Add_System_Log_Traffic", CON);
                                                    //cmd2.CommandType = CommandType.StoredProcedure;
                                                    //cmd2.Parameters.AddWithValue("@serverip", row["serverip"].ToString());
                                                    //cmd2.ExecuteNonQuery();
                                                    //CON.Close();
                                                }
                                                state = "in";
                                            }
                                        }

                                        if (state == "")
                                        {
                                            SqlCommand cmd2 = new SqlCommand();
                                            cmd2.Connection = CON;
                                            CON.Open();
                                            cmd2.CommandType = System.Data.CommandType.Text;
                                            cmd2.CommandText = "insert into system_log_traffic (serverip,traffic) values(@serverip,@traffic) ";
                                            cmd2.Parameters.Add("@traffic", SqlDbType.NVarChar, 100).Value = Convert.ToDecimal(network["BytesTotalPersec"]);
                                            cmd2.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                            cmd2.ExecuteNonQuery();
                                            cmd2.Dispose();
                                            cmd2 = null;
                                            CON.Close();
                                        }


                                        CON.Open();
                                        SqlCommand cmd = new SqlCommand();
                                        cmd.Connection = CON;
                                        cmd.CommandType = System.Data.CommandType.Text;
                                        cmd.CommandText = "update server_traffic set traffic = @traffic  where serverip = @serverip ";
                                        cmd.Parameters.Add("@traffic", SqlDbType.NVarChar, 100).Value = Convert.ToDecimal(network["BytesTotalPersec"]);
                                        cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                        cmd = null;
                                        CON.Close();
                                        CON.Open();

                                        SqlCommand cmd3 = new SqlCommand();
                                        cmd3.Connection = CON;
                                        cmd3.CommandType = System.Data.CommandType.Text;
                                        cmd3.CommandText = "insert into temp_system_log_traffic (serverip,traffic) values(@serverip,@traffic) ";
                                        cmd3.Parameters.Add("@traffic", SqlDbType.NVarChar, 100).Value = Convert.ToDecimal(network["BytesTotalPersec"]);
                                        cmd3.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                        cmd3.ExecuteNonQuery();
                                        cmd3.Dispose();
                                        cmd3 = null;
                                        CON.Close();

                                    }

                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message + row["serverip"].ToString());
                            }
                        
                    }
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
         
        }
       
    }
}
