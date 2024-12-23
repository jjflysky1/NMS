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
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Diagnostics;

namespace WebApplication1
{
    public partial class main4 : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Cookies["userinfo"] == null)
            {
                Label3.Text = "<script>alert('로그인 해주세요');</script>";
                Response.Redirect("/Default.aspx");
            }
            TextBox5.Value = Request["id"];

            //javascript();
            //javascript2();
            TBLSET();
            TBLSET2();
            TBLSET3();
            TBLSET4();


        }
        /// <summary>
        /// 
        /// 메모리
        /// </summary>
        private void javascript()
        {
            string SQL = "select top 10  cpu,memory,serverip,getdate() as getdate, category,status, left(os,30) as os  from service where memory is not null and flag = '1'  order by " +
                "CAST(PARSENAME(ServerIP, 4) AS INT),CAST(PARSENAME(ServerIP, 3) AS INT),CAST(PARSENAME(ServerIP, 2) AS INT),CAST(PARSENAME(ServerIP, 1) AS INT)";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            string cpu, memory, serverip, category, os, status = "";
            int i = 1000;
            int rote = 1;


            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                //Label1.Text = row1["getdate"].ToString() + " ";
                status += row1["status"].ToString();

            }

            System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
            createDiv.ID = "myChart" + i;
            createDiv.InnerHtml = "";
            createDiv.Attributes["style"] = "width:70%;  z-index:100; ";
            //div101.Controls.Add(createDiv);

            Label label = new Label();
            label.ID = "Label" + i;
            //div101.Controls.Add(label);
            StringBuilder st = new StringBuilder();
            st.Append("<script>");
            //st.Append("$('#div2').html('');");
            //st.Append("$('#div2').html('<canvas id='myChart1' style='z-index:200; position:relative; width:100%; height:100%;'></canvas>');");
            st.Append("var ctx2 = document.getElementById('myChart" + i + "').getContext('2d');");
            st.Append("var myChart2 = new Chart(ctx2, {");
            st.Append("type: 'pie',");
            st.Append("data: {");
            st.Append("labels: ['서울','부산','경기','광주'],");
            st.Append("datasets: [{");
            st.Append("data: ['50','50','50','50'],");
            if (status.Contains("Disconnect") == true)
            {
                st.Append("backgroundColor: ['rgba(0, 166, 90, .7)','rgba(243, 104, 88, .7)','rgba(0, 166, 90, .7)','rgba(0, 166, 90, .7)']");
            }
            else
            {
                st.Append("backgroundColor: ['rgba(0, 166, 90, .7)','rgba(0, 166, 90, .7)','rgba(0, 166, 90, .7)','rgba(0, 166, 90, .7)']");
            }

            st.Append("}]");
            st.Append("},");
            st.Append("options: {");
            st.Append("title: {");
            st.Append("display: false,");
            st.Append("text: '상황판',");
            st.Append("fontSize:20");
            st.Append("}," +
                "animation:{duration:0}" +
                "},");
            st.Append("tooltips: {enabled: false}");
            st.Append("});");
            st.Append("</script>");

            label.Text = st.ToString();

            st = null;


        }
        /// <summary>
        /// CPU
        /// </summary>
        private void javascript2()
        {
            string SQL = "select  cpu,memory,serverip,getdate() as getdate, category, os from service where memory is not null and flag = '1' order by " +
                "CAST(PARSENAME(ServerIP, 4) AS INT),CAST(PARSENAME(ServerIP, 3) AS INT),CAST(PARSENAME(ServerIP, 2) AS INT),CAST(PARSENAME(ServerIP, 1) AS INT) ";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            string cpu, memory, serverip, category, os = "";
            int i = 100;
            int rote = 1;


            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                //Label1.Text = row1["getdate"].ToString() + " ";
                cpu = row1["cpu"].ToString();
                memory = row1["memory"].ToString();
                serverip = row1["serverip"].ToString();
                category = row1["category"].ToString();
                os = row1["os"].ToString();

                //캔버스 추가
                if (rote == 4)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:200; margin-top:50px; ";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "','" + os + "')");
                    //div7.Controls.Add(createDiv);
                    //rote = 1;
                }
                if (rote == 3)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:200; margin-top:50px;";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "','" + os + "')");
                    //div5.Controls.Add(createDiv);
                    //rote = 1;
                }
                if (rote == 2)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:200; margin-top:50px;";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "','" + os + "')");
                    //div3.Controls.Add(createDiv);
                    //rote = 3;
                }
                if (rote == 1)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:200;  margin-top:50px; ";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "','" + os + "')");
                    //div1.Controls.Add(createDiv);
                    //rote = 2;
                }


                //라벨 추가
                Label label = new Label();
                label.ID = "Label" + i;
                string roatetion = "";
                if (rote == 4)
                {
                    //div7.Controls.Add(label);
                    rote = 1;
                    roatetion = "4";
                }
                if (rote == 3)
                {
                    //div5.Controls.Add(label);
                    rote = 4;
                }
                if (rote == 2)
                {
                    //div3.Controls.Add(label);
                    rote = 3;
                }
                if (rote == 1 && roatetion == "")
                {
                    //div1.Controls.Add(label);
                    rote = 2;
                }

                StringBuilder st = new StringBuilder();
                //st.Append("<script>");
                //st.Append("var ctx2 = document.getElementById('myChart" + i + "').getContext('2d');");
                //st.Append("var myChart2 = new Chart(ctx2, {");
                //st.Append("type: 'doughnut',");
                //st.Append("data: {");
                ////st.Append("labels: ['CPU','Memory'],");
                //st.Append("labels: ['CPU'],");
                //st.Append("datasets: [{");
                ////st.Append("data:['" + row1["cpu"].ToString() + "','"+ row1["memory"].ToString() + "'],");
                //int total = 100;
                //total = total - Convert.ToInt32(row1["cpu"].ToString());
                //st.Append("data:['" + row1["cpu"].ToString() + "','" + total + "'],");
                ////st.Append("backgroundColor: ['rgba(55, 123, 179, .7)','rgba(41, 186, 156, .7)']");
                //st.Append("backgroundColor: ['rgba(55, 123, 179, .7)']");
                //st.Append("}]");
                //st.Append("},");
                //st.Append("options: {");
                //st.Append("title: {");
                //st.Append("display: true,");
                //st.Append("text: '" + row1["serverip"].ToString() + "'");
                //st.Append("}," +
                //    "animation:{duration:0}" +
                //    "}");
                //st.Append("});");
                //st.Append("</script>");
                st.Append("<img src='server.png'");

                label.Text = st.ToString();

                st = null;
                i++;
            }


        }



        protected void Button2_Click(object sender, EventArgs e)
        {

            //XDocument doc = new XDocument(
            //    new XElement("Root",
            //        new XElement("targetip", HiddenField1.Value),
            //        new XElement("localip", HiddenField3.Value),
            //        new XElement("category", HiddenField2.Value)

            //    )
            //);
            //doc.Save("c:\\SSIM WATCHER\\IP.xml");



            //Process.Start("c:\\SSIM WATCHER\\SocketServer.exe");



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

        private void TBLSET()
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
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; color:white;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "내용";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; color:white;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "시각";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; color:white;";
            TR.Cells.Add(TD);

            TBLLIST.Rows.Add(TR);
            TBLLOAD();
        }

        private void TBLSET2()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            //TR.BackColor = System.Drawing.Color.WhiteSmoke;
            

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = "<a href='Service/Service.aspx' style='color:white'>전체</a>";
            TD.Attributes["style"] = "text-align:center; cursor:pointer; ";
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='white'");
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = "<a href='Service/Service.aspx' style='color:white'>장애</a>";
            TD.Attributes["style"] = "text-align : center; cursor:pointer;";
            TD.Attributes.Add("onmouseover", "this.style.color='green'");
            TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = "관리 장비";
            TD.Attributes["style"] = "text-align : center; cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = "네트워크/보안";
            TD.Attributes["style"] = "text-align : center;  cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('네트워크')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = "서버";
            TD.Attributes["style"] = "text-align : center;  cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('서버')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = "AP";
            TD.Attributes["style"] = "text-align : center;  cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('ap')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = "PC";
            TD.Attributes["style"] = "text-align : center;  cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('pc')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = "관리제외";
            TD.Attributes["style"] = "text-align : center; cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('null')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);



            TBLLIST2.Rows.Add(TR);
            TBLLOAD2();
        }

        private void TBLSET3()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            //TR.BackColor = System.Drawing.Color.WhiteSmoke;

            TD = new TableHeaderCell();
            TD.Width = new Unit("10%");
            TD.Text = "<a href='Log/Service_Log.aspx' style='color:white'>서비스</a>";
            TD.Attributes["style"] = "text-align : center; cursor:pointer;";
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);



            TBLLIST3.Rows.Add(TR);
            TBLLOAD3();
        }

        private void TBLLOAD()
        {
            SQL = "SELECT a.*, b.computer_name from Event_log a, service b WHERE a.serverip = b.serverip order by time desc limit 3";

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD(row["serverip"].ToString(), row["event_log"].ToString(), row["time"].ToString(), row["computer_name"].ToString());

            }
        }

        private void TBLLOAD2()
        {
            //장애
            string error_count = "";
            SQL = "select distinct COUNT(*) AS count from Service where status <> 'Server Connect' and flag = 1 AND category IS NOT null";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            string status = "";
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                error_count = row["count"].ToString();
            }

            SQL = "select count(distinct serverip) as allcount, count(case when category is not null then 1 end) 'Alivecount' , count(case when category = N'서버 장비'  then 1 end) 'Server',count(case when category = N'네트워크/보안 장비'  then 1 end) 'security'," +
                "count(case when category ='PC'  then 1 end) 'pc', count(case when category ='AP'  then 1 end) 'AP' ,count(case when  category is null then 1 end) 'none'  from service_v";
            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET5 = new DataSet();
            ADT5.Fill(DBSET5, "BD5");
            foreach (DataRow row in DBSET5.Tables["BD5"].Rows)
            {
                TBLADD2(row["allcount"].ToString(), row["alivecount"].ToString(), row["Server"].ToString(), row["security"].ToString(), row["pc"].ToString(), row["none"].ToString(), row["ap"].ToString(), error_count);
            }


        }

        private void TBLLOAD3()
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
                Label1.Text = row["getdate"].ToString() + " ";
                Label1.Text = Label1.Text + "(" + row["name"].ToString() + ") ";
                Label1.Text = Label1.Text + row["time"].ToString();
                //Label2.Text = downcount;
                TBLADD3(downcount);
            }
        }
        long a = 1;
        private void TBLADD(string serverip, string event_log, string time, string computer_name)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();
            TR.ForeColor = System.Drawing.Color.White;
            TD = new TableCell();
            TD.Width = 20;

            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 10;
            TD.Text = "<font size='2'>" + serverip.ToString() + " / " + computer_name + "</font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 250;
            TD.Text = "<font size='2'>" + event_log.ToString() + "</font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 100;
            TD.Text = "<font size='2'>" + time.ToString() + "</font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            a++;
            TBLLIST.Rows.Add(TR);

        }


        private void TBLADD2(string allcount, string alivecount, string Server, string security, string pc, string none, string ap, string error_count)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5'><b><a href='Service/Service.aspx' style='color:white'>" + allcount.ToString() + "</a></b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer;";
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 60;
            if (error_count == "0")
            {
                TD.Text = "<font size='5'><b><a href='Service/Service.aspx' style='color:white'>" + error_count.ToString() + "</a></b></font>";
            }
            else
            {
                TD.Text = "<font size='5'><b><a href='Service/Service.aspx' style='color:red'>" + error_count.ToString() + "</a></b></font>";
            }

            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer;";
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5'><b>" + alivecount.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5'><b>" + security.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('네트워크')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5'><b>" + Server.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('서버')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5'><b>" + ap.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('AP')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5'><b>" + pc.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('pc')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<font size='5'><b>" + none.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; color:white;";
            TD.Attributes.Add("Onclick", "go('null')");
            //TD.Attributes.Add("onmouseover", "this.style.color='green'");
            //TD.Attributes.Add("onmouseout", "this.style.color='black'");
            TR.Cells.Add(TD);

            TBLLIST2.Rows.Add(TR);

        }

        private void TBLADD3(string downcount)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 20;
            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; color:white;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 10;
            TD.Text = "<font size='5' ><b>" + downcount.ToString() + "</b></font>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; color:white;";
            TR.Cells.Add(TD);

            TBLLIST3.Rows.Add(TR);
        }

        //다운 서버 장비 top
        private void TBLLOAD4()
        {
            SQL = "select distinct os,name,serverip,serverid,serverpwd,status,network_name,computer_name,category from Service where status <> 'Server Connect' and flag = 1 AND category IS NOT null ";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            string status = "";
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD4(row["os"].ToString(), row["Name"].ToString(), row["serverip"].ToString(),
                     row["serverid"].ToString(), row["serverpwd"].ToString(), row["status"].ToString(), row["network_name"].ToString(), row["computer_name"].ToString()
                     , row["category"].ToString());
                status = row["status"].ToString();
            }
            //if (status.Contains("Disconnect") == true)
            //{
            //    inner2.Attributes["style"] = "visibility:hidden;";
            //    Div7.Attributes["style"] = "visibility:inherit; margin-top:-62px; margin-left:20px;";

            //}
            //else
            //{
            //    inner2.Attributes["style"] = "visibility:inherit; margin-left:20px;";
            //    Div7.Attributes["style"] = "visibility:hidden; margin-top:-62px; margin-left:20px;";
            //}
        }
        long c = 1;
        private void TBLADD4(string os, string name, string serverip, string serverid, string serverpwd, string status
            , string network_name, string computer_name, string category)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();
            TR.ForeColor = System.Drawing.Color.White;
            //TR.BackColor = System.Drawing.Color.Red;
            
            //TR.Font.Bold = true;


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


            c++;
            TBLLIST4.Rows.Add(TR);

        }
        private void TBLSET4()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            //TR.BackColor = System.Drawing.Color.White;
            TR.ForeColor = System.Drawing.Color.White;
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
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이피";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "대역대";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TBLLIST4.Rows.Add(TR);


            TBLLOAD4();
        }
        public class Customer
        {
            public string cpu { get; set; }
            public string memory { get; set; }
            public string serverip { get; set; }

        }
        [WebMethod]
        public static List<Customer> chart1()
        {
            SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer> Parts = new List<Customer>();


            //string count = "";
            //string SQL = "select area_chart from Log_Time_Config";
            //SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            //DataSet DBSET = new DataSet();
            //ADT.Fill(DBSET, "BD4");
            //foreach (DataRow row1 in DBSET.Tables["BD4"].Rows)
            //{

            //    count = row1["area_chart"].ToString();
            //}

            string time1 = DateTime.Now.ToString("HH");
            SqlDataAdapter ADT3 = new SqlDataAdapter("Traffic_top_server", DB);
            ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            ADT3.SelectCommand.Parameters.AddWithValue("@time", time1);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            {
                //그래프
                //temp = row["serverip"].ToString()+",";
                temp += row["serverip"].ToString() + ",";
            }
            serverip = temp.Split(',');


            //string SQL = "select top 5 serverip, round((traffic / 1024 / 1024),2) as traffic, left(CONVERT(CHAR(8), time, 3),2) + '일' + left(CONVERT(CHAR(8), time, 24),5)  as temptime from System_Log_Traffic where serverip = " +
            // "'" + serverip[0] + "' order by time desc";
            string SQL = "select cpu,memory,serverip from service where memory is not null order by memory desc ";
            SqlDataAdapter ADT4 = new SqlDataAdapter(SQL, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts.Add(new Customer
                {
                    cpu = row1["cpu"].ToString(),
                    memory = row1["memory"].ToString(),
                    serverip = row1["serverip"].ToString()
                });
            }



            string iresurlt = "";
            iresurlt = JsonConvert.SerializeObject(Parts);


            return Parts;

        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            XDocument doc = new XDocument(
                 new XElement("Root",
                     //new XElement("targetip", "192.168.0.170"),
                     new XElement("localip", "192.168.0.111")
                     //new XElement("category", "네트워크/보안 장비"),
                     //new XElement("os", "")
                 )
             );
            doc.Save("c:\\SSIM WATCHER\\IP.xml");

            Process.Start("c:\\SSIM WATCHER\\SocketServer.exe");
        }
    }
}