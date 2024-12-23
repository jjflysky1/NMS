using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Drawing.Text;
using System.Web.Script.Services;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using MySql.Data;
using iTextSharp.text;
using Tamir.SharpSsh.jsch;
using Tamir.SharpSsh.jsch.jce;

namespace WebApplication1
{
    public partial class main8 : System.Web.UI.Page
    {
        
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        soapchart.chart jsonchart = new soapchart.chart();
        protected void Page_Load(object sender, EventArgs e)
        {


            UISET();
            //javascript2();

            if (Request.Cookies["userinfo"] == null)
            {
                Label3.Text = "<script>alert('로그인 해주세요');</script>";
                Response.Redirect("/Default.aspx");
            }
            TextBox5.Value = Request["id"];


        }

        private void UISET()
        {
            TBLSET0();
            TBLSET();
            TBLSET1();
            TBLSET2();
            TBLSET3();
            TBLSET3_1();
            TBLSET4();
            //TBLSET6();
            TBLSET7();
            TBLLOAD5();
            //TBLLOAD10();
        }
        private void TBLSET()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            //TR.BackColor = System.Drawing.Color.White;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 60;
            //TD.Text = "순번";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "운영체제";
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "서비스";
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "상태";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이피";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "대역대";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);


            TBLLIST.Rows.Add(TR);


            TBLLOAD();
        }
        private void TBLSET0()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            TR.Height = 5;

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = " <img src='Img/iconmonstr-cube-1-240_white.png' width='50px' height='50px' /><br><font size='4'><a href='Service/Service.aspx' style='color:#d4d4d4'>Total</a></font>";
            TD.Attributes["style"] = "text-align : center; cursor:pointer;";
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = " <img src='Img/iconmonstr-monitoring-6-240_white.png' width='50px' height='50px' /><br><font size='4'><font color='#d4d4d4'>Management Amount</font>";
            TD.Attributes["style"] = "text-align : center; cursor:pointer;";
            TD.Attributes.Add("Onclick", "go('')");

            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = " <img src='Img/iconmonstr-firewall-2-240_white.png' width='50px' height='50px' /><br><font size='4'><font color='#d4d4d4'>Network / Security</font>";
            TD.Attributes["style"] = "text-align : center;  cursor:pointer; ";
            TD.Attributes.Add("Onclick", "go('네트워크')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = " <img src='Img/iconmonstr-server-7-240_white.png' width='50px' height='50px' /><br><font size='4'><font color='#d4d4d4'>Server</font>";
            TD.Attributes["style"] = "text-align : center;  cursor:pointer; ";
            TD.Attributes.Add("Onclick", "go('서버')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = " <img src='Img/iconmonstr-laptop-1-240_white.png' width='50px' height='50px' /><br><font size='4'><font color='#d4d4d4'>PC</font>";
            TD.Attributes["style"] = "text-align : center;  cursor:pointer; ";
            TD.Attributes.Add("Onclick", "go('pc')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = " <img src='Img/iconmonstr-minus-2-240_white.png' width='50px' height='50px' /><br><font size='4'><font color='#d4d4d4'>NONE</font>";
            TD.Attributes["style"] = "text-align : center; cursor:pointer; ";
            TD.Attributes.Add("Onclick", "go('null')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = " <img src='Img/iconmonstr-stop-3-240_white.png' width='50px' height='50px' /><br><font size='4'><font color='#d4d4d4'>Serivce Down</font> ";
            TD.Attributes["style"] = "text-align : center; cursor:pointer; ";
            TD.Attributes.Add("Onclick", "go3('null')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);


            TBLLIST0.Rows.Add(TR);


            //TBLLOAD();
        }
        private void TBLSET2()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            //TR.BackColor = System.Drawing.Color.White;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 60;
            //TD.Text = "순번";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "운영체제";
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "서비스";
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "상태";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이피";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "대역대";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);

            TBLLIST2.Rows.Add(TR);


            TBLLOAD2();


        }

        private void TBLSET3()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();
            TR.Font.Size = 9;
            //TR.BackColor = System.Drawing.Color.White;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 10;
            //TD.Text = "순번";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "서비스";
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 200;
            TD.Text = "IP / HostName";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "CPU";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "MEMORY";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 10;
            //TD.Text = "STORAGE";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);


            TBLLIST3.Rows.Add(TR);


            TBLLOAD3();
        }

        private void TBLSET3_1()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();
            TR.Font.Size = 9;
            //TR.BackColor = System.Drawing.Color.White;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 10;
            //TD.Text = "순번";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "서비스";
            TD.Attributes["style"] = "text-align : center;";
            //TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 200;
            TD.Text = "IP / HostName";
            TD.Attributes["style"] = "text-align : center;  color:#d4d4d4;";
            TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "CPU";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "MEMORY";
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 10;
            //TD.Text = "STORAGE";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);


            TBLLIST3_1.Rows.Add(TR);


            TBLLOAD3_1();
        }
        private void TBLSET4()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            //TR.BackColor = System.Drawing.Color.White;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 10;
            //TD.Text = "순번";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "Service";
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이피";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "응답시간";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "Traffic";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);




            //TBLLIST4.Rows.Add(TR);


            TBLLOAD4();
        }

        private void TBLSET6()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            TR.BackColor = System.Drawing.Color.White;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 10;
            //TD.Text = "순번";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "어플리케이션";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "수량";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            //TBLLIST6.Rows.Add(TR);


            TBLLOAD6();
        }

        private void TBLSET7()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            //TR.BackColor = System.Drawing.Color.White;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 10;
            //TD.Text = "순번";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "대역대";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "수량";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            //TBLLIST7.Rows.Add(TR);


            TBLLOAD7();
        }

        private void TBLSET10()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            TR.BackColor = System.Drawing.Color.White;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 10;
            //TD.Text = "순번";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "대역대";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "수량";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            //TBLLIST7.Rows.Add(TR);


            TBLLOAD7();
        }
        //다운 서버 장비 top
        private void TBLLOAD()
        {
            SQL = "select distinct os,name,serverip,serverid,serverpwd,status,network_name,computer_name,category from Service where status <> 'Server Connect' and flag = 1 and category <> N'네트워크/보안 장비' limit 3 ";


            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            string status = "";
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD(row["os"].ToString(), row["Name"].ToString(), row["serverip"].ToString(),
                     row["serverid"].ToString(), row["serverpwd"].ToString(), row["status"].ToString(), row["network_name"].ToString(), row["computer_name"].ToString()
                     , row["category"].ToString());
                status = row["status"].ToString();
            }
            if (status.Contains("Disconnect") == true)
            {
                //inner2.Attributes["style"] = "visibility:hidden;";
                //Div7.Attributes["style"] = "visibility:inherit; margin-top:-62px; margin-left:20px;";

            }
            else
            {
                //inner2.Attributes["style"] = "visibility:inherit; margin-left:20px;";
                //Div7.Attributes["style"] = "visibility:hidden; margin-top:-62px; margin-left:20px;";
            }
        }
        ///다운 네트워크 / 보안장비 top
        private void TBLLOAD2()
        {
            SQL = "select * from Service where status <> 'Server Connect' and flag = 1 and category = N'네트워크/보안 장비' limit 3 ";


            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            string status = "";
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD2(row["NO"].ToString(), row["os"].ToString(), row["Name"].ToString(), row["serverip"].ToString(),
                     row["serverid"].ToString(), row["serverpwd"].ToString(), row["status"].ToString(), row["network_name"].ToString(), row["computer_name"].ToString(),
                     row["category"].ToString());
                status = row["status"].ToString();
            }

            if (status.Contains("Disconnect") == true)
            {
                //inner1.Attributes["style"] = "visibility:hidden;";
                //Div3.Attributes["style"] = "visibility:inherit; margin-top:-62px; margin-left:20px;";

            }
            else
            {
                //inner1.Attributes["style"] = "visibility:inherit; margin-left:20px;";
                //Div3.Attributes["style"] = "visibility:hidden; margin-top:-62px; margin-left:20px;";
            }
        }
        ///서버 자원top
        private void TBLLOAD3()
        {
            MySqlDataAdapter ADT = new MySqlDataAdapter("Server_resource_list", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD3(row["serverip"].ToString(), row["cpu"].ToString(), row["memory"].ToString(), row["cpulimit"].ToString(), row["memorylimit"].ToString(), row["mailip"].ToString(), row["hd"].ToString()
                    , row["network_name"].ToString(), row["computer_name"].ToString(), row["category"].ToString());
            }
        }
        /// 네트워크/보안 자원top
        private void TBLLOAD3_1()
        {


            MySqlDataAdapter ADT = new MySqlDataAdapter("Secure_resource_list", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD3_1(row["serverip"].ToString(), row["cpu"].ToString(), row["memory"].ToString(), row["cpulimit"].ToString(), row["memorylimit"].ToString(), row["mailip"].ToString(), row["hd"].ToString()
                    , row["network_name"].ToString(), row["computer_name"].ToString(), row["category"].ToString());
            }



        }
        //트래픽 top
        private void TBLLOAD4()
        {
            SQL = "select distinct b.computer_name, b.network_name, a.serverip, round(a.traffic,2) as traffic, b.pingtime , category,c.trafficlimit , c.mailip from server_traffic a, service b , mail_info c " +
                "where traffic is not null and a.serverip = b.ServerIP and b.status = 'Server Connect' and b.flag = 1 order by traffic desc limit 5";


            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD4(row["serverip"].ToString(), row["traffic"].ToString(), row["pingtime"].ToString(), row["trafficlimit"].ToString(), row["mailip"].ToString(),
                    row["network_name"].ToString(), row["computer_name"].ToString(), row["category"].ToString());
            }
        }
        //종합
        public void TBLLOAD5()
        {
            ///다운된 서비스
            string downcount = "";
            SQL = "select count(name) as count, left( now(), 10) as getdate,  SUBSTR(_UTF8'일월화수목금토', DAYOFWEEK(now()), 1) as name, DATE_FORMAT(NOW(), '%T') as time from down_log where left(time, 10) = LEFT(now(), 10) and status = 'down'";
            MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET2 = new DataSet();
            ADT2.Fill(DBSET2, "BD2");
            foreach (DataRow row in DBSET2.Tables["BD2"].Rows)
            {
                downcount = row["count"].ToString();
                //Label1.Text = row["getdate"].ToString() + " ";
                //Label1.Text = Label1.Text + "(" + row["name"].ToString() + ") ";
                //Label1.Text = Label1.Text + row["time"].ToString();
            }

            //평균 트래픽 top1
            string time1 = DateTime.Now.ToString("HH");
            //string time2 = DateTime.Now.ToString(row2["time"].ToString());
            //SQL = "select top 1 serverip, round(traffic,2) as traffic from System_Log_Traffic where serverip " +
            //    "IN (select serverip from Service where flag = '1') " +
            //    " and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = "+ time1.ToString() +"" +
            //    //" and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, now()), 121), 12, 2)" +
            //    " and left(time, 10) = LEFT(now(), 10) order by traffic desc";
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            ADT3.SelectCommand.Parameters.AddWithValue("@time1", time1);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            {

                //Label4.Text = row["traffic"].ToString();
                // Label5.Text = row["serverip"].ToString();

                if (row["serverip"].ToString() == "")
                {
                    //그래프
                    SQL = "select serverip, round(traffic,2) as traffic from System_Log_Traffic where serverip " +
                        "IN (select serverip from Service where flag = '1') " +
                        " and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, getdate()), 121), 12, 2)" +
                        " and left(time, 10) = LEFT(GETDATE(), 10) order by traffic desc limit 1";
                    MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
                    DataSet DBSET4 = new DataSet();
                    ADT4.Fill(DBSET4, "BD4");
                    foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
                    {

                        // Label4.Text = row["traffic"].ToString();
                        // Label5.Text = row["serverip"].ToString();
                    }
                }


            }

            //장비 댓수 카운트
            SQL = "select count(distinct serverip) as allcount, count(case when category is not null then 1 end) 'Alivecount' , count(case when category = N'서버 장비'  then 1 end) 'Server',count(case when category = N'네트워크/보안 장비'  then 1 end) 'security'," +
                "count(case when category ='PC'  then 1 end) 'pc', count(case when  category is null then 1 end) 'none'  from service_v";
            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET5 = new DataSet();
            ADT5.Fill(DBSET5, "BD5");
            foreach (DataRow row in DBSET5.Tables["BD5"].Rows)
            {
                TBLADD0(row["allcount"].ToString(), row["alivecount"].ToString(), row["Server"].ToString(), row["security"].ToString(), row["pc"].ToString(), row["none"].ToString()
                    , downcount);
            }

            //메모리 top
            SQL = "select distinct  serverip, memory from Service where status = 'Server Connect' and flag = '1'  order by memory desc limit 1";
            MySqlDataAdapter ADT6 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET6 = new DataSet();
            ADT6.Fill(DBSET6, "BD6");
            foreach (DataRow row in DBSET6.Tables["BD6"].Rows)
            {
                //Label6.Text = row["serverip"].ToString();
                //Label7.Text = row["memory"].ToString();
            }

        }

        private void TBLLOAD6()
        {
            SQL = "select count(*) count,app from server_app group by app order by count desc limit 5";


            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD6(row["count"].ToString(), row["app"].ToString());
            }
        }

        //대역대 리스트
        private void TBLLOAD7()
        {
            SQL = "select count( network_name) count ,  network_name from Service_v " +
                "where network_name<> 'null' group by network_name " +
                "order by count desc";


            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD7(row["count"].ToString(), row["network_name"].ToString());
            }
        }


        private void TBLADD0(string allcount, string alivecount, string Server, string security, string pc, string none, string downcount)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5'><b><a href='Service/Service.aspx' style='color:#d4d4d4'>" + allcount.ToString() + "</a></b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; margin-top:-10px; cursor:pointer;";
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5' color='#d4d4d4'><b>" + alivecount.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer;";
            TD.Attributes.Add("Onclick", "go('')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5' color='#d4d4d4'><b>" + security.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; ";
            TD.Attributes.Add("Onclick", "go('네트워크')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5' color='#d4d4d4'><b>" + Server.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; ";
            TD.Attributes.Add("Onclick", "go('서버')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5' color='#d4d4d4'><b>" + pc.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; ";
            TD.Attributes.Add("Onclick", "go('pc')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5' color='#d4d4d4'><b>" + none.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; ";
            TD.Attributes.Add("Onclick", "go('null')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5' color='#d4d4d4'><b>" + downcount.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; ";
            TD.Attributes.Add("Onclick", "go3('null')");
            TD.Attributes.Add("onmouseover", "this.style.color='white'");
            TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);



            TBLLIST0.Rows.Add(TR);

        }


        long a = 1;
        private void TBLADD(string os, string name, string serverip, string serverid, string serverpwd, string status
            , string network_name, string computer_name, string category)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();
            //TR.ForeColor = System.Drawing.Color.White;
            TR.BackColor = System.Drawing.Color.Red;
            TR.Font.Size = 11;
            TR.Font.Bold = true;


            //TD = new TableCell();
            //TD.Width = 10;
            //TD.Text = a.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = os.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);



            TD = new TableCell();
            TD.Width = 60;
            TD.Text = name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = status.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 10;
            TD.Text = serverip.ToString() + " / " + computer_name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 10;
            TD.Text = network_name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);


            //if (status.Contains("Disconnect") == true)
            //{
            //    cube.Attributes["Class"] = "cssload-thecube1";
            //    inner2.Attributes["Class"] = "cssload-loader1";
            //    inner2_1.Attributes["Class"] = "cssload-inner1 cssload-one";
            //    inner2_2.Attributes["Class"] = "cssload-inner1 cssload-two";
            //    inner2_3.Attributes["Class"] = "cssload-inner1 cssload-three";
            //}
            //else
            //{
            //    inner2.Attributes["Class"] = "cssload-loader";
            //    inner2_1.Attributes["Class"] = "cssload-inner cssload-one";
            //    inner2_2.Attributes["Class"] = "cssload-inner cssload-two";
            //    inner2_3.Attributes["Class"] = "cssload-inner cssload-three";
            //}
            a++;
            TBLLIST.Rows.Add(TR);

        }


        long b = 1;
        private void TBLADD2(string NO, string os, string name, string serverip, string serverid, string serverpwd, string status
            , string network_name, string computer_name, string category)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();
            TR.ForeColor = System.Drawing.Color.White;
            TR.BackColor = System.Drawing.Color.Red;
            TR.Font.Size = 11;
            TR.Font.Bold = true;

            TD = new TableCell();
            TD.Width = 40;
            TD.Text = NO.ToString();
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            //TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 10;
            //TD.Text = b.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle; ";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = os.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; ";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = status.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 10;
            TD.Text = serverip.ToString() + " / " + computer_name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 10;
            TD.Text = network_name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);



            b++;
            TBLLIST2.Rows.Add(TR);



        }

        long c = 1;
        private void TBLADD3(string serverip, string cpu, string memory, string cpulimit, string memorylimit, string mailip,
            string hd, string network_name, string computer_name, string category)
        {
            TableRow TR;
            TableCell TD;

            TR = new TableRow();
            TR.Font.Size = 9;
            TD = new TableCell();
            TD.Width = 10;
            //TD.Text = NO.ToString();
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            //TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = c.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 100;
            TD.Text = serverip.ToString() + " / " + computer_name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer; color:#d4d4d4;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 100;
            //TD.Text = "<a name='poll_bar'></a><span name='poll_val'>"+ cpu.ToString() + "%</span>";
            TD.Text = cpu.ToString() + "%" + "<div class='progress' style='width:100%'>" +
                "<div class='progress-bar progress-bar-success progress-bar-striped' role='progressbar' aria-valuenow='40' aria-valuemin='0' aria-valuemax='100' style='width:" + cpu.ToString() + "%'>" +
                " </div></div>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer; color:#d4d4d4;";
            if (Convert.ToDouble(cpu.ToString()) > Convert.ToDouble(cpulimit.ToString()) && Convert.ToDouble(cpulimit.ToString()) != 0)
            {
                TD.ForeColor = System.Drawing.Color.Red;

            }
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 100;
            //TD.Text = "<a name='poll_bar'></a><span name='poll_val'>" + memory.ToString() + "%</span>";
            TD.Text = memory.ToString() + "%" + "<div class='progress' style='width:100%'>" +
                "<div class='progress-bar progress-bar-primary progress-bar-striped' role='progressbar' aria-valuenow='40' aria-valuemin='0' aria-valuemax='100' style='width:" + memory.ToString() + "%'>" +
                "<span  class='popOver' data-toggle='tooltip' data-placement='top' title='75 %'> </span>   </div></div>";

            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer; color:#d4d4d4;";
            if (Convert.ToDouble(memory.ToString()) > Convert.ToDouble(memorylimit.ToString()) && Convert.ToDouble(memorylimit.ToString()) != 0)
            {
                TD.ForeColor = System.Drawing.Color.Red;

            }
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 120;
            ////TD.Text = "<a name='poll_bar'></a><span name='poll_val'>" + memory.ToString() + "%</span>";
            //TD.Text = hd.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer; ";
            //if (Convert.ToDouble(memory.ToString()) > Convert.ToDouble(memorylimit.ToString()) && Convert.ToDouble(memorylimit.ToString()) != 0)
            //{
            //    TD.ForeColor = System.Drawing.Color.Red;
            //}
            //TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            //TR.Cells.Add(TD);

            c++;
            TBLLIST3.Rows.Add(TR);

        }

        long c_1 = 1;
        private void TBLADD3_1(string serverip, string cpu, string memory, string cpulimit, string memorylimit, string mailip,
            string hd, string network_name, string computer_name, string category)
        {
            TableRow TR;
            TableCell TD;

            TR = new TableRow();
            TR.Font.Size = 9;
            TD = new TableCell();
            TD.Width = 10;
            //TD.Text = NO.ToString();
            TD.Attributes["style"] = "text-align : center; color:#d4d4d4;";
            //TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = c.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 100;
            TD.Text = serverip.ToString() + " / " + computer_name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer; color:#d4d4d4;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 100;
            //TD.Text = "<a name='poll_bar'></a><span name='poll_val'>"+ cpu.ToString() + "%</span>";
            TD.Text = cpu.ToString() + "%" + "<div class='progress' style='width:100%; '>" +
                "<div class='progress-bar progress-bar-success progress-bar-striped' role='progressbar' aria-valuenow='40' aria-valuemin='0' aria-valuemax='100' style='width:" + cpu.ToString() + "%'>" +
                " </div></div>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer; color:#d4d4d4;";
            if (Convert.ToDouble(cpu.ToString()) > Convert.ToDouble(cpulimit.ToString()) && Convert.ToDouble(cpulimit.ToString()) != 0)
            {
                TD.ForeColor = System.Drawing.Color.Red;

            }
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);


            if (memory.ToString().Length == 0)
            {
                memory = "0";
            }
            TD = new TableCell();
            TD.Width = 100;
            //TD.Text = "<a name='poll_bar'></a><span name='poll_val'>" + memory.ToString() + "%</span>";
            TD.Text = memory.ToString() + "%" + "<div class='progress' style='width:100%'>" +
                "<div class='progress-bar progress-bar-primary progress-bar-striped' role='progressbar' aria-valuenow='40' aria-valuemin='0' aria-valuemax='100' style='width:" + memory.ToString() + "%'>" +
                "<span  class='popOver' data-toggle='tooltip' data-placement='top' title='75 %'> </span>   </div></div>";

            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer; color:#d4d4d4;";
            if (Convert.ToDouble(memory.ToString()) > Convert.ToDouble(memorylimit.ToString()) && Convert.ToDouble(memorylimit.ToString()) != 0)
            {
                TD.ForeColor = System.Drawing.Color.Red;

            }
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 120;
            ////TD.Text = "<a name='poll_bar'></a><span name='poll_val'>" + memory.ToString() + "%</span>";
            //TD.Text = hd.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer; ";
            //TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            //TR.Cells.Add(TD);

            c++;
            TBLLIST3_1.Rows.Add(TR);

        }

        long d = 1;
        private void TBLADD4(string serverip, string traffic, string pingtime, string trafficlimit, string mailip,
            string network_name, string computer_name, string category)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 10;
            //TD.Text = NO.ToString();
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = d.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverip.ToString() + " / " + computer_name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);



            TD = new TableCell();
            TD.Width = 60;
            TD.Text = pingtime.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 100;
            //TD.Text = "<span name='poll_val1'>" + traffic.ToString() + " KB / S" + "</span> <br> <a name='poll_bar1'></a>";
            if (Convert.ToInt64(traffic.ToString()) == 0)
            {
                TD.Text = traffic.ToString() + " Bits / S";
            }
            else
            {
                TD.Text = String.Format("{0:#,###}", Convert.ToInt64(traffic)) + " Bits / S";
            }

            TD.Attributes["style"] = "text-align : center; vertical-align:middle;cursor:pointer;";
            TD.Attributes.Add("Onclick", "go2('" + serverip + "','" + category + "')");
            TR.Cells.Add(TD);
            //if (Convert.ToDouble(traffic) >= 10000)
            //{
            //    TD.ForeColor = System.Drawing.Color.Red;
            //}

            if (Convert.ToDouble(traffic.ToString()) > Convert.ToDouble(trafficlimit.ToString()) && Convert.ToDouble(trafficlimit.ToString()) != 0)
            {
                TD.ForeColor = System.Drawing.Color.Red;

            }

            d++;
            //TBLLIST4.Rows.Add(TR);

        }

        long e = 1;
        private void TBLADD6(string count, string app)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 10;
            //TD.Text = NO.ToString();
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = e.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = app.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = count.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            e++;
            //TBLLIST6.Rows.Add(TR);

        }

        long f = 1;
        private void TBLADD7(string count, string network_name)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 10;
            //TD.Text = NO.ToString();
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = f.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = network_name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = count.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            f++;
            // TBLLIST7.Rows.Add(TR);

        }


        private void TBLADD10(string serverip, string cpu, string memory, string status, string trafic)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 10;
            //TD.Text = NO.ToString();
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = f.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverip.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);



            f++;
            //TBLLIST10.Rows.Add(TR);

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            //TBLLOAD5();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Service/Service.aspx?nowpage=" + 1 + "&type1=5" + "&search=" + HiddenField1.Value);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Service/Service.aspx");

        }



        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //DB.Open();

            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = DB;

            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = "update log_time_config set area_chart = @area_chart";
            //cmd.Parameters.Add("@area_chart", SqlDbType.NVarChar, 100).Value = DropDownList2.SelectedValue;
            //cmd.ExecuteNonQuery();
            //DB.Close();
            //cmd.Dispose();
            //cmd = null;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("Service/Service_list.aspx?serverip=" + HiddenField1.Value + "&category=" + HiddenField2.Value);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("Log/Service_Log.aspx?startdate=" + DateTime.Now.ToString("yyyy-MM-dd") + " & enddate=" + DateTime.Now.ToString("yyyy-MM-dd"));
        }

        private void TBLSET1()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            //TR.BackColor = System.Drawing.Color.WhiteSmoke;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 20;
            TD.Text = "서버 아이피";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; color:#d4d4d4;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "내용";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; color:#d4d4d4;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "시각";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; color:#d4d4d4;";
            TR.Cells.Add(TD);

            TBLLIST1.Rows.Add(TR);
            TBLLOAD1();
        }
        private void TBLLOAD1()
        {
            SQL = "SELECT a.*, b.computer_name from Event_log a, service b WHERE a.serverip = b.serverip order by time desc limit 3";

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD1(row["serverip"].ToString(), row["event_log"].ToString(), row["time"].ToString(), row["computer_name"].ToString());

            }
        }

        private void TBLADD1(string serverip, string event_log, string time, string computer_name)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();
            TR.ForeColor = System.Drawing.Color.Black;
            TD = new TableCell();
            TD.Width = 20;

            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 10;
            TD.Text = "<font size='2'>" + serverip.ToString() + " / " + computer_name + "</font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; color:#d4d4d4;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 250;
            TD.Text = "<font size='2'>" + event_log.ToString() + "</font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;  color:#d4d4d4;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 100;
            TD.Text = "<font size='2'>" + time.ToString() + "</font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; color:#d4d4d4; ";
            TR.Cells.Add(TD);


            a++;
            TBLLIST1.Rows.Add(TR);

        }
     
        public class network
        {
            public string product { get; set; }
            public string status { get; set; }
       
        }
        /// <summary>
        /// 네트워크/보안
        /// </summary>
        [WebMethod] 
        public static List<network> javascript2()
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<network> Parts = new List<network>();
            Parts.Clear();


            //string SQL = "select  cpu,memory,serverip, category, now() as getdate,status , left(os,30) as os from service where memory is not null and flag = '1' and Category like '%네트워크%'  ORDER BY INET_ATON(serverip) LIMIT 10 ";
            //string SQL = "select  cpu,memory,serverip, category, now() as getdate,status , left(os,30) as os from service where flag = '1' and Category IS NOT null  ORDER BY INET_ATON(serverip)   ";
            //string SQL = "(SELECT distinct cpu, MEMORY, a.serverip, category, now() as getdate,status , left(os, 30) as os , b.Model, a.computer_name , a.network_name " +
            //    "from service a, server_oid_list b where flag = '1' AND Category IS NOT NULL and  a.serverip = b.serverip ORDER BY network_name asc, INET_ATON(a.serverip) desc) " +
            //    "UNION " +
            //    "(select  cpu, memory, serverip, category, now() as getdate, status, left(os, 30) as os, case when category != '서버 장비' then '＃' end AS model, computer_name, network_name FROM " +
            //    "service where flag = '1' and Category = '서버 장비') ORDER BY network_name asc, INET_ATON(serverip) desc";
            string SQL = "SELECT distinct cpu, MEMORY, serverip, category, now() as getdate,status , left(os, 30) as os , vandor, computer_name , network_name FROM service where flag = '1' AND Category IS NOT NULL";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            string cpu, memory, serverip, category, status, os, vandor = "";
            int i = 0;
            List<string> list = new List<string>();
            list.Clear();
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {

                if (list.Contains(row1["serverip"].ToString()) == false)
                {
                    list.Add(row1["serverip"].ToString());
                    cpu = row1["cpu"].ToString();
                    memory = row1["memory"].ToString();
                    serverip = row1["serverip"].ToString();
                    category = row1["category"].ToString();
                    status = row1["status"].ToString();
                    os = row1["os"].ToString();
                    vandor = row1["vandor"].ToString();
                    

                    //라벨 추가
                    Label label = new Label();
                    label.ID = "Label" + i;
                    //string roatetion = "";

                    //main7_ d = new main7_();
                    //d.div3.Controls.Add(label);
                    
                    StringBuilder st = new StringBuilder();
                  
                    
                    st.Append("<div class='product' id='product" + i + "'  style='width:130px; height:90px; float:left; margin:0 auto;'><center>");
                    //st.Append("<center><a href='Service/Service_list.aspx?serverip=" + row1["serverip"].ToString() + "&category=" + row1["category"] + "' style='text-decoration:none; color='black''>");
                    if (vandor.Contains("CISCO") == true || vandor.Contains("＃") == true)
                    {
                        if (status.Contains("Disconnect") == true)
                        {
                            st.Append("<img src='Dash_image/switch_error.png' width='100' height='50' >");
                        }
                        else
                        {
                            st.Append("<img src='Dash_image/switch.png' width='100' height='50' >");
                        }
                    }
                    if (vandor.Contains("SECUI") == true)
                    {
                        if (status.Contains("Disconnect") == true)
                        {
                            st.Append("<img src='Dash_image/security_error.png' width='100' height='50'>");
                        }
                        else
                        {
                            st.Append("<img src='Dash_image/security.png' width='100' height='50' >");
                        }
                    }
                    if (vandor.Contains("AXGATE") == true)
                    {
                        if (status.Contains("Disconnect") == true)
                        {
                            st.Append("<img src='Dash_image/security_error.png' width='100' height='50'>");
                        }
                        else
                        {
                            st.Append("<img src='Dash_image/security.png' width='100' height='50'>");
                        }
                    }
                    if (category.Contains("서버") == true)
                    {
                        if (status.Contains("Disconnect") == true)
                        {
                            st.Append("<img src='Dash_image/server2_error.png' width='100' height='50' >");
                        }
                        else
                        {
                            st.Append("<img src='Dash_image/server2.png' width='100' height='50' >");
                        }
                    }
                    //st.Append("<font size='2' color='#d4d4d4' >" + row1["serverip"].ToString() + "</font></a></center>");
                    st.Append("<div style='margin-top:-5px'><font size='2' color='#d4d4d4' >" + row1["network_name"].ToString() + "</font></div>");
                    st.Append("<div style='margin-top:-5px'><font size='1.5' color='#d4d4d4' >" + row1["computer_name"].ToString() + "</font></center></div>");
                    st.Append("</div>");
                    
                    label.Text = st.ToString();

                    Parts.Add(new network
                    {
                        product = label.Text,
                        status = status

                    });
                    st = null;
                    i++;

                 

                }
            }
            return Parts;
        }

    }

}
