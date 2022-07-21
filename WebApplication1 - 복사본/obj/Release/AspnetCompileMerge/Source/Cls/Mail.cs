using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebApplication1.Cls
{
    public class Mail
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";

        public void cpu_sendmail(string mailip, string serverip, double cpu, double cpulimit)
        {
            SQL = "select b.no ,  a.email  from user_ba a , service b ,  mail_info c where c.flag=1 and DATEADD(minute, 10, b.cpumail) < GETDATE() " +
                  "and serverip = '" + serverip + "'";
            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                try
                {
                    //Response.Write("TEST");

                    MailMessage MAIL = new MailMessage();
                    SmtpClient SMTPMAIL = new SmtpClient(mailip);
                    MAIL.From = new MailAddress("noreply@sungsimit.co.kr");
                    SMTPMAIL.Port = 25;
                    //MAIL.To.Add(Recever.Text.ToString());
                    MAIL.To.Add(row["email"].ToString());
                    //if (CC.Text.ToString() != "")
                    //{
                    //    MAIL.CC.Add(CC.Text.ToString());
                    //}

                    MAIL.Subject = "CPU 알림";
                    MAIL.Body = serverip + " 서버 현재 CPU 알림 기준치인 " + cpulimit + " 넘어 " + cpu + " 입니다. ";
                    MAIL.BodyEncoding = System.Text.Encoding.UTF8;
                    MAIL.SubjectEncoding = System.Text.Encoding.UTF8;


                    SMTPMAIL.Send(MAIL);

                    DB.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DB;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "update service set cpumail = getdate() where serverip = @serverip";
                    cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 50).Value = serverip;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;
                    DB.Close();
                    DB.Dispose();

                }
                catch
                {

                }
            }
        }

        public void memory_sendmail(string mailip, string serverip, double memory, double memorylimit)
        {
            SQL = "select b.no ,  a.email  from user_ba a , service b ,  mail_info c where c.flag=1 and DATEADD(minute, 10, b.memorymail) < GETDATE() " +
                "and serverip = '" + serverip + "'";
            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                try
                {
                    //Response.Write("TEST");

                    MailMessage MAIL = new MailMessage();
                    SmtpClient SMTPMAIL = new SmtpClient(mailip);
                    MAIL.From = new MailAddress("noreply@sungsimit.co.kr");
                    SMTPMAIL.Port = 25;
                    //MAIL.To.Add(Recever.Text.ToString());
                    MAIL.To.Add(row["email"].ToString());
                    //if (CC.Text.ToString() != "")
                    //{
                    //    MAIL.CC.Add(CC.Text.ToString());
                    //}

                    MAIL.Subject = "메모리 알림";
                    MAIL.Body = serverip + " 서버 현재 메모리 알림 기준치인 " + memorylimit + " 넘어 " + memory + " 입니다. ";
                    MAIL.BodyEncoding = System.Text.Encoding.UTF8;
                    MAIL.SubjectEncoding = System.Text.Encoding.UTF8;


                    SMTPMAIL.Send(MAIL);

                    DB.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DB;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "update service set memorymail = getdate() where serverip = @serverip";
                    cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 50).Value = serverip;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;
                    DB.Close();
                    DB.Dispose();

                }
                catch
                {

                }
            }
        }

        public void Traffic_sendmail(string mailip, string serverip, double traffic, double trafficlimit)
        {
            SQL = "select b.no ,  a.email  from user_ba a , service b ,  mail_info c where c.flag=1 and DATEADD(minute, 10, b.trafficmail) < GETDATE() " +
                "and serverip = '"+ serverip +"'";
            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                try
                {
                    //Response.Write("TEST");

                    MailMessage MAIL = new MailMessage();
                    SmtpClient SMTPMAIL = new SmtpClient(mailip);
                    MAIL.From = new MailAddress("noreply@sungsimit.co.kr");
                    SMTPMAIL.Port = 25;
                    //MAIL.To.Add(Recever.Text.ToString());
                    MAIL.To.Add(row["email"].ToString());
                    //if (CC.Text.ToString() != "")
                    //{
                    //    MAIL.CC.Add(CC.Text.ToString());
                    //}

                    MAIL.Subject = "트래픽알림";
                    MAIL.Body = serverip + " 서버 현재 트래픽 알림 기준치인 " + trafficlimit + " 넘어 " + traffic + " 입니다. ";
                    MAIL.BodyEncoding = System.Text.Encoding.UTF8;
                    MAIL.SubjectEncoding = System.Text.Encoding.UTF8;


                    SMTPMAIL.Send(MAIL);

                    DB.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DB;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "update service set trafficmail = getdate() where serverip = @serverip";
                    cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 50).Value = serverip;
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
                    //        SqlCommand cmd = new SqlCommand();
                    //        cmd.Connection = DB;
                    //        SqlDataAdapter ADT = new SqlDataAdapter();
                    //        DataSet DBSET = new DataSet();

                    //        cmd.Parameters.Add("@receiver", SqlDbType.NVarChar, 100).Value = Recever.Text;
                    //        cmd.Parameters.Add("@cc", SqlDbType.NVarChar, 100).Value = CC.Text;
                    //        cmd.Parameters.Add("@file_name", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;
                    //        cmd.Parameters.Add("@title", SqlDbType.NVarChar, 100).Value = TITLE.Text;
                    //        cmd.Parameters.Add("@body", SqlDbType.NVarChar, 1000).Value = BODY.Text;



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