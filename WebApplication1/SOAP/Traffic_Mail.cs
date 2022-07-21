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
    class Traffic_Mail
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        public void Traffic_sendmail( string serverip, string nowtraffic)
        {
          
            string SQL = "";
            SQL = "select distinct a.email, a.mailno , c.mailip, c.trafficlimit, mail_sender from mail_target a , service b ,  mail_info c where c.flag=1 and DATE_ADD( b.trafficmail, INTERVAL 10 MINUTE) < now() and a.serverip = b.ServerIP " +
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

                        MAIL.Subject = "트래픽알림";
                        MAIL.Body = serverip + " 서버 현재 트래픽 알림 기준치인 " + row["trafficlimit"].ToString() + " 넘어 " + nowtraffic + " 입니다. ";
                        MAIL.BodyEncoding = System.Text.Encoding.UTF8;
                        MAIL.SubjectEncoding = System.Text.Encoding.UTF8;


                        SMTPMAIL.Send(MAIL);

                        DB.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = DB;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update service set trafficmail = now() where serverip = @serverip";
                        cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 50).Value = serverip;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cmd = null;
                        DB.Close();
                        DB.Dispose();


                        //try
                        //{
                        //    DB.Open();
                        //    try
                        //    {
                        //        MySqlCommand cmd = new MySqlCommand();
                        //        cmd.Connection = DB;
                        //        MySqlDataAdapter ADT = new MySqlDataAdapter();
                        //        DataSet DBSET = new DataSet();

                        //        cmd.Parameters.Add("@receiver", MySqlDbType.VarChar, 100).Value = Recever.Text;
                        //        cmd.Parameters.Add("@cc", MySqlDbType.VarChar, 100).Value = CC.Text;
                        //        cmd.Parameters.Add("@file_name", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;
                        //        cmd.Parameters.Add("@title", MySqlDbType.VarChar, 100).Value = TITLE.Text;
                        //        cmd.Parameters.Add("@body", MySqlDbType.VarChar, 1000).Value = BODY.Text;



                        //        cmd.CommandType = System.Data.CommandType.Text;
                        //        cmd.CommandText = "insert into send_mail (receiver, cc, file_name, title ,body) " +
                        //        "values(@reciver,@cc,@file_name,@title,@body)";

                        //        cmd.ExecuteNonQuery();
                        //        cmd.Dispose();
                        //        cmd = null;

                        //    }
                        //    catch
                        //    {
                        //        Label3.Text = "<script>alert('인설트 실패');</script>";
                        //    }

                        //}
                        //catch
                        //{
                        //    Label3.Text = "<script>alert('디비오픈 실패');</script>";
                        //}


                        //Label3.Text = "<script>alert('메일 발송 완료');</script>";
                    }
                    catch
                    {
                        //Label3.Text = "<script>alert('에러!');</script>";
                    }
                }
                
            }

        }


    }
}
