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

namespace WebApplication1
{
    public partial class main : System.Web.UI.Page
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

            XDocument doc = new XDocument(
                new XElement("Root",
                    new XElement("targetip", HiddenField1.Value),
                    new XElement("localip", HiddenField3.Value),
                    new XElement("category", HiddenField2.Value)

                )
            );
            doc.Save("c:\\SSIM WATCHER\\IP.xml");



            //Process.Start("c:\\SSIM WATCHER\\SocketServer.exe");



            //Response.Redirect("Service/Service.aspx?nowpage=" + 1 + "&type1=5"+ "&search=" + HiddenField1.Value);
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
    }
}