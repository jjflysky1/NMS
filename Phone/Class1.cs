using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace Phone
{
    public class Class1
    {
        public void phone_thread()
        {
            DBCON.Class1 DBCON = new DBCON.Class1();
            SqlConnection CON = new SqlConnection(DBCON.DBCON);
            string SQL = "";
            int count = 0;
            SQL = "select  count(distinct serverip) as count from service";
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
            SQL1 = "select distinct serverip  from service ";
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
                //Console.WriteLine(serverip[i]);
                thread[i] = new Thread(delegate ()
                {
                   // Phone(serverip[i]);
                });
                thread[i].Start();
                Thread.Sleep(1000);
            }
        }

        public void Phone()
        {
            DBCON.Class1 DBCON = new DBCON.Class1();
            SqlConnection CON = new SqlConnection(DBCON.DBCON);
            string SQL = "";
            while (true)
            {
                SQL = "select distinct serverip from service where flag = '1' and os is null  ";
                SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");
                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {

                    try
                    {
                        Ping ping = new Ping();
                        PingOptions options = new PingOptions();
                        options.DontFragment = true;
                        string data = "aaaaaaaaaaaaaaaaa";
                        byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
                        int timeout = 200;
                        PingReply reply = ping.Send(row["serverip"].ToString(), timeout, buffer, options);
                        if (reply.Status == IPStatus.Success) // 네트워크 사용 가능할 때~~
                        {
                            Process p2 = new Process();
                            p2.StartInfo.UseShellExecute = false;
                            p2.StartInfo.RedirectStandardOutput = true;
                            p2.StartInfo.RedirectStandardInput = true;
                            p2.StartInfo.FileName = @"C:\Program Files (x86)\Nmap\nmap.exe";
                            p2.StartInfo.Arguments = @" -p 62078 " + row["serverip"].ToString();

                            p2.Start();

                            //Console.WriteLine(p2.StandardOutput.ReadToEnd());
                            string[] phone = { };
                            phone = p2.StandardOutput.ReadToEnd().Split('\n');
                            //Console.WriteLine(phone[7]);

                            if (phone[7].ToString().Contains("Apple") == true)
                            {
                                Console.WriteLine(row["serverip"].ToString() + " Apple");
                            }
                            if (phone[7].ToString().Contains("Samsung") == true)
                            {
                                Console.WriteLine(row["serverip"].ToString() + " Samsung");
                            }
                            p2.WaitForExit();

                        }
                       

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                Thread.Sleep(10000);
            }
        }
    }
}
