using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace WebApplication1.SOAP
{
    /// <summary>
    /// Systeminfo의 요약 설명입니다.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
    // [System.Web.Script.Services.ScriptService]
    public class Systeminfo : System.Web.Services.WebService
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //[WebMethod]
        //public DataSet LOGIN(string Computername, string IP)
        //{

        //        string SQL = "";
        //        SQL = "select * from Service where ServerIP = '192.168.0.104' ";


        //        MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
        //        DataSet DBSET = new DataSet();
        //        ADT.Fill(DBSET, "BD");
        //        return DBSET;

        //}
        [WebMethod]
        public string LOGIN(string user_id, string user_pwd, string ip)
        {


            string SQL = "select * from user_ba where id = @id and pwd = @pwd and flag = 1";

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.SelectCommand.Parameters.Add("@id", MySqlDbType.VarChar, 50).Value = user_id;
            ADT.SelectCommand.Parameters.Add("@pwd", MySqlDbType.VarChar, 50).Value = user_pwd;

            try
            {
                ADT.Fill(DBSET);
                if (DBSET.Tables[0].Rows.Count > 0)
                {
                    DB.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = DB;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "insert into login_log_list (Clientip, id, time) values(@Clientip, @id, now())";
                    cmd.Parameters.Add("@id", MySqlDbType.VarChar, 100).Value = user_id;
                    cmd.Parameters.Add("@Clientip", MySqlDbType.VarChar, 100).Value = ip;
                    cmd.ExecuteNonQuery();
                    DB.Close();
                    cmd.Dispose();
                    cmd = null;

                    return "성공";
                }
                else
                {
                   
                }
                DBSET.Clear();
            }
            catch (Exception EX)
            {
               
            }
            DBSET.Dispose();
            ADT.Dispose();

            DBSET = null;
            ADT = null;




            return "실패";

        }



        [WebMethod]
        public string COMPUTERNAME(string Computername, string IP)
        {
            try
            {
                DB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DB;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "update service set computer_name = @computer_name where serverip = @serverip ";
                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = IP;
                cmd.Parameters.Add("@computer_name", MySqlDbType.VarChar, 100).Value = Computername;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                DB.Close();

                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [WebMethod]
        public string OS(string OS,string IP)
        {
            try
            {
                DB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DB;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "update service set os = @os where serverip = @serverip ";
                cmd.Parameters.Add("@OS", MySqlDbType.VarChar, 100).Value = OS;
                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = IP;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                DB.Close();

                return "OK";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        [WebMethod]
        public string CPU(string CPU, string IP)
        {
            try
            {
                DB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DB;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "update service set cpu = @cpu  where serverip = @serverip ";
                cmd.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = CPU;
                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = IP;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                DB.Close();

             

                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [WebMethod]
        public string MEMORY(string MEMORY, string IP)
        {
            try
            {
                DB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DB;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "update service set  memory = @memory where serverip = @serverip ";
                cmd.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = MEMORY;
                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = IP;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                DB.Close();



             


                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [WebMethod]
        public string DISK(string DISK, string IP)
        {
            try
            {
                DB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DB;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "update server_hd set hd = @hd where serverip = @serverip ";
                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = IP;
                cmd.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = DISK;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                DB.Close();

                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [WebMethod]
        public string Traffic(string Traffic, string IP)
        {
            try
            {
                DB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DB;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "update server_traffic set traffic = @traffic  where serverip = @serverip ";
                cmd.Parameters.Add("@traffic", MySqlDbType.LongText).Value = Traffic;
                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = IP;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                DB.Close();

                DB.Open();
                MySqlCommand cmd3 = new MySqlCommand();
                cmd3.Connection = DB;
                cmd3.CommandType = System.Data.CommandType.Text;
                cmd3.CommandText = "insert into temp_system_log_traffic (serverip,traffic) values(@serverip,@traffic) ";
                cmd3.Parameters.Add("@traffic", MySqlDbType.LongText).Value = Traffic;
                cmd3.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = IP;
                cmd3.ExecuteNonQuery();
                cmd3.Dispose();
                cmd3 = null;
                DB.Close();

                string SQL2 = "select count(*) as count from system_log_traffic where serverip = '" + IP + "'  ";
                MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL2, DB);
                DataSet DBSET2 = new DataSet();
                ADT2.Fill(DBSET2, "BD2");
                string state = "";
                foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                {
                    state = row2["count"].ToString();

                }
                if (state == "0")
                {
                    DB.Open();
                    MySqlCommand cmd2 = new MySqlCommand();
                    cmd2.Connection = DB;
                    cmd2.CommandType = System.Data.CommandType.Text;
                    cmd2.CommandText = "insert into system_log_traffic (serverip,traffic) values(@serverip,@traffic) ";
                    cmd2.Parameters.Add("@traffic", MySqlDbType.LongText).Value = Traffic;
                    cmd2.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = IP;
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
                    cmd2 = null;
                    DB.Close();
                }


           
                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [WebMethod]
        public string ALL(string CPU, string MEMORY , string DISK , string IP)
        {
            try
            {
                DB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DB;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into temp_system_log_cpu_memory (serverip,cpu,memory,hd) values(@serverip,@cpu,@memory,@hd) ";
                cmd.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = CPU;
                cmd.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = MEMORY;
                cmd.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = DISK;
                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = IP;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                DB.Close();

                string SQL2 = "select count(*) as count from system_log_cpu_memory where serverip = '" + IP + "'  ";
                MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL2, DB);
                DataSet DBSET2 = new DataSet();
                ADT2.Fill(DBSET2, "BD2");
                string state = "";
                foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                {
                    state = row2["count"].ToString();

                }
                if (state == "0")
                {
                    DB.Open();
                    MySqlCommand cmd2 = new MySqlCommand();
                    cmd2.Connection = DB;
                    cmd2.CommandType = System.Data.CommandType.Text;
                    cmd2.CommandText = "insert into system_log_cpu_memory (serverip,cpu,memory,hd) values(@serverip,@cpu,@memory,@hd) ";
                    cmd2.Parameters.Add("@cpu", MySqlDbType.VarChar, 100).Value = CPU;
                    cmd2.Parameters.Add("@memory", MySqlDbType.VarChar, 100).Value = MEMORY;
                    cmd2.Parameters.Add("@hd", MySqlDbType.VarChar, 100).Value = DISK;
                    cmd2.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = IP;
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
                    cmd2 = null;
                    DB.Close();
                }

                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        [WebMethod]
        //public string create_log(string serverip, string cpu, string memory, string traffic)
        public string create_log(string serverip, string cpu, string memory, string traffic)
        {
            try
            {
                //
                string SQL = "";
                SQL = "select distinct os, serverip, serverid, serverpwd, b.cpulimit , b.memorylimit, b.trafficlimit , c.log_time from service a , mail_info b , log_time_config c where a.flag = '1' and  a.status = 'Server Connect' " +
                          " and serverip = '" + serverip + "' ";

                MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");
                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {
                    /// CPU메일 보내기
                    if (Convert.ToInt32(row["cpulimit"]) < Convert.ToInt32(cpu) && Convert.ToInt32(row["cpulimit"]) > 0)
                    {
                        Cpu_Mail mail = new Cpu_Mail();
                        mail.cpu_sendmail(row["serverip"].ToString(), (cpu).ToString());
                    }
                    ///메모리메일 보내기
                    if (Convert.ToInt32(row["memorylimit"]) < Convert.ToDouble(memory) && Convert.ToInt32(row["memorylimit"]) > 0)
                    {
                        Memory_Mail mail = new Memory_Mail();
                        mail.memory_sendmail(row["serverip"].ToString(), memory.ToString());
                    }
                    ///트래픽메일 보내기
                    if (Convert.ToDouble(row["trafficlimit"]) < Convert.ToDouble(traffic) && Convert.ToDouble(row["trafficlimit"]) > 0)
                    {
                        decimal nowtraffic = (Convert.ToDecimal(traffic));
                        Traffic_Mail mail = new Traffic_Mail();
                        mail.Traffic_sendmail(row["serverip"].ToString(), traffic.ToString());
                    }

                    string state = "";
                    //if (row["log_time"].ToString() == "1")//1시간
                    //{
                    //    string SQL2 = "select  date_format(DATE_ADD(time, INTERVAL 30 MINUTE), '%Y-%c-%d %H:%i') as time from system_log_cpu_memory where serverip = '" + row["serverip"].ToString() + "' order by no desc limit 1";
                    //    MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL2, DB);
                    //    DataSet DBSET2 = new DataSet();
                    //    ADT2.Fill(DBSET2, "BD2");

                    //    foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                    //    {
                    //        string time1 = DateTime.Now.ToString("HH");
                    //        string time2 = DateTime.Now.ToString(row2["time"].ToString());
                    //        time2 = Regex.Replace(time2, " ", ":");
                    //        //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                    //        if (time1 != time2)
                    //        {
                    //            DB.Open();
                    //            MySqlCommand cmd3 = new MySqlCommand("Add_System_Log_Cpu_Memory", DB);
                    //            cmd3.CommandType = CommandType.StoredProcedure;
                    //            cmd3.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                    //            cmd3.ExecuteNonQuery();
                    //            DB.Close();

                    //            DB.Open();
                    //            MySqlCommand cmd4 = new MySqlCommand("Add_System_Log_Traffic", DB);
                    //            cmd4.CommandType = CommandType.StoredProcedure;
                    //            cmd4.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                    //            cmd4.ExecuteNonQuery();
                    //            DB.Close();
                    //        }
                    //        state = "in";
                    //    }

                    //}
                    if (row["log_time"].ToString() == "2")//30분
                    {
                        //string SQL2 = "select date_format(DATE_ADD(time, INTERVAL 30 MINUTE), '%Y-%c-%d %H:%i') as time from system_log_cpu_memory where serverip = '" + row["serverip"].ToString() + "' order by no desc limit 1";
                        //MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL2, DB);
                        //DataSet DBSET2 = new DataSet();
                        //ADT2.Fill(DBSET2, "BD2");

                        //foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
                        //{
                        //    string time1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                        //    string time2 = DateTime.Now.ToString(row2["time"].ToString());
                        //    //TimeSpan gap = (Convert.ToDateTime(time1) - Convert.ToDateTime(time2));
                        //    //time2 = Regex.Replace(time2, " ", ":");
                        //    if (Convert.ToDateTime(time1) > Convert.ToDateTime(time2))
                        //    {
                               
                        //    }
                        //    state = "in";
                        //}
                        DB.Open();
                        MySqlCommand cmd3 = new MySqlCommand("Add_System_Log_Cpu_Memory", DB);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                        cmd3.ExecuteNonQuery();
                        DB.Close();

                        DB.Open();
                        MySqlCommand cmd4 = new MySqlCommand("Add_System_Log_Traffic", DB);
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.AddWithValue("@serverip1", row["serverip"].ToString());
                        cmd4.ExecuteNonQuery();
                        DB.Close();

                    }

                }
                return "OK";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
            
        }

    }
}
