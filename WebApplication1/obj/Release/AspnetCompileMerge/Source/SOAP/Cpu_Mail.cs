using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace WebApplication1.SOAP
{
    class Cpu_Mail
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        public void cpu_sendmail(string serverip, string nowcpu)
        {

         
            string SQL = "";
            SQL = "select distinct   a.email, a.mailno , c.mailip, c.cpulimit, mail_sender from mail_target a , service b ,  mail_info c where  c.flag=1 and DATE_ADD( b.cpumail, INTERVAL 10 MINUTE) < now() and a.serverip = b.ServerIP " +
                " and b.serverip = '" + serverip + "'";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                if (row["email"].ToString() != "")
                {
                    try
                    {
                        //Response.Write("TEST");

                        MailMessage MAIL = new MailMessage();
                        SmtpClient SMTPMAIL = new SmtpClient(row["mailip"].ToString());
                        MAIL.From = new MailAddress(row["mail_sender"].ToString());
                        SMTPMAIL.Port = 25;
                        //MAIL.To.Add(Recever.Text.ToString());
                        MAIL.To.Add(row["email"].ToString());
                        //if (CC.Text.ToString() != "")
                        //{
                        //    MAIL.CC.Add(CC.Text.ToString());
                        //}

                        MAIL.Subject = "Cpu 알림";
                        MAIL.Body = serverip + " 서버 현재 Cpu 알림 기준치인 " + row["cpulimit"].ToString() + " 넘어 " + nowcpu + " 입니다. ";
                        MAIL.BodyEncoding = System.Text.Encoding.UTF8;
                        MAIL.SubjectEncoding = System.Text.Encoding.UTF8;


                        SMTPMAIL.Send(MAIL);

                        DB.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = DB;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update service set cpumail = now() where serverip = @serverip";
                        cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 50).Value = serverip;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cmd = null;
                        DB.Close();
                        DB.Dispose();


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                
            }

        }
    }
}
