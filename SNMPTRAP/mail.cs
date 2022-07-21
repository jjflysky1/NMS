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
using System.Windows.Forms;

namespace SNMPTRAP
{
    class mail
    {
        public void Event_sendmail(string serverip, string Eventtext, string eventname)
        {

            DBCON.Class1 DBCON = new DBCON.Class1();
            MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
            string SQL = "";
            SQL = "select distinct a.*, b.mailip from  event_mail_target a , mail_info b , Event_list c where eventname like '%" + eventname.ToString() + "%' and c.flag = '1' AND a.eventname = c.name";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                ////문자
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
                //        formData.Add(new StringContent( serverip +" 에서" + Eventtext + " 이벤트가 발견 되었습니다"), "msg");                // 메세지 내용
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
                //        //CON.Open();
                //        //MySqlCommand cmd = new MySqlCommand();
                //        //cmd.Connection = CON;
                //        //cmd.CommandType = System.Data.CommandType.Text;
                //        //cmd.CommandText = "update service set trafficmail = now() where serverip = @serverip";
                //        //cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 50).Value = serverip;
                //        //cmd.ExecuteNonQuery();
                //        //cmd.Dispose();
                //        //cmd = null;
                //        //CON.Close();
                //        //CON.Dispose();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                if (row["email"].ToString() != "")
                {
                    try
                    {
                        // MessageBox.Show("메일!");
                        MailMessage MAIL = new MailMessage();
                        SmtpClient SMTPMAIL = new SmtpClient(row["mailip"].ToString());
                        //MAIL.From = new MailAddress(row["mail_sender"].ToString());
                        MAIL.From = new MailAddress("noreply@sungsimit.co.kr");
                        SMTPMAIL.Port = 25;
                        //MAIL.To.Add(Recever.Text.ToString());
                        MAIL.To.Add(row["email"].ToString());
                        //if (CC.Text.ToString() != "")
                        //{
                        //    MAIL.CC.Add(CC.Text.ToString());
                        //}
                        MAIL.Subject = "이벤트 알림";
                        MAIL.Body = Eventtext + " 이벤트가 발견 되었습니다";
                        MAIL.BodyEncoding = System.Text.Encoding.UTF8;
                        MAIL.SubjectEncoding = System.Text.Encoding.UTF8;


                        SMTPMAIL.Send(MAIL);

                        //CON.Open();
                        //MySqlCommand cmd = new MySqlCommand();
                        //cmd.Connection = CON;
                        //cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.CommandText = "update service set trafficmail = getdate() where serverip = @serverip";
                        //cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 50).Value = serverip;
                        //cmd.ExecuteNonQuery();
                        //cmd.Dispose();
                        //cmd = null;
                        //CON.Close();
                        //CON.Dispose();
                    }
                    catch
                    {

                    }
                }

            }

        }
    }
}
