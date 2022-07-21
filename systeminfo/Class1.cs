
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace systeminfo
{
    public class Class1
    {
        /// <summary>
        /// 라이센스 갯수
        /// </summary>
        int sleep = 10000;
        
        public void systeminfo_thread()
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
                    Parallel.For(0, count, i =>
                    {
                        systeminfo(serverip[i]);

                    });
                    //for (int i = 0; i < count; i++)
                    //{

                    //    thread[i] = new Thread(delegate ()
                    //    {
                    //        systeminfo(serverip[i]);
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
                Console.WriteLine(e.Message);
            }

        }
        //public void computer_name(string serverip)
        //{
        //    DBCON.Class1 DBCON = new DBCON.Class1();
        //    SqlConnection CON = new SqlConnection(DBCON.DBCON);
        //    string SQL = "";

        //    while (true)
        //    {
        //        SQL = "select distinct os, serverip, serverid, serverpwd from service a  where flag = '1' and status = 'Server Connect' and category <> N'네트워크/보안 장비'" +
        //            " and serverip = '"+serverip +"'";
        //        SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
        //        DataSet DBSET = new DataSet();
        //        ADT.Fill(DBSET, "BD");

        //        foreach (DataRow row in DBSET.Tables["BD"].Rows)
        //        {
        //            Ping ping = new Ping();
        //            PingOptions options = new PingOptions();
        //            options.DontFragment = true;
        //            string data = "aaaaaaaaaaaaaaaaa";
        //            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
        //            int timeout = 120;
        //            PingReply reply = ping.Send(row["serverip"].ToString(), timeout, buffer, options);
        //            if (reply.Status == IPStatus.Success) // 네트워크 사용 가능할 때~~
        //            {
        //                ///윈도우
        //                ///
        //                if (row["os"].ToString().Contains("Window") == true)
        //                {
        //                    try
        //                    {
        //                        ConnectionOptions con = new ConnectionOptions();
        //                        con.Username = row["serverid"].ToString();
        //                        con.Password = row["serverpwd"].ToString();
        //                        ManagementScope servercon = new ManagementScope(@"\\" + row["serverip"].ToString() + @"\root\cimv2", con);

        //                        servercon.Connect();

        //                        ObjectQuery computer_name = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
        //                        ManagementObjectSearcher computer_name2 = new ManagementObjectSearcher(servercon, computer_name);
        //                        // Loop through the drives retrieved, although it should normally be only one loop going on here
        //                        ManagementObjectCollection loResult = computer_name2.Get();

        //                        foreach (ManagementObject incomputer_name in loResult)
        //                        {
        //                            Console.WriteLine(row["serverip"].ToString() + incomputer_name["name"].ToString());

        //                            CON.Open();
        //                            SqlCommand cmd = new SqlCommand();
        //                            cmd.Connection = CON;
        //                            cmd.CommandType = System.Data.CommandType.Text;
        //                            cmd.CommandText = "update service set computer_name = @computer_name where serverip = @serverip ";
        //                            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
        //                            cmd.Parameters.Add("@computer_name", SqlDbType.NVarChar, 100).Value = incomputer_name["name"].ToString();
        //                            cmd.ExecuteNonQuery();
        //                            cmd.Dispose();
        //                            cmd = null;
        //                            CON.Close();
        //                        }
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e.Message);
        //                        CON.Open();
        //                        SqlCommand cmd = new SqlCommand();
        //                        cmd.Connection = CON;
        //                        cmd.CommandType = System.Data.CommandType.Text;
        //                        cmd.CommandText = "update service set computer_name = @computer_name where serverip = @serverip ";
        //                        cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
        //                        cmd.Parameters.Add("@computer_name", SqlDbType.NVarChar, 100).Value = "No Access";
        //                        cmd.ExecuteNonQuery();
        //                        cmd.Dispose();
        //                        cmd = null;
        //                        CON.Close();
        //                    }

        //                }

                     
        //            }
        //        }
        //        Thread.Sleep(sleep);
        //    }
        //}
        //public void APPinfo()
        //{
        //    DBCON.Class1 DBCON = new DBCON.Class1();
        //    SqlConnection CON = new SqlConnection(DBCON.DBCON);
        //    string SQL = "";

        //    while (true)
        //    {
        //        SQL = "select distinct os, serverip, serverid, serverpwd from service where flag = '1' and status = 'Server Connect'and category <> '3'";
        //        SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
        //        DataSet DBSET = new DataSet();
        //        ADT.Fill(DBSET, "BD");

        //        foreach (DataRow row in DBSET.Tables["BD"].Rows)
        //        {
        //            Ping ping = new Ping();
        //            PingOptions options = new PingOptions();
        //            options.DontFragment = true;
        //            string data = "aaaaaaaaaaaaaaaaa";
        //            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
        //            int timeout = 120;
        //            PingReply reply = ping.Send(row["serverip"].ToString(), timeout, buffer, options);
        //            if (reply.Status == IPStatus.Success) // 네트워크 사용 가능할 때~~
        //            {
        //                ///윈도우
        //                ///
        //                if (row["os"].ToString().Contains("Window") == true)
        //                {
        //                    try
        //                    {
        //                        ConnectionOptions con = new ConnectionOptions();
        //                        con.Username = row["serverid"].ToString();
        //                        con.Password = row["serverpwd"].ToString();
        //                        ManagementScope servercon = new ManagementScope(@"\\" + row["serverip"].ToString() + @"\root\cimv2", con);

        //                        servercon.Connect();

        //                        string softwareRegLoc = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
        //                        ManagementClass registry = new ManagementClass(servercon, new ManagementPath("StdRegProv"), null);
        //                        ManagementBaseObject inParams = registry.GetMethodParameters("EnumKey");
        //                        inParams["hDefKey"] = 0x80000002;//HKEY_LOCAL_MACHINE
        //                        inParams["sSubKeyName"] = softwareRegLoc;

        //                        // Read Registry Key Names 
        //                        ManagementBaseObject outParams = registry.InvokeMethod("EnumKey", inParams, null);
        //                        string[] programGuids = outParams["sNames"] as string[];

        //                        foreach (string subKeyName in programGuids)
        //                        {
        //                            inParams = registry.GetMethodParameters("GetStringValue");
        //                            inParams["hDefKey"] = 0x80000002;//HKEY_LOCAL_MACHINE
        //                            inParams["sSubKeyName"] = softwareRegLoc + @"\" + subKeyName;
        //                            inParams["sValueName"] = "DisplayName";
        //                            // Read Registry Value 
        //                            outParams = registry.InvokeMethod("GetStringValue", inParams, null);
        //                            if (outParams.Properties["sValue"].Value != null)
        //                            {
                                       
        //                                string softwareName = outParams.Properties["sValue"].Value.ToString();
        //                                CON.Open();
        //                                SqlCommand cmd = new SqlCommand("Auto_add_app", CON);
        //                                cmd.CommandType = CommandType.StoredProcedure;
        //                                cmd.Parameters.AddWithValue("@serverip", row["serverip"].ToString());
        //                                cmd.Parameters.AddWithValue("@App", softwareName.ToString());
        //                                cmd.ExecuteNonQuery();
        //                                CON.Close();
        //                                //Console.WriteLine(softwareName);
        //                            }
        //                        }
        //                        Console.WriteLine(row["serverip"].ToString() + "APP IN");
                             
                               

        //                        string softwareRegLoc2 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

        //                        inParams["hDefKey"] = 0x80000002;//HKEY_LOCAL_MACHINE
        //                        inParams["sSubKeyName"] = softwareRegLoc2;

        //                        // Read Registry Key Names 
        //                        ManagementBaseObject outParams2 = registry.InvokeMethod("EnumKey", inParams, null);
        //                        string[] programGuids2 = outParams2["sNames"] as string[];

        //                        foreach (string subKeyName2 in programGuids2)
        //                        {
                                    
        //                            inParams = registry.GetMethodParameters("GetStringValue");
        //                            inParams["hDefKey"] = 0x80000002;//HKEY_LOCAL_MACHINE
        //                            inParams["sSubKeyName"] = softwareRegLoc2 + @"\" + subKeyName2;
        //                            inParams["sValueName"] = "DisplayName";
        //                            // Read Registry Value 
        //                            outParams2 = registry.InvokeMethod("GetStringValue", inParams, null);
        //                            if (outParams2.Properties["sValue"].Value != null)
        //                            {
        //                                string softwareName = outParams2.Properties["sValue"].Value.ToString();
        //                                //Console.WriteLine(softwareName);
        //                                CON.Open();
        //                                SqlCommand cmd = new SqlCommand("Auto_add_app", CON);
        //                                cmd.CommandType = CommandType.StoredProcedure;
        //                                cmd.Parameters.AddWithValue("@serverip", row["serverip"].ToString());
        //                                cmd.Parameters.AddWithValue("@App", softwareName.ToString());
        //                                cmd.ExecuteNonQuery();
        //                                CON.Close();
        //                            }
        //                        }
        //                        Console.WriteLine(row["serverip"].ToString() + "APP IN 64");


        //                        CON.Open();
        //                        SqlCommand cmd2 = new SqlCommand("Auto_add_app_i", CON);
        //                        cmd2.CommandType = CommandType.StoredProcedure;
        //                        cmd2.ExecuteNonQuery();
        //                        CON.Close();
        //                    }
        //                    catch(Exception e)
        //                    {
        //                        Console.WriteLine(e.Message);
        //                    }
                            
        //                }
        //            }
        //        }
        //        Thread.Sleep(sleep);
        //    }
        //}
        //public void HDinfo(string serverip)
        //{
        //    DBCON.Class1 DBCON = new DBCON.Class1();
        //    SqlConnection CON = new SqlConnection(DBCON.DBCON);
        //    string SQL = "";
        //    while (true)
        //    {

        //        SQL = "select distinct os, serverip, serverid, serverpwd, b.cpulimit , b.memorylimit from service a , mail_info b where a.flag = '1' and a.status = 'Server Connect' and category <> N'네트워크/보안 장비'" +
        //            " and serverip ='"+serverip+"'";
        //        SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
        //        DataSet DBSET = new DataSet();
        //        ADT.Fill(DBSET, "BD");

        //        foreach (DataRow row in DBSET.Tables["BD"].Rows)
        //        {
        //            Ping ping = new Ping();
        //            PingOptions options = new PingOptions();
        //            options.DontFragment = true;
        //            string data = "aaaaaaaaaaaaaaaaa";
        //            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
        //            int timeout = 120;
        //            PingReply reply = ping.Send(row["serverip"].ToString(), timeout, buffer, options);
        //            if (reply.Status == IPStatus.Success) // 네트워크 사용 가능할 때~~
        //            {
        //                if (row["os"].ToString().Contains("Window") == true)
        //                {
        //                    try
        //                    {
        //                        ConnectionOptions con = new ConnectionOptions();
        //                        con.Username = row["serverid"].ToString();
        //                        con.Password = row["serverpwd"].ToString();
        //                        ManagementScope servercon = new ManagementScope(@"\\" + row["serverip"].ToString() + @"\root\cimv2", con);
        //                        servercon.Connect();


        //                        ObjectQuery HDD = new ObjectQuery("SELECT * FROM Win32_LogicalDisk where drivetype=3");
        //                        ManagementObjectSearcher HDD2 = new ManagementObjectSearcher(servercon, HDD);
        //                        // Loop through the drives retrieved, although it should normally be only one loop going on here
        //                        ManagementObjectCollection loResult = HDD2.Get();
        //                        string Drives = "";
        //                        int iPercent = 0;
        //                        foreach (ManagementObject inHDD in loResult)
        //                        {
        //                            if (Convert.ToDecimal(inHDD["Size"]) > 0)
        //                            {
        //                                decimal Size = Convert.ToDecimal(inHDD["Size"]) / 1073741824;
        //                                decimal FreeSpace = Convert.ToDecimal(inHDD["FreeSpace"]) / 1073741824;
        //                                decimal usespace = Size - FreeSpace;
        //                                iPercent = Convert.ToInt32((usespace / Size) * 100);
        //                                string sDriveLetter = Convert.ToString(inHDD["Name"]);
        //                                Console.WriteLine(sDriveLetter + " " + iPercent + "%");

        //                                Drives += sDriveLetter + " " + iPercent + "%  ";
        //                            }
        //                        }
        //                        CON.Open();
        //                        SqlCommand cmd = new SqlCommand();
        //                        cmd.Connection = CON;
        //                        cmd.CommandType = System.Data.CommandType.Text;
        //                        cmd.CommandText = "update server_hd set hd = @hd where serverip = @serverip ";
        //                        cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
        //                        cmd.Parameters.Add("@hd", SqlDbType.NVarChar, 100).Value = Drives;
        //                        cmd.ExecuteNonQuery();
        //                        cmd.Dispose();
        //                        cmd = null;
        //                        CON.Close();
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e.Message);
        //                    }
        //                }
        //            }
        //        }
        //        Thread.Sleep(sleep);
        //    }
        //}
        public void systeminfo(string serverip)
        {
            try
            {
                DBCON.Class1 DBCON = new DBCON.Class1();
                SqlConnection CON = new SqlConnection(DBCON.DBCON);
                string SQL = "";
                //while (true)
                //{

                    SQL = "select distinct os, serverip, serverid, serverpwd, b.cpulimit , b.memorylimit , c.log_time from service a , mail_info b , log_time_config c where a.flag = '1' and a.status = 'Server Connect' and serverid is not null " +
                        " and serverip = '" + serverip + "'";

                    SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
                    DataSet DBSET = new DataSet();
                    ADT.Fill(DBSET, "BD");
                    foreach (DataRow row in DBSET.Tables["BD"].Rows)
                    {
                        if(row["serverid"].ToString() != "")
                        {
                       
                            string CO = "";
                            Console.WriteLine(row["serverip"].ToString() + " OS 검사");
                            
                            try
                            {
                                ConnectionOptions con = new ConnectionOptions();
                                con.Username = row["serverid"].ToString();
                                con.Password = row["serverpwd"].ToString();
                                ManagementScope servercon = new ManagementScope(@"\\" + row["serverip"].ToString() + @"\root\cimv2", con);

                                servercon.Connect();

                                ObjectQuery computer_name = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                                ManagementObjectSearcher computer_name2 = new ManagementObjectSearcher(servercon, computer_name);
                                // Loop through the drives retrieved, although it should normally be only one loop going on here
                                ManagementObjectCollection loResult = computer_name2.Get();

                                foreach (ManagementObject incomputer_name in loResult)
                                {
                                    Console.WriteLine(row["serverip"].ToString() + incomputer_name["Caption"].ToString());

                                    CON.Open();
                                    SqlCommand cmd = new SqlCommand();
                                    cmd.Connection = CON;
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.CommandText = "update service set os = @os where serverip = @serverip ";
                                    cmd.Parameters.Add("@OS", SqlDbType.NVarChar, 100).Value = incomputer_name["Caption"].ToString();
                                    cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;
                                    CON.Close();
                                }
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
                           


                            ///윈도우
                            ///
                            if (row["os"].ToString().Contains("Window") == true)
                            {
                                try
                                {
                                    ConnectionOptions con = new ConnectionOptions();
                                    con.Username = row["serverid"].ToString();
                                    con.Password = row["serverpwd"].ToString();
                                    ManagementScope servercon = new ManagementScope(@"\\" + row["serverip"].ToString() + @"\root\cimv2", con);

                                    servercon.Connect();

                                    ObjectQuery computer_name = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
                                    ManagementObjectSearcher computer_name2 = new ManagementObjectSearcher(servercon, computer_name);
                                    // Loop through the drives retrieved, although it should normally be only one loop going on here
                                    ManagementObjectCollection loResult = computer_name2.Get();

                                    foreach (ManagementObject incomputer_name in loResult)
                                    {
                                        Console.WriteLine(row["serverip"].ToString() + incomputer_name["name"].ToString());

                                        CON.Open();
                                        SqlCommand cmd = new SqlCommand();
                                        cmd.Connection = CON;
                                        cmd.CommandType = System.Data.CommandType.Text;
                                        cmd.CommandText = "update service set computer_name = @computer_name where serverip = @serverip ";
                                        cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                        cmd.Parameters.Add("@computer_name", SqlDbType.NVarChar, 100).Value = incomputer_name["name"].ToString();
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                        cmd = null;
                                        CON.Close();
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    CON.Open();
                                    SqlCommand cmd = new SqlCommand();
                                    cmd.Connection = CON;
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.CommandText = "update service set computer_name = @computer_name where serverip = @serverip ";
                                    cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                    cmd.Parameters.Add("@computer_name", SqlDbType.NVarChar, 100).Value = "No Access";
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;
                                    CON.Close();
                                }

                                try
                                {
                                    ConnectionOptions con = new ConnectionOptions();
                                    con.Username = row["serverid"].ToString();
                                    con.Password = row["serverpwd"].ToString();
                                    ManagementScope servercon = new ManagementScope(@"\\" + row["serverip"].ToString() + @"\root\cimv2", con);
                                    servercon.Connect();


                                    ObjectQuery getcpu = new ObjectQuery("SELECT * FROM Win32_Processor");
                                    ManagementObjectSearcher getcpu2 = new ManagementObjectSearcher(servercon, getcpu);
                                    // Loop through the drives retrieved, although it should normally be only one loop going on here
                                    ManagementObjectCollection loResult = getcpu2.Get();


                                    int i = 0;
                                    int sumcpu = 0;
                                    foreach (ManagementObject incpu in loResult)
                                    {
                                        i++;
                                        sumcpu += Convert.ToInt32(incpu["LoadPercentage"]);

                                        // Set all the fields to the appropriate values
                                        //Console.WriteLine("CPU: " + Convert.ToDecimal(incpu["LoadPercentage"]));

                                    }
                                    Console.WriteLine("CPU: " + sumcpu / i);


                                    if (Convert.ToInt32(row["cpulimit"]) < sumcpu / i && Convert.ToInt32(row["cpulimit"]) > 0)
                                    {
                                        Cpu_Mail mail = new Cpu_Mail();
                                        mail.cpu_sendmail(row["serverip"].ToString(), (sumcpu / i).ToString());
                                    }
                                    CON.Open();
                                    SqlCommand cmd = new SqlCommand();
                                    cmd.Connection = CON;
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.CommandText = "update service set cpu = @cpu  where serverip = @serverip ";
                                    cmd.Parameters.Add("@cpu", SqlDbType.NVarChar, 100).Value = sumcpu / i;
                                    cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    cmd = null;
                                    CON.Close();


                                    //메모리
                                    double real = 0;
                                    ObjectQuery getmemory = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                                    ManagementObjectSearcher getmemory2 = new ManagementObjectSearcher(servercon, getmemory);
                                    ManagementObjectCollection loResult2 = getmemory2.Get();
                                    foreach (ManagementObject inmemory in loResult2)
                                    {
                                        double free = Double.Parse(inmemory["FreePhysicalMemory"].ToString());
                                        double total = Double.Parse(inmemory["TotalVisibleMemorySize"].ToString());

                                        real = (total - free) / total * 100;
                                        Console.WriteLine("Memory: " + real.ToString("N1"));

                                        if (Convert.ToInt32(row["memorylimit"]) < Convert.ToInt32(real) && Convert.ToInt32(row["memorylimit"]) > 0)
                                        {

                                            Memory_Mail mail = new Memory_Mail();
                                            mail.memory_sendmail(row["serverip"].ToString(), real.ToString("N1"));
                                        }
                                        CON.Open();
                                        SqlCommand cmd3 = new SqlCommand();
                                        cmd3.Connection = CON;

                                        cmd3.CommandType = System.Data.CommandType.Text;
                                        cmd3.CommandText = "update service set  memory = @memory where serverip = @serverip ";
                                        cmd3.Parameters.Add("@memory", SqlDbType.NVarChar, 100).Value = real.ToString("N1");
                                        cmd3.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                        cmd3.ExecuteNonQuery();
                                        cmd3.Dispose();
                                        cmd3 = null;
                                        CON.Close();
                                    }
                                    int iPercent = 0;
                                    string Drives = "";
                                    string Drives2 = "";
                                    ///HD
                                    try
                                    {
                                        ObjectQuery HDD = new ObjectQuery("SELECT * FROM Win32_LogicalDisk where drivetype=3");
                                        ManagementObjectSearcher HDD2 = new ManagementObjectSearcher(servercon, HDD);
                                        // Loop through the drives retrieved, although it should normally be only one loop going on here
                                        ManagementObjectCollection loResult3 = HDD2.Get();
                                        foreach (ManagementObject inHDD in loResult3)
                                        {
                                            if (Convert.ToDecimal(inHDD["Size"]) > 0)
                                            {
                                                decimal Size = Convert.ToDecimal(inHDD["Size"]) / 1073741824;
                                                decimal FreeSpace = Convert.ToDecimal(inHDD["FreeSpace"]) / 1073741824;
                                                decimal usespace = Size - FreeSpace;
                                                iPercent = Convert.ToInt32((usespace / Size) * 100);
                                                string sDriveLetter = Convert.ToString(inHDD["Name"]);
                                                Console.WriteLine(sDriveLetter + " " + iPercent + "%");

                                                Drives += sDriveLetter + " " + iPercent + "%  ";

                                            }
                                        }
                                        CON.Open();
                                        SqlCommand cmd4 = new SqlCommand();
                                        cmd4.Connection = CON;
                                        cmd4.CommandType = System.Data.CommandType.Text;
                                        cmd4.CommandText = "update server_hd set hd = @hd where serverip = @serverip ";
                                        cmd4.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                        cmd4.Parameters.Add("@hd", SqlDbType.NVarChar, 100).Value = Drives;
                                        cmd4.ExecuteNonQuery();
                                        cmd4.Dispose();
                                        cmd4 = null;
                                        CON.Close();
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }

                                    string state = "";
                                    if (row["log_time"].ToString() == "1")//1시간
                                    {
                                        string SQL2 = "select top 1 SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2)  as time from system_log_cpu_memory where serverip = '" + row["serverip"].ToString() + "' order by no desc ";
                                        SqlDataAdapter ADT2 = new SqlDataAdapter(SQL2, CON);
                                        DataSet DBSET2 = new DataSet();
                                        ADT2.Fill(DBSET2, "BD2");

                                        foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                                        {
                                            string time1 = DateTime.Now.ToString("HH");
                                            string time2 = DateTime.Now.ToString(row2["time"].ToString());
                                            time2 = Regex.Replace(time2, " ", ":");
                                            //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                                            if (time1 != time2)
                                            {
                                                CON.Open();
                                                SqlCommand cmd3 = new SqlCommand("Add_System_Log_Cpu_Memory", CON);
                                                cmd3.CommandType = CommandType.StoredProcedure;
                                                cmd3.Parameters.AddWithValue("@serverip", row["serverip"].ToString());
                                                cmd3.ExecuteNonQuery();
                                                CON.Close();

                                                CON.Open();
                                                SqlCommand cmd4 = new SqlCommand("Add_System_Log_Traffic", CON);
                                                cmd4.CommandType = CommandType.StoredProcedure;
                                                cmd4.Parameters.AddWithValue("@serverip", row["serverip"].ToString());
                                                cmd4.ExecuteNonQuery();
                                                CON.Close();
                                            }
                                            state = "in";
                                        }
                                    }
                                    if (row["log_time"].ToString() == "2")//30분
                                    {
                                        string SQL2 = "select top 1 SUBSTRING(Convert(nvarchar,dateadd(mi, 30,time),121),0,17)  as time from system_log_cpu_memory where serverip = '" + row["serverip"].ToString() + "' order by no desc ";
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
                                                CON.Open();
                                                SqlCommand cmd3 = new SqlCommand("Add_System_Log_Cpu_Memory", CON);
                                                cmd3.CommandType = CommandType.StoredProcedure;
                                                cmd3.Parameters.AddWithValue("@serverip", row["serverip"].ToString());
                                                cmd3.ExecuteNonQuery();
                                                CON.Close();

                                                CON.Open();
                                                SqlCommand cmd4 = new SqlCommand("Add_System_Log_Traffic", CON);
                                                cmd4.CommandType = CommandType.StoredProcedure;
                                                cmd4.Parameters.AddWithValue("@serverip", row["serverip"].ToString());
                                                cmd4.ExecuteNonQuery();
                                                CON.Close();
                                            }
                                            state = "in";
                                        }
                                    }

                                    if (state == "")
                                    {
                                        CON.Open();
                                        SqlCommand cmd3 = new SqlCommand();
                                        cmd3.Connection = CON;
                                        cmd3.CommandType = System.Data.CommandType.Text;
                                        cmd3.CommandText = "insert into system_log_cpu_memory (serverip,cpu,memory,hd) values(@serverip,@cpu,@memory,@hd) ";
                                        cmd3.Parameters.Add("@cpu", SqlDbType.NVarChar, 100).Value = sumcpu / i;
                                        cmd3.Parameters.Add("@memory", SqlDbType.NVarChar, 100).Value = real.ToString("N1");
                                        cmd3.Parameters.Add("@hd", SqlDbType.NVarChar, 100).Value = Drives;
                                        cmd3.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                        cmd3.ExecuteNonQuery();
                                        cmd3.Dispose();
                                        cmd3 = null;
                                        CON.Close();
                                    }
                                    CON.Open();
                                    SqlCommand cmd2 = new SqlCommand();
                                    cmd2.Connection = CON;
                                    cmd2.CommandType = System.Data.CommandType.Text;
                                    cmd2.CommandText = "insert into temp_system_log_cpu_memory (serverip,cpu,memory,hd) values(@serverip,@cpu,@memory,@hd) ";
                                    cmd2.Parameters.Add("@cpu", SqlDbType.NVarChar, 100).Value = sumcpu / i;
                                    cmd2.Parameters.Add("@memory", SqlDbType.NVarChar, 100).Value = real.ToString("N1");
                                    cmd2.Parameters.Add("@hd", SqlDbType.NVarChar, 100).Value = Drives;
                                    cmd2.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = row["serverip"].ToString();
                                    cmd2.ExecuteNonQuery();
                                    cmd2.Dispose();
                                    cmd2 = null;
                                    CON.Close();




                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }

                        
                    }
                        

                    }
                    //Thread.Sleep(sleep);
                //}
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

        }
    }
}
