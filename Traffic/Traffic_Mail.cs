using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Traffic
{
    class Traffic_Mail
    {
     
        public void Traffic_sendmail( string serverip, string nowtraffic)
        {
            DBCON.Class1 DBCON = new DBCON.Class1();
            SqlConnection CON = new SqlConnection(DBCON.DBCON);
            string SQL = "";
            SQL = "select distinct a.email, a.mailno , c.mailip, c.trafficlimit, mail_sender from mail_target a , service b ,  mail_info c where c.flag=1 and DATEADD(minute, 10, b.trafficmail) < GETDATE() and a.serverip = b.ServerIP " +
                " and b.serverip = '" + serverip + "'";
            SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
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

                        CON.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = CON;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "update service set trafficmail = getdate() where serverip = @serverip";
                        cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 50).Value = serverip;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cmd = null;
                        CON.Close();
                        CON.Dispose();


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
}
