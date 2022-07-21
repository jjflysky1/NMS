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
    class Memory_Mail
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        public void memory_sendmail(string serverip, string nowmemroy)
        {
       
            string SQL = "";
            SQL = "select distinct a.email, a.mailno , c.mailip, c.memorylimit, mail_sender from mail_target a , service b ,  mail_info c where  c.flag=1 and DATEADD(minute, 10, b.memorymail) < GETDATE() and a.serverip = b.ServerIP " +
                " and b.serverip = '" + serverip + "'";
            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                if(row["email"].ToString() != "")
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

                        MAIL.Subject = "메모리 알림";
                        MAIL.Body = serverip + " 서버 현재 메모리 알림 기준치인 " + row["memorylimit"].ToString() + " 넘어 " + nowmemroy + " 입니다. ";
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
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

        }
    }
}
