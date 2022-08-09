using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace pingtime
{
    class server_down_mail
    {
        public void send_mail(string serverip, string status)
        {
            if (status.Contains("복구") == true)
            {
                try
                {
                    DBCON.Class1 DBCON = new DBCON.Class1();
                    MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                    string SQL = "";
                    SQL = "select distinct a.phone , a.email, a.mailno ,mailip , mail_sender  from mail_target a , server_down_log b , " +
                        " mail_info c where c.flag = 1 and b.serverip = '" + serverip + "' and a.serverip = b.ServerIP ORDER BY TIME DESC  ";
                    MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                    DataSet DBSET = new DataSet();
                    ADT.Fill(DBSET, "BD");
                    foreach (DataRow row in DBSET.Tables["BD"].Rows)
                    {

                        if (row["email"].ToString() != "")
                        {
                            try
                            {

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

                                MAIL.Subject = "장비 복구 알림";
                                MAIL.Body = serverip + " 장비가 복구 되었습니다. " + Environment.NewLine + "시간 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                                MAIL.BodyEncoding = System.Text.Encoding.UTF8;
                                MAIL.SubjectEncoding = System.Text.Encoding.UTF8;


                                SMTPMAIL.Send(MAIL);


                                CON.Open();
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update server_down_log set mail_send_time = NULL where serverip = @serverip";
                                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 50).Value = serverip;
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();
                                CON.Dispose();

                                Console.WriteLine("------------------------------------------------------------- 복구 이메일 보내기 완료");
                    
                            }
                            catch
                            {
                                Console.WriteLine("복구 메일에러 1");
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("복구 메일에러 2");
                }
            }
            else if (status.Contains("다운") == true)
            {
                try
                {
                    DBCON.Class1 DBCON = new DBCON.Class1();
                    MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                    string SQL = "";
                    //SQL = "select distinct a.phone , a.email, a.mailno ,mailip , mail_sender  from mail_target a , server_down_log b ,  mail_info c " +
                    //    "where c.flag = 1 AND (SELECT  date_format(DATE_ADD(mail_send_time, INTERVAL 1440 MINUTE), '%Y-%m-%d %T') FROM server_down_log " +
                    //    "WHERE serverip = '" + serverip + "' ORDER BY TIME DESC LIMIT 1) < NOW() AND b.serverip = '" + serverip + "' and a.serverip = b.ServerIP and DATE_ADD(mail_send_time, INTERVAL 1440 MINUTE) < now()  ORDER BY TIME DESC";
                    SQL = "select distinct a.phone , a.email, a.mailno ,mailip , mail_sender  FROM mail_target a , server_down_log b ,  mail_info c where c.flag = 1 AND  " +
                        "b.serverip = '" + serverip + "' and a.serverip = b.ServerIP AND mail_send_time IS null ORDER BY TIME DESC";
                    MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                    DataSet DBSET = new DataSet();
                    ADT.Fill(DBSET, "BD");
                    foreach (DataRow row in DBSET.Tables["BD"].Rows)
                    {
                        if (row["email"].ToString() != "")
                        {
                            try
                            {

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

                                MAIL.Subject = "장비 다운 알림";
                                MAIL.Body = serverip + " 장비가 다운 되었습니다." + Environment.NewLine + "시간 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                                MAIL.BodyEncoding = System.Text.Encoding.UTF8;
                                MAIL.SubjectEncoding = System.Text.Encoding.UTF8;


                                SMTPMAIL.Send(MAIL);


                                CON.Open();
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update server_down_log set mail_send_time = now() where serverip = @serverip";
                                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 50).Value = serverip;
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();
                                CON.Dispose();
                                Console.WriteLine("------------------------------------------------------------- 다운 이메일 보내기 완료");

                            }
                            catch
                            {
                                Console.WriteLine("다운 메일에러 1");
                            }
                        }

                    }
                }
                catch
                {
                    Console.WriteLine("다운 메일에러 2");
                }
            }
            else if (status.Contains("타임") == true)
            {
                try
                {
                    DBCON.Class1 DBCON = new DBCON.Class1();
                    MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                    string SQL = "";
                    //SQL = "select distinct a.phone , a.email, a.mailno ,mailip , mail_sender  from mail_target a , server_down_log b ,  mail_info c " +
                    //    "where c.flag = 1 AND (SELECT  date_format(DATE_ADD(mail_send_time, INTERVAL 1440 MINUTE), '%Y-%m-%d %T') FROM server_down_log " +
                    //    "WHERE serverip = '" + serverip + "' ORDER BY TIME DESC LIMIT 1) < NOW() AND b.serverip = '" + serverip + "' and a.serverip = b.ServerIP and DATE_ADD(mail_send_time, INTERVAL 1440 MINUTE) < now()  ORDER BY TIME DESC";
                    SQL = "select distinct a.phone , a.email, a.mailno ,mailip , mail_sender  FROM mail_target a , server_down_log b ,  mail_info c where c.flag = 1 AND  " +
                        "b.serverip = '" + serverip + "' and a.serverip = b.ServerIP AND mail_send_time IS null ORDER BY TIME DESC";
                    MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                    DataSet DBSET = new DataSet();
                    ADT.Fill(DBSET, "BD");
                    foreach (DataRow row in DBSET.Tables["BD"].Rows)
                    {
                        if (row["email"].ToString() != "")
                        {
                            try
                            {

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

                                MAIL.Subject = "장비 타임아웃 알림";
                                MAIL.Body = serverip + " 장비가 타임아웃 되었습니다." + Environment.NewLine + "시간 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                                MAIL.BodyEncoding = System.Text.Encoding.UTF8;
                                MAIL.SubjectEncoding = System.Text.Encoding.UTF8;


                                SMTPMAIL.Send(MAIL);


                                CON.Open();
                                MySqlCommand cmd = new MySqlCommand();
                                cmd.Connection = CON;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "update server_down_log set mail_send_time = now() where serverip = @serverip";
                                cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 50).Value = serverip;
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();
                                CON.Dispose();
                                Console.WriteLine("------------------------------------------------------------- 타임아웃 이메일 보내기 완료");

                            }
                            catch
                            {
                                Console.WriteLine("다운 메일에러 1");
                            }
                        }

                    }
                }
                catch
                {
                    Console.WriteLine("다운 메일에러 2");
                }
            }





        }

    }
}
////문자 알림
//try
//{
//    FileInfo imgFile = new FileInfo(@"C:\Users\Administrator\Pictures\food.jpeg");
//    byte[] imgData = new byte[0];

//    if (imgFile.Exists && imgFile.Length > 0)
//        imgData = File.ReadAllBytes(imgFile.FullName);

//    using (HttpClient client = new HttpClient())
//    {
//        MultipartFormDataContent formData = new MultipartFormDataContent();
//        formData.Add(new StringContent("jjflysky"), "user_id");                                                 //SMS 아이디
//        formData.Add(new StringContent("4wpguuo9vve9r7zdvfl8g9a4jsexf9sj"), "key");                                                     //인증키
//        formData.Add(new StringContent(serverip + " 서버가 다운 되었습니다"), "msg");                // 메세지 내용
//        formData.Add(new StringContent(row["phone"].ToString()), "receiver");                         // 수신번호
//        formData.Add(new StringContent("01111111111|담당자,01111111112|홍길동"), "destination");        // 수신인 %고객명% 치환      
//        formData.Add(new StringContent(""), "sender");                                                  // 발신번호
//        formData.Add(new StringContent(""), "rdate");                                                   // 예약일자 - 20161004 : 2016-10-04일기준
//        formData.Add(new StringContent(""), "rtime");                                                   // 예약시간 - 1930 : 오후 7시30분
//        formData.Add(new StringContent("Y"), "testmode_yn");                                            // Y 인경우 실제문자 전송X , 자동취소(환불) 처리
//        formData.Add(new StringContent(""), "title");                                            //  LMS, MMS 제목 (미입력시 본문중 44Byte 또는 엔터 구분자 첫라인)

//        if (imgData.Length > 0)
//            formData.Add(new StreamContent(new MemoryStream(imgData)), "image", imgFile.Name);

//        client.DefaultRequestHeaders.Add("Accept", "*/*");

//        var response = client.PostAsync("https://apis.aligo.in/send/", formData).Result;


//        if (!response.IsSuccessStatusCode)
//            Console.WriteLine(response.StatusCode);
//        else
//        {
//            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
//            Console.WriteLine(content);
//        }
//        CON.Open();
//        MySqlCommand cmd = new MySqlCommand();
//        cmd.Connection = CON;
//        cmd.CommandType = System.Data.CommandType.Text;
//        cmd.CommandText = "update server_down_log set mail_send_time=now() where serverip = @serverip";
//        cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 50).Value = serverip.ToString();
//        cmd.ExecuteNonQuery();
//        cmd.Dispose();
//        cmd = null;
//        CON.Close();
//        CON.Dispose();
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}