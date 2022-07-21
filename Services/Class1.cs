using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

using System.Threading;

using System.Diagnostics;
using Renci.SshNet;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace SERVICECLS
{
    //리눅스 , 윈도우 서버 서비스 살리는 함수, 서버 상태 함수
    public class SERVICECLS
    {
        /// <summary>
        /// 라이센스 갯수
        /// </summary>
        int sleep = 10000;

        public void service_window_thread()
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


                    int count = 0;

                    string[] serverip = { };
                    string tempip = "";
                    MySqlDataAdapter ADT1 = new MySqlDataAdapter("Server_list_check", CON);
                    ADT1.SelectCommand.CommandType = CommandType.StoredProcedure;
                    ADT1.SelectCommand.Parameters.AddWithValue("@where1", " ");
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
                        service_window(serverip[i]);

                    });
                    //for (int i = 0; i < count; i++)
                    //{

                    //    thread[i] = new Thread(delegate ()
                    //    {
                    //        service_window(serverip[i]);
                    //    });
                    //    thread[i].Start();
                    //    Thread.Sleep(500);

                    //}
                    //for (int i = 0; i < count; i++)
                    //{
                    //    thread[i].Join(60000);
                    //    thread[i] = null;
                    //    Thread.Sleep(500);

                    //}
                    Thread.Sleep(sleep);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void service_linux_thread()
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

                    string SQL = "";
                    int count = 0;

                    string[] serverip = { };
                    string tempip = "";
                    MySqlDataAdapter ADT1 = new MySqlDataAdapter("Server_list_check", CON);
                    ADT1.SelectCommand.CommandType = CommandType.StoredProcedure;
                    ADT1.SelectCommand.Parameters.AddWithValue("@where1", " ");
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
                    var options = new ParallelOptions()
                    {
                        MaxDegreeOfParallelism = 10
                    };
                    Parallel.For(0, count, i =>
                   {
                       service_linux(serverip[i]);

                   });
                    //for (int i = 0; i < count; i++)
                    //{

                    //    thread[i] = new Thread(delegate ()
                    //    {
                    //        service_linux(serverip[i]);
                    //    });
                    //    thread[i].Start();
                    //    Thread.Sleep(500);

                    //}
                    //for (int i = 0; i < count; i++)
                    //{
                    //    thread[i].Join(60000);
                    //    thread[i] = null;
                    //    Thread.Sleep(500);

                    //}

                    Thread.Sleep(sleep);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void service_window(string serverip)
        {
            try
            {
                DBCON.Class1 DBCON = new DBCON.Class1();
                MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                string SQL = "";

                //while (true)
                //{
                SQL = "select serverip,no,serverid,serverpwd,os,name from service where flag = '1'  and name is not null and serverip = '" + serverip + "'";
                MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");
                string CO = "";

                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {

                    if (row["serverid"].ToString() != "")
                    {
                        CO = row["os"].ToString();

                        if (row["name"].ToString() == "")
                        {
                            Console.WriteLine("서비스 없다");
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = CON;
                            CON.Open();
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandText = "update service set status = @state where no = @no ";
                            cmd.Parameters.Add("@state", MySqlDbType.VarChar, 100).Value = "Server Connect";
                            cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = row["no"].ToString();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cmd = null;
                            CON.Close();
                            CO = "Pass";
                        }

                        if (row["os"].ToString().Contains("Windows") == true)
                        {
                            Process p = new Process();
                            p.StartInfo.CreateNoWindow = true;
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.RedirectStandardOutput = true;
                            p.StartInfo.RedirectStandardInput = true;
                            p.StartInfo.FileName = @"C:\Windows\System32\psexec.exe";
                            p.StartInfo.Arguments = @"\\" + row["serverip"].ToString() + " -u " + row["Serverid"].ToString() + " -p " + row["Serverpwd"].ToString() + " sc query " + row["name"].ToString();


                            p.Start();

                            //p.StandardInput.Write(@"sc stop MSSQLSERVER");
                            string output = p.StandardOutput.ReadToEnd();
                            Console.WriteLine(output);



                            if (output.Contains("1  STOPPED") == true)
                            {
                                try
                                {
                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.Connection = CON;
                                    CON.Open();
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                    cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = row["name"].ToString();
                                    cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["Serverip"].ToString();
                                    cmd.Parameters.Add("@time", MySqlDbType.VarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                    cmd.Parameters.Add("@status", MySqlDbType.VarChar, 100).Value = "Down";
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;
                                    CON.Close();
                                }
                                catch
                                {

                                }


                                Process p2 = new Process();
                                Console.WriteLine(row["name"].ToString() + " DIE");
                                p2.StartInfo.FileName = @"C:\Windows\System32\psexec.exe";
                                p2.StartInfo.Arguments = @"\\" + row["serverip"].ToString() + " -u " + row["Serverid"].ToString() + " -p " + row["Serverpwd"].ToString() + " sc start " + row["name"].ToString();
                                p2.Start();

                                CON.Open();
                                MySqlCommand cmd1 = new MySqlCommand();
                                cmd1.Connection = CON;

                                cmd1.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                cmd1.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = row["name"].ToString();
                                cmd1.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["Serverip"].ToString();
                                cmd1.Parameters.Add("@time", MySqlDbType.VarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                cmd1.Parameters.Add("@status", MySqlDbType.VarChar, 100).Value = "Alive";
                                cmd1.ExecuteNonQuery();
                                cmd1.Dispose();
                                cmd1 = null;

                                CON.Close();
                                p2.WaitForExit();
                                p2.Dispose();
                            }
                            else
                            {
                                Console.WriteLine(row["name"].ToString() + " Alive");
                            }
                            p.WaitForExit();
                            p.Dispose();

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

        public void service_linux(string serverip)
        {
            try
            {
                DBCON.Class1 DBCON = new DBCON.Class1();
                MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                string SQL = "";

                //while (true)
                //{
                SQL = "select serverip,no,serverid,serverpwd,os,name,sshport from service where flag = '1'  and name is not null and serverip = '" + serverip + "' ";
                MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");
                string CO = "";

                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {

                    if (row["os"].ToString().Contains("Linux") == true)
                    {
                        if (row["serverid"].ToString() != "")
                        {
                            if (row["name"].ToString().Length > 1)
                            {
                                try
                                {
                                    int sshport = 22;
                                    if (row["sshport"].ToString() == "")
                                    {

                                    }
                                    else
                                    {
                                        sshport = Convert.ToInt32(row["sshport"].ToString());
                                    }

                                    Console.WriteLine(row["serverip"].ToString() + " status -Connecting...");
                                    SshClient ssh = new SshClient(row["serverip"].ToString(), sshport, row["serverid"].ToString(), row["serverpwd"].ToString());
                                    ssh.Connect();
                                    var sshcmd = ssh.RunCommand("ps -ef | grep " + row["name"].ToString());
                                    sshcmd.Execute();
                                    string osstatus = sshcmd.Result;
                                    string[] osstatus1 = osstatus.Split('\n');
                                    Console.WriteLine(osstatus1[2]);

                                    MySqlCommand cmd3 = new MySqlCommand();
                                    cmd3.Connection = CON;
                                    CON.Open();
                                    cmd3.CommandType = System.Data.CommandType.Text;
                                    cmd3.CommandText = "update service set status = @state where no = @no ";
                                    cmd3.Parameters.Add("@state", MySqlDbType.VarChar, 100).Value = "Server Connect";
                                    cmd3.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = row["no"].ToString();
                                    cmd3.ExecuteNonQuery();
                                    cmd3.Dispose();
                                    cmd3 = null;
                                    CON.Close();

                                    CO = "Pass";
                                    if (osstatus1[2].Contains(row["name"].ToString()) == true)
                                    {
                                        Console.WriteLine(row["name"].ToString() + " Alive");
                                    }
                                    else
                                    {
                                        Console.WriteLine(row["name"].ToString() + " Dead");
                                        sshcmd = ssh.RunCommand("service " + row["name"].ToString() + " start");
                                        sshcmd.Execute();
                                        string lios2 = sshcmd.Result;


                                        if (lios2.Contains("Fail") == true)
                                        {

                                            Console.WriteLine(row["name"].ToString() + " Have None ");
                                            MySqlCommand cmd = new MySqlCommand();
                                            cmd.Connection = CON;
                                            CON.Open();
                                            cmd.CommandType = System.Data.CommandType.Text;
                                            cmd.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = row["name"].ToString();
                                            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["Serverip"].ToString();
                                            cmd.Parameters.Add("@time", MySqlDbType.VarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                            cmd.Parameters.Add("@status", MySqlDbType.VarChar, 100).Value = "Service Have None";
                                            cmd.ExecuteNonQuery();
                                            cmd.Dispose();
                                            cmd = null;
                                            CON.Close();
                                        }
                                        else
                                        {
                                            MySqlCommand cmd = new MySqlCommand();
                                            cmd.Connection = CON;
                                            CON.Open();
                                            cmd.CommandType = System.Data.CommandType.Text;
                                            cmd.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = row["name"].ToString();
                                            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["Serverip"].ToString();
                                            cmd.Parameters.Add("@time", MySqlDbType.VarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                            cmd.Parameters.Add("@status", MySqlDbType.VarChar, 100).Value = "Down";
                                            cmd.ExecuteNonQuery();
                                            cmd.Dispose();
                                            cmd = null;
                                            CON.Close();

                                            sshcmd = ssh.RunCommand("service " + row["name"].ToString() + " start");
                                            sshcmd.Execute();
                                            string lios3 = sshcmd.Result;


                                            CON.Open();
                                            MySqlCommand cmd2 = new MySqlCommand();
                                            cmd2.Connection = CON;
                                            cmd2.CommandType = System.Data.CommandType.Text;
                                            cmd2.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                            cmd2.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = row["name"].ToString();
                                            cmd2.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["Serverip"].ToString();
                                            cmd2.Parameters.Add("@time", MySqlDbType.VarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                            cmd2.Parameters.Add("@status", MySqlDbType.VarChar, 100).Value = "Alive";
                                            cmd2.ExecuteNonQuery();
                                            cmd2.Dispose();
                                            cmd2 = null;
                                            CON.Close();
                                        }



                                    }

                                    ssh.Disconnect();
                                    ssh.Dispose();
                                    CO = "Pass";
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    if (e.Message == "Auth fail")
                                    {
                                        MySqlCommand cmd = new MySqlCommand();
                                        cmd.Connection = CON;
                                        CON.Open();
                                        cmd.CommandType = System.Data.CommandType.Text;
                                        cmd.CommandText = "update service set status = @state where no = @no ";
                                        cmd.Parameters.Add("@state", MySqlDbType.VarChar, 100).Value = "Server Disconnect";
                                        cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = row["no"].ToString();
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                        cmd = null;
                                        CON.Close();

                                    }
                                }
                            }

                        }
                    }
                    //Thread.Sleep(sleep);

                }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

    }
}
