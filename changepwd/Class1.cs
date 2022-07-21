using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace changepwd
{
    public class Class1
    {
        
        public void changepwd()
        {
            DBCON.Class1 DBCON = new DBCON.Class1();
            
            //MySqlConnection CON = new MySqlConnection("Server =192.168.72.133; Database=cs; User id=sa; Password=P@ssw0rd");
            MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
            string SQL = "";

            while (true)
            {
                SQL = "select  no, os, serverip, serverid, serverpwd, newpwd, oripwd, sshport from service where category <> N'네트워크/보안 장비'";
                MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");

                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {
                    //윈도우
                    if (row["oripwd"].ToString() != row["newpwd"].ToString() && row["os"].ToString().Contains("Win") == true)
                    {
                        Process p = new Process();
                        try
                        {
                            Console.WriteLine(row["serverip"].ToString() + "PWD -Connecting...");
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.RedirectStandardOutput = true;
                            p.StartInfo.RedirectStandardInput = true;
                            p.StartInfo.FileName = @"C:\Windows\System32\pspasswd.exe";
                            p.StartInfo.Arguments = @"\\" + row["serverip"].ToString() + " -u " + row["Serverid"].ToString() + " -p test " + row["Serverid"].ToString() + " " + row["newpwd"].ToString() + "";
                            //p.StartInfo.Arguments = @"\\192.168.72.137 -u administrator -p P@ssw0rd administrator 'P@ssw0rd1'";

                            p.Start();

                            Console.WriteLine(p.StandardOutput.ReadToEnd());

                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = CON;
                            CON.Open();
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandText = "update service set serverpwd = @newpwd , newpwd = null , oripwd = null where serverip = @serverip and no = @no";
                            cmd.Parameters.Add("@newpwd", MySqlDbType.VarChar, 100).Value = row["newpwd"].ToString();
                            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                            cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = row["no"].ToString();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cmd = null;
                            CON.Close();

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            if (row["newpwd"].ToString().Length > 1)
                            {
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                CON.Open();
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set serverpwd = newpwd  , newpwd = null , oripwd = null where no = @no";
                                cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = row["no"].ToString();
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();
                            }
                        }
                        p.WaitForExit();
                        Thread.Sleep(1000);
                    }
                       

                    //리눅스
                    if (row["oripwd"].ToString() != row["newpwd"].ToString()  && row["os"].ToString().Contains("Win") != true)
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

                            Console.WriteLine(row["serverip"].ToString() + "PWD -Connecting...");
                            SshClient ssh = new SshClient(row["serverip"].ToString(),sshport, row["serverid"].ToString(), row["oripwd"].ToString());
                            ssh.Connect();
                            var sshcmd = ssh.RunCommand("echo '" + row["serverid"].ToString() +":"+ row["newpwd"].ToString() + "' | chpasswd");
                            //var sshcmd = ssh.RunCommand("echo '" + row["newpwd"].ToString() + "' | passwd --stdin root");
                            sshcmd.Execute();


                            Console.WriteLine(sshcmd.Result);

                            Console.WriteLine("변경되었습니다.");
                            ssh.Disconnect();

                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = CON;
                            CON.Open();
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandText = "update service set serverpwd = @newpwd , newpwd = null , oripwd = null where serverip = @serverip and no = @no";
                            cmd.Parameters.Add("@newpwd", MySqlDbType.VarChar, 100).Value = row["newpwd"].ToString();
                            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = row["serverip"].ToString();
                            cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = row["no"].ToString();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            cmd = null;
                            CON.Close();


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            if(row["newpwd"].ToString().Length > 1)
                            {
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                CON.Open();
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update service set serverpwd = newpwd , newpwd = null , oripwd = null  where no = @no";
                                cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = row["no"].ToString();
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();
                            }

                        }
                  
                }
                Thread.Sleep(1000);
            }
           
                
        }
    }
}
