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

namespace SERVICECLS
{
    //리눅스 , 윈도우 서버 서비스 살리는 함수, 서버 상태 함수
    public class SERVICECLS
    {
        int sleep = 10000;
        public void service_window_thread()
        {
            DBCON.Class1 DBCON = new DBCON.Class1();
            SqlConnection CON = new SqlConnection(DBCON.DBCON);
            string SQL = "";
            int count = 0;
            SQL = "select  count(distinct serverip) as count from service where flag = '1'";
            SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                count = Convert.ToInt32(row["count"].ToString());
            }


            string SQL1 = "";
            string[] serverip = { };
            string tempip = "";
            SQL1 = "select distinct serverip  from service where flag = '1'  ";
            SqlDataAdapter ADT1 = new SqlDataAdapter(SQL1, CON);
            DataSet DBSET1 = new DataSet();
            ADT1.Fill(DBSET1, "BD1");
            foreach (DataRow row in DBSET1.Tables["BD1"].Rows)
            {
                tempip += row["serverip"].ToString() + ",";

            }

            serverip = tempip.Split(',');

            Thread[] thread = new Thread[count];
            for (int i = 0; i < count; i++)
            {

                thread[i] = new Thread(delegate ()
                {
                    service_window(serverip[i]);
                });
                thread[i].Start();
                Thread.Sleep(1000);
            }


        }

        public void service_linux_thread()
        {
            DBCON.Class1 DBCON = new DBCON.Class1();
            SqlConnection CON = new SqlConnection(DBCON.DBCON);
            string SQL = "";
            int count = 0;
            SQL = "select  count(distinct serverip) as count from service where flag = '1'";
            SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                count = Convert.ToInt32(row["count"].ToString());
            }


            string SQL1 = "";
            string[] serverip = { };
            string tempip = "";
            SQL1 = "select distinct serverip  from service where flag = '1'  ";
            SqlDataAdapter ADT1 = new SqlDataAdapter(SQL1, CON);
            DataSet DBSET1 = new DataSet();
            ADT1.Fill(DBSET1, "BD1");
            foreach (DataRow row in DBSET1.Tables["BD1"].Rows)
            {
                tempip += row["serverip"].ToString() + ",";

            }

            serverip = tempip.Split(',');

            Thread[] thread = new Thread[count];
            for (int i = 0; i < count; i++)
            {

                thread[i] = new Thread(delegate ()
                {
                    service_linux(serverip[i]);
                });
                thread[i].Start();
                Thread.Sleep(1000);
            }


        }
        public void service_window(string serverip)
        {
            DBCON.Class1 DBCON = new DBCON.Class1();
            SqlConnection CON = new SqlConnection(DBCON.DBCON);
            string SQL = "";

            while (true)
            {
                SQL = "select * from service where flag = '1' and serverip = '"+serverip+"'";
                SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");
                string CO = "";

                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {
                    Ping ping1 = new Ping();
                    PingOptions options1 = new PingOptions();
                    options1.DontFragment = true;
                    string data1 = "aaaaaaaaaaaaaaaaa";
                    byte[] buffer1 = ASCIIEncoding.ASCII.GetBytes(data1);
                    int timeout1 = 120;
                    PingReply reply2 = ping1.Send(row["serverip"].ToString(), timeout1, buffer1, options1);
                    if (reply2.Status != IPStatus.Success) // 네트워크 사용 불가능 할때~~
                    {
                        //Console.WriteLine(row["serverip"].ToString() + " 사용 불가");
                        //CON.Open();
                        //SqlCommand cmd2 = new SqlCommand();
                        //cmd2.Connection = CON;
                        //cmd2.CommandType = System.Data.CommandType.Text;
                        //cmd2.CommandText = "update service set status = @status where no = @no ";
                        //cmd2.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Server Disconnect";
                        //cmd2.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
                        //cmd2.ExecuteNonQuery();
                        //cmd2.Dispose();
                        //cmd2 = null;
                        //CON.Close();
                    }
                    else
                    {
                        Console.WriteLine(row["serverip"].ToString() + " 사용 가능");
                        CON.Open();
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = CON;
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.CommandText = "update service set status = @status where no = @no ";
                        cmd2.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Server Connect";
                        cmd2.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
                        cmd2.ExecuteNonQuery();
                        cmd2.Dispose();
                        cmd2 = null;
                        CON.Close();
                    }

                    if (row["serverid"].ToString() != "")
                    {
                        CO = row["os"].ToString();
                        Ping ping = new Ping();
                        PingOptions options = new PingOptions();
                        options.DontFragment = true;
                        string data = "aaaaaaaaaaaaaaaaa";
                        byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
                        int timeout = 120;
                        PingReply reply = ping.Send(row["serverip"].ToString(), timeout, buffer, options);
                        if (reply.Status == IPStatus.Success) // 네트워크 사용 가능할 때~~
                        {
                            if (row["name"].ToString() == "")
                            {
                                Console.WriteLine("서비스 없다");
                                SqlCommand cmd = new SqlCommand();
                                cmd.Connection = CON;
                                CON.Open();
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set status = @state where no = @no ";
                                cmd.Parameters.Add("@state", SqlDbType.NVarChar, 100).Value = "Server Connect";
                                cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();
                                CO = "Pass";
                            }

                            if (row["os"].ToString().Contains("Windows") == true)
                            {
                                Process p3 = new Process();
                                p3.StartInfo.UseShellExecute = false;
                                p3.StartInfo.RedirectStandardOutput = true;
                                p3.StartInfo.RedirectStandardInput = true;
                                p3.StartInfo.FileName = @"C:\Windows\System32\pslist.exe";
                                p3.StartInfo.Arguments = @"\\" + row["serverip"].ToString() + " -u " + row["Serverid"].ToString() + " -p " + row["Serverpwd"].ToString();
                                p3.Start();
                                string pslist = p3.StandardOutput.ReadToEnd();
                                //Console.WriteLine(pslist);
                                if (pslist.Contains("액세스") == true || pslist.Contains("Fail") == true)//아이디 비번이 틀릴때
                                {
                                    //Console.WriteLine("들어왔다");
                                    try
                                    {
                                        //SqlCommand cmd = new SqlCommand();
                                        //cmd.Connection = CON;
                                        //CON.Open();
                                        //cmd.CommandType = System.Data.CommandType.Text;
                                        //cmd.CommandText = "update service set status = @status where no = @no ";
                                        //cmd.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Server Disconnect";
                                        //cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
                                        //cmd.ExecuteNonQuery();
                                        //cmd.Dispose();
                                        //cmd = null;
                                        //CON.Close();
                                    }
                                    catch
                                    {

                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        SqlCommand cmd = new SqlCommand();
                                        cmd.Connection = CON;
                                        CON.Open();
                                        cmd.CommandType = System.Data.CommandType.Text;
                                        cmd.CommandText = "update service set status = @os where no = @no ";
                                        cmd.Parameters.Add("@OS", SqlDbType.NVarChar, 100).Value = "Server Connect";
                                        cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                        cmd = null;
                                        CON.Close();
                                    }
                                    catch
                                    {

                                    }
                                }

                                p3.WaitForExit();
                            }


                            if (row["os"].ToString().Contains("Windows") == true)
                            {
                                Process p = new Process();
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
                                        SqlCommand cmd = new SqlCommand();
                                        cmd.Connection = CON;
                                        CON.Open();
                                        cmd.CommandType = System.Data.CommandType.Text;
                                        cmd.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = row["name"].ToString();
                                        cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["Serverip"].ToString();
                                        cmd.Parameters.Add("@time", SqlDbType.NVarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                        cmd.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Down";
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                        cmd = null;
                                        CON.Close();
                                    }
                                    catch
                                    {

                                    }



                                    Console.WriteLine(row["name"].ToString() + " DIE");
                                    p.StartInfo.FileName = @"C:\Windows\System32\psexec.exe";
                                    p.StartInfo.Arguments = @"\\" + row["serverip"].ToString() + " -u " + row["Serverid"].ToString() + " -p " + row["Serverpwd"].ToString() + " sc start " + row["name"].ToString();
                                    p.Start();

                                    CON.Open();
                                    SqlCommand cmd1 = new SqlCommand();
                                    cmd1.Connection = CON;

                                    cmd1.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                    cmd1.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = row["name"].ToString();
                                    cmd1.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["Serverip"].ToString();
                                    cmd1.Parameters.Add("@time", SqlDbType.NVarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                    cmd1.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Alive";
                                    cmd1.ExecuteNonQuery();
                                    cmd1.Dispose();
                                    cmd1 = null;

                                    CON.Close();
                                }
                                else if (output.Contains("could not") == true)
                                {

                                    try
                                    {
                                        //CON.Open();

                                        //SqlCommand cmd = new SqlCommand();
                                        //cmd.Connection = CON;

                                        //cmd.CommandType = System.Data.CommandType.Text;
                                        //cmd.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                        //cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = row["name"].ToString();
                                        //cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["Serverip"].ToString();
                                        //cmd.Parameters.Add("@time", SqlDbType.NVarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                        //cmd.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Server Disconnect";
                                        //cmd.ExecuteNonQuery();
                                        //cmd.Dispose();
                                        //cmd = null;

                                        //CON.Close();
                                    }
                                    catch
                                    {

                                    }


                                }
                                else
                                {
                                    Console.WriteLine(row["name"].ToString() + " Alive");
                                }
                                p.WaitForExit();

                            }
                            Thread.Sleep(sleep);
                        }
                        else if (CO != "Pass")
                        {

                            try
                            {


                                CON.Open();
                                SqlCommand cmd2 = new SqlCommand();
                                cmd2.Connection = CON;
                                cmd2.CommandType = System.Data.CommandType.Text;
                                cmd2.CommandText = "update service set status = @status where no = @no ";
                                cmd2.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Server Disconnect";
                                cmd2.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
                                cmd2.ExecuteNonQuery();
                                cmd2.Dispose();
                                cmd2 = null;
                                CON.Close();

                                //CON.Open();
                                //SqlCommand cmd3 = new SqlCommand();
                                //cmd3.Connection = CON;
                                //cmd3.CommandType = System.Data.CommandType.Text;
                                //cmd3.CommandText = "insert into server_down_log select @serverip, getdate()";
                                //cmd3.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                //cmd3.ExecuteNonQuery();
                                //cmd3.Dispose();
                                //cmd3 = null;
                                //CON.Close();


                            }
                            catch
                            {

                            }
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }

        public void service_linux(string serverip)
        {
            DBCON.Class1 DBCON = new DBCON.Class1();
            SqlConnection CON = new SqlConnection(DBCON.DBCON);
            string SQL = "";

            while (true)
            {
                SQL = "select * from service where flag = '1' and serverip = '"+ serverip +"' ";
                SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");
                string CO = "";

                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {
                    if (row["os"].ToString().Contains("Linux") == true)
                    {
                        Ping ping1 = new Ping();
                        PingOptions options1 = new PingOptions();
                        options1.DontFragment = true;
                        string data1 = "aaaaaaaaaaaaaaaaa";
                        byte[] buffer1 = ASCIIEncoding.ASCII.GetBytes(data1);
                        int timeout1 = 120;
                        PingReply reply2 = ping1.Send(row["serverip"].ToString(), timeout1, buffer1, options1);
                        if (reply2.Status != IPStatus.Success) // 네트워크 사용 불가능 할때~~
                        {
                            Console.WriteLine(row["serverip"].ToString() + " 사용 불가");
                            CON.Open();
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.Connection = CON;
                            cmd2.CommandType = System.Data.CommandType.Text;
                            cmd2.CommandText = "update service set status = @status where no = @no ";
                            cmd2.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Server Disconnect";
                            cmd2.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
                            cmd2.ExecuteNonQuery();
                            cmd2.Dispose();
                            cmd2 = null;
                            CON.Close();


                        }
                        else
                        {
                            Console.WriteLine(row["serverip"].ToString() + " 사용 가능");
                            CON.Open();
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.Connection = CON;
                            cmd2.CommandType = System.Data.CommandType.Text;
                            cmd2.CommandText = "update service set status = @status where no = @no ";
                            cmd2.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Server Connect";
                            cmd2.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
                            cmd2.ExecuteNonQuery();
                            cmd2.Dispose();
                            cmd2 = null;
                            CON.Close();
                        }

                        if (row["serverid"].ToString() != "")
                        {
                            CO = row["os"].ToString();
                            Ping ping = new Ping();
                            PingOptions options = new PingOptions();
                            options.DontFragment = true;
                            string data = "aaaaaaaaaaaaaaaaa";
                            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
                            int timeout = 120;
                            PingReply reply = ping.Send(row["serverip"].ToString(), timeout, buffer, options);
                            if (reply.Status == IPStatus.Success) // 네트워크 사용 가능할 때~~
                            {
                                if (row["name"].ToString() == "")
                                {
                                    Console.WriteLine("서비스 없다");
                                    SqlCommand cmd = new SqlCommand();
                                    cmd.Connection = CON;
                                    CON.Open();
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.CommandText = "update service set status = @state where no = @no ";
                                    cmd.Parameters.Add("@state", SqlDbType.NVarChar, 100).Value = "Server Connect";
                                    cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;
                                    CON.Close();
                                    CO = "Pass";
                                }

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

                                    SqlCommand cmd3 = new SqlCommand();
                                    cmd3.Connection = CON;
                                    CON.Open();
                                    cmd3.CommandType = System.Data.CommandType.Text;
                                    cmd3.CommandText = "update service set status = @state where no = @no ";
                                    cmd3.Parameters.Add("@state", SqlDbType.NVarChar, 100).Value = "Server Connect";
                                    cmd3.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
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
                                            SqlCommand cmd = new SqlCommand();
                                            cmd.Connection = CON;
                                            CON.Open();
                                            cmd.CommandType = System.Data.CommandType.Text;
                                            cmd.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = row["name"].ToString();
                                            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["Serverip"].ToString();
                                            cmd.Parameters.Add("@time", SqlDbType.NVarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                            cmd.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Service Have None";
                                            cmd.ExecuteNonQuery();
                                            cmd.Dispose();
                                            cmd = null;
                                            CON.Close();
                                        }
                                        else
                                        {
                                            SqlCommand cmd = new SqlCommand();
                                            cmd.Connection = CON;
                                            CON.Open();
                                            cmd.CommandType = System.Data.CommandType.Text;
                                            cmd.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = row["name"].ToString();
                                            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["Serverip"].ToString();
                                            cmd.Parameters.Add("@time", SqlDbType.NVarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                            cmd.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Down";
                                            cmd.ExecuteNonQuery();
                                            cmd.Dispose();
                                            cmd = null;
                                            CON.Close();

                                            sshcmd = ssh.RunCommand("service " + row["name"].ToString() + " start");
                                            sshcmd.Execute();
                                            string lios3 = sshcmd.Result;


                                            CON.Open();
                                            SqlCommand cmd2 = new SqlCommand();
                                            cmd2.Connection = CON;
                                            cmd2.CommandType = System.Data.CommandType.Text;
                                            cmd2.CommandText = "insert into down_log (name, serverip, time, status) values(@name,@serverip,@time,@status)";
                                            cmd2.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = row["name"].ToString();
                                            cmd2.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["Serverip"].ToString();
                                            cmd2.Parameters.Add("@time", SqlDbType.NVarChar, 100).Value = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                                            cmd2.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Alive";
                                            cmd2.ExecuteNonQuery();
                                            cmd2.Dispose();
                                            cmd2 = null;
                                            CON.Close();
                                        }



                                    }

                                    ssh.Disconnect();
                                    CO = "Pass";
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    if (e.Message == "Auth fail")
                                    {
                                        SqlCommand cmd = new SqlCommand();
                                        cmd.Connection = CON;
                                        CON.Open();
                                        cmd.CommandType = System.Data.CommandType.Text;
                                        cmd.CommandText = "update service set status = @state where no = @no ";
                                        cmd.Parameters.Add("@state", SqlDbType.NVarChar, 100).Value = "Server Disconnect";
                                        cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = row["no"].ToString();
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                        cmd = null;
                                        CON.Close();
                                        CO = "Pass";
                                    }
                                }

                            }
                        }
                    }
                }
                Thread.Sleep(sleep);
            }

        }

    }
}
