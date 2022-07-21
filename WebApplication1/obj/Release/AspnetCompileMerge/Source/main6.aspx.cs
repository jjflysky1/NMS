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
    public partial class main6 : System.Web.UI.Page
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