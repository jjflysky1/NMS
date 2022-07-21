using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace OS
{
    public class Class1
    {
        int sleep = 10000;
        int Liesence = 200;
        public void os_thread()
        {
            while(true)
            {
                DBCON.Class1 DBCON = new DBCON.Class1();
                SqlConnection CON = new SqlConnection(DBCON.DBCON);

                string SQL1 = "";
                string[] serverip = { };
                string tempip = "";
                int count = 0;
                SqlDataAdapter ADT = new SqlDataAdapter("Server_list_check", CON);
                ADT.SelectCommand.CommandType = CommandType.StoredProcedure;
                ADT.SelectCommand.Parameters.AddWithValue("@where", " ");
                ADT.SelectCommand.Parameters.AddWithValue("@liesence", Liesence);
                DataSet DBSET1 = new DataSet();
                ADT.Fill(DBSET1, "BD1");
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
                        OS(serverip[i]);
                    });
                    thread[i].Start();
                    Thread.Sleep(500);
                }
              
            }
 
        }
        public void OS(string serverip)
        {
            try
            {
                DBCON.Class1 DBCON = new DBCON.Class1();
                SqlConnection CON = new SqlConnection(DBCON.DBCON);
                string SQL = "";
                //while (true)
                //{
                SQL = "select distinct serverip,os,serverid,serverpwd from service where flag = '1' and serverid is not null and serverip = '" + serverip + "' ";
                SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");
                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {

                    string CO = "";
                    Console.WriteLine(row["serverip"].ToString() + " OS 검사");
                    Process p2 = new Process();
                    try
                    {

                        p2.StartInfo.UseShellExecute = false;
                        p2.StartInfo.RedirectStandardOutput = true;
                        p2.StartInfo.RedirectStandardInput = true;
                        p2.StartInfo.FileName = @"C:\Windows\System32\psinfo.exe";
                        p2.StartInfo.Arguments = @"\\" + row["serverip"].ToString() + " -u " + row["Serverid"].ToString() + " -p " + row["Serverpwd"].ToString();
                        //p.StartInfo.Arguments = @"\\192.168.79.135 -u administrator -p P@ssw0rd";

                        p2.Start();
                        string os1 = p2.StandardOutput.ReadToEnd();
                        string[] os2 = os1.Split('\n');
                        string[] OS = os2[7].Substring(15).Trim().Split(',');
                        Console.WriteLine(OS[0]);

                        CON.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = CON;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update service set os = @os where serverip = @serverip ";
                        cmd.Parameters.Add("@OS", SqlDbType.NVarChar, 100).Value = OS[0].ToString();
                        cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cmd = null;
                        CON.Close();
                    }
                    catch
                    {
                        //CON.Open();
                        //SqlCommand cmd = new SqlCommand();
                        //cmd.Connection = CON;
                        //cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.CommandText = "update service set status = @status where serverip = @serverip ";
                        //cmd.Parameters.Add("@status", SqlDbType.NVarChar, 100).Value = "Server Disconnect";
                        //cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                        //cmd.ExecuteNonQuery();
                        //cmd.Dispose();
                        //cmd = null;
                        //CON.Close();
                    }
                    p2.WaitForExit();
                    //Thread.Sleep(sleep);

                }
               

                //}
            }
            catch (Exception e)
            {
                
            }
            return;


        }

    }

    
}
