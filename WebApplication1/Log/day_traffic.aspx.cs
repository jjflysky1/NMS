using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace WebApplication1
{


    public partial class day_traffic : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                //Search.Text = Request["search"];
                //DropDownList1.SelectedValue = Request["type"];
                startdate.Text = Request["startdate"];
                enddate.Text = Request["enddate"];
                
                serverip.Value =  Request["serverip"];
                if (startdate.Text == "")
                {
                    startdate.Text = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
                }
                else
                {

                }
                if (enddate.Text == "")
                {
                    
                }
                else
                {
                    
                }
            }
            else
            {

            }
            enddate.Text = Convert.ToDateTime(startdate.Text).AddDays(1).ToString("yyyy-MM-dd");
            //chart();
            //chart2();
            //chart3();
            //chart4();




            if (Request.Cookies["userinfo"] == null)
            {
                Label3.Text = "<script>alert('로그인 해주세요');</script>";
                Response.Redirect("/Default.aspx");
            }
            TextBox5.Value = Request["id"];



        }
        public class Customer
        {
            public string serverip { get; set; }
            public string traffic { get; set; }
            public string time { get; set; }
        }
        [WebMethod]
        public static List<Customer> chart1(string serverip , string startdate , string enddate)
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer> Parts = new List<Customer>();
         

            string SQL = "select  serverip, round(traffic,2) as traffic, time from Secure_Log " +
                   "where serverip like '%" + serverip + "%' and  time between '" + startdate + "' and '" + enddate + "' order by traffic desc limit 10";
            MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string temp = "";

            foreach (DataRow row1 in DBSET3.Tables["BD3"].Rows)
            {

                temp = temp + row1["serverip"].ToString() + ",";

            }
            string[] temp_serverip = { };
            temp_serverip = temp.Split(',');


            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + temp_serverip[0] + "'";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            string mac = "";
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }


            string SQL2 = "SELECT serverip, round((avg(traffic) / 1024 / 1024),2) as traffic, concat(DATE_FORMAT(time, '%e'), '일', DATE_FORMAT(time, '%T')) as temptime  FROM secure_log where " +
                " serverip = '" + temp_serverip[0] + "' and  time between '" + startdate + "' and '" + enddate + "' group by UNIX_TIMESTAMP(time) DIV 60 order by time desc limit 60";
            //and  time between '" + startdate + "' and '" + enddate + "' group by UNIX_TIMESTAMP(time) DIV 60
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            //temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts.Add(new Customer
                {
                    serverip = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic = row1["traffic"].ToString(),
                    time = row1["temptime"].ToString()
                });
            }


            string iresurlt = "";
            iresurlt = JsonConvert.SerializeObject(Parts);


            return Parts;

        }
        public class Customer2
        {
            public string serverip2 { get; set; }
            public string traffic2 { get; set; }
            public string time2 { get; set; }
        }
        [WebMethod]
        public static List<Customer2> chart2(string serverip, string startdate, string enddate)
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer2> Parts2 = new List<Customer2>();



            string SQL = "select  serverip, round(traffic,2) as traffic, time from Secure_Log " +
                           "where serverip like '%" + serverip + "%' and  time between '" + startdate + "' and '" + enddate + "' order by traffic desc limit 10";
            MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string temp = "";

            foreach (DataRow row1 in DBSET3.Tables["BD3"].Rows)
            {

                temp = temp + row1["serverip"].ToString() + ",";

            }
            string[] temp_serverip = { };
            temp_serverip = temp.Split(',');



            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + temp_serverip[0] + "'";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            string mac = "";
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }

            string SQL2 = "SELECT serverip, round((avg(traffic) / 1024 / 1024),2) as traffic, concat(DATE_FORMAT(time, '%e'), '일', DATE_FORMAT(time, '%T')) as temptime  FROM secure_log where " +
                " serverip = '" + temp_serverip[0] + "' and  time between '" + startdate + "' and '" + enddate + "'  group by UNIX_TIMESTAMP(time) DIV 300 order by time desc limit 60";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            //temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts2.Add(new Customer2
                {
                    serverip2 = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic2 = row1["traffic"].ToString(),
                    time2 = row1["temptime"].ToString()
                });
            }




            string iresurlt = "";
            iresurlt = JsonConvert.SerializeObject(Parts2);


            return Parts2;

        }

        public class Customer3
        {
            public string serverip3 { get; set; }
            public string traffic3 { get; set; }
            public string time3 { get; set; }
        }
        [WebMethod]
        public static List<Customer3> chart3(string serverip, string startdate, string enddate)
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer3> Parts3 = new List<Customer3>();

         

            string SQL = "select serverip, round(traffic,2) as traffic, time from Secure_Log " +
               "where serverip like '%" + serverip + "%' and  time between '" + startdate + "' and '" + enddate + "' order by traffic desc limit 10";
            MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string temp = "";

            foreach (DataRow row1 in DBSET3.Tables["BD3"].Rows)
            {

                temp = temp + row1["serverip"].ToString() + ",";

            }
            string[] temp_serverip = { };
            temp_serverip = temp.Split(',');


            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + temp_serverip[2] + "'";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            string mac = "";
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }

            string SQL2 = "SELECT serverip, round((avg(traffic) / 1024 / 1024),2) as traffic, concat(DATE_FORMAT(time, '%e'), '일', DATE_FORMAT(time, '%T')) as temptime  FROM secure_log where " +
                " serverip = '" + temp_serverip[0] + "' and  time between '" + startdate + "' and '" + enddate + "'  group by UNIX_TIMESTAMP(time) DIV 1800 order by time desc limit 60";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            //temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts3.Add(new Customer3
                {
                    serverip3 = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic3 = row1["traffic"].ToString(),
                    time3 = row1["temptime"].ToString()
                });
            }




            string iresurlt = "";
            iresurlt = JsonConvert.SerializeObject(Parts3);


            return Parts3;

        }
        public class Customer4
        {
            public string serverip4 { get; set; }
            public string traffic4 { get; set; }
            public string time4 { get; set; }
        }
        [WebMethod]
        public static List<Customer4> chart4(string serverip, string startdate, string enddate)
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer4> Parts4 = new List<Customer4>();

      

            string SQL = "select serverip, round(traffic,2) as traffic, time from Secure_Log " +
               "where serverip like '%" + serverip + "%' and  time between '" + startdate + "' and '" + enddate + "' order by traffic desc limit 10";
            MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string temp = "";

            foreach (DataRow row1 in DBSET3.Tables["BD3"].Rows)
            {

                temp = temp + row1["serverip"].ToString() + ",";

            }
            string[] temp_serverip = { };
            temp_serverip = temp.Split(',');

            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + temp_serverip[2] + "'";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            string mac = "";
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }

            string SQL2 = "select concat(DATE_FORMAT(time, '%e'), '일', DATE_FORMAT(time, '%T')) as temptime, round((avg(traffic) / 1024 / 1024),2) as traffic, serverip from secure_log where " +
               "serverip = '" + temp_serverip[0] + "' and  time between '" + startdate + "' and '" + enddate + "'  group by UNIX_TIMESTAMP(time) DIV 3600 order by time desc limit 60";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            //temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts4.Add(new Customer4
                {
                    serverip4 = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic4 = row1["traffic"].ToString(),
                    time4 = row1["temptime"].ToString()
                });
            }




            string iresurlt = "";
            iresurlt = JsonConvert.SerializeObject(Parts4);


            return Parts4;

        }






        public int abcb = 0;
        public void chart()
        {
            string time1 = DateTime.Now.ToString("HH");
            string IN = "";
            //string time2 = DateTime.Now.ToString(row2["time"].ToString());
            //SQL = "select top 1 serverip, round(traffic,2) as traffic from System_Log_Traffic where serverip " +
            //    "IN (select serverip from Service where flag = '1') " +
            //    " and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = "+ time1.ToString() +"" +
            //    //" and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, now()), 121), 12, 2)" +
            //    " and left(time, 10) = LEFT(now(), 10) order by traffic desc";
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            ADT3.SelectCommand.Parameters.AddWithValue("@time", time1);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            {
                temp = temp + row["serverip"].ToString() + ",";

            }
            serverip = temp.ToString().Split(',');

            if (serverip[0] == serverip[1])
            {
                BindChart(serverip[0], serverip[1 + 1], serverip[2 + 1]);
                if (serverip[1 + 1] == serverip[2 + 1])
                {
                    BindChart(serverip[0], serverip[1 + 1], serverip[2 + 2]);
                }
            }
            else if (serverip[0] == serverip[2])
            {
                BindChart(serverip[0], serverip[1], serverip[2 + 1]);
                if (serverip[1] == serverip[2 + 1])
                {
                    BindChart(serverip[0], serverip[1], serverip[2 + 2]);
                }
            }
            else if (serverip[1] == serverip[2])
            {
                BindChart(serverip[0], serverip[1], serverip[2 + 1]);
                if (serverip[0] == serverip[2 + 1])
                {
                    BindChart(serverip[0], serverip[1], serverip[2 + 2]);
                }
            }
            else
            {
                BindChart(serverip[0], serverip[1], serverip[2]);
            }
        }
        public void char2t2()
        {
            string time1 = DateTime.Now.ToString("HH");
            string IN = "";
            //string time2 = DateTime.Now.ToString(row2["time"].ToString());
            //SQL = "select top 1 serverip, round(traffic,2) as traffic from System_Log_Traffic where serverip " +
            //    "IN (select serverip from Service where flag = '1') " +
            //    " and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = "+ time1.ToString() +"" +
            //    //" and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, now()), 121), 12, 2)" +
            //    " and left(time, 10) = LEFT(now(), 10) order by traffic desc";
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            ADT3.SelectCommand.Parameters.AddWithValue("@time", time1);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            {
                temp = temp + row["serverip"].ToString() + ",";

            }
            serverip = temp.ToString().Split(',');

            if (serverip[0] == serverip[1])
            {
                BindChart2(serverip[0], serverip[1 + 1], serverip[2 + 1]);
                if (serverip[1 + 1] == serverip[2 + 1])
                {
                    BindChart2(serverip[0], serverip[1 + 1], serverip[2 + 2]);
                }
            }
            else if (serverip[0] == serverip[2])
            {
                BindChart2(serverip[0], serverip[1], serverip[2 + 1]);
                if (serverip[1] == serverip[2 + 1])
                {
                    BindChart2(serverip[0], serverip[1], serverip[2 + 2]);
                }
            }
            else if (serverip[1] == serverip[2])
            {
                BindChart2(serverip[0], serverip[1], serverip[2 + 1]);
                if (serverip[0] == serverip[2 + 1])
                {
                    BindChart2(serverip[0], serverip[1], serverip[2 + 2]);
                }
            }
            else
            {
                BindChart2(serverip[0], serverip[1], serverip[2]);
            }
        }
        public void chart33()
        {
            string time1 = DateTime.Now.ToString("HH");
            string IN = "";
            //string time2 = DateTime.Now.ToString(row2["time"].ToString());
            //SQL = "select top 1 serverip, round(traffic,2) as traffic from System_Log_Traffic where serverip " +
            //    "IN (select serverip from Service where flag = '1') " +
            //    " and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = "+ time1.ToString() +"" +
            //    //" and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, now()), 121), 12, 2)" +
            //    " and left(time, 10) = LEFT(now(), 10) order by traffic desc";
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            ADT3.SelectCommand.Parameters.AddWithValue("@time", time1);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            {
                temp = temp + row["serverip"].ToString() + ",";

            }
            serverip = temp.ToString().Split(',');

            if (serverip[0] == serverip[1])
            {
                BindChart3(serverip[0], serverip[1 + 1], serverip[2 + 1]);
                if (serverip[1 + 1] == serverip[2 + 1])
                {
                    BindChart3(serverip[0], serverip[1 + 1], serverip[2 + 2]);
                }
            }
            else if (serverip[0] == serverip[2])
            {
                BindChart3(serverip[0], serverip[1], serverip[2 + 1]);
                if (serverip[1] == serverip[2 + 1])
                {
                    BindChart3(serverip[0], serverip[1], serverip[2 + 2]);
                }
            }
            else if (serverip[1] == serverip[2])
            {
                BindChart3(serverip[0], serverip[1], serverip[2 + 1]);
                if (serverip[0] == serverip[2 + 1])
                {
                    BindChart3(serverip[0], serverip[1], serverip[2 + 2]);
                }
            }
            else
            {
                BindChart3(serverip[0], serverip[1], serverip[2]);
            }
        }
        public void chart4()
        {
            string time1 = DateTime.Now.ToString("HH");
            string IN = "";
            //string time2 = DateTime.Now.ToString(row2["time"].ToString());
            //SQL = "select top 1 serverip, round(traffic,2) as traffic from System_Log_Traffic where serverip " +
            //    "IN (select serverip from Service where flag = '1') " +
            //    " and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = "+ time1.ToString() +"" +
            //    //" and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, now()), 121), 12, 2)" +
            //    " and left(time, 10) = LEFT(now(), 10) order by traffic desc";
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            ADT3.SelectCommand.Parameters.AddWithValue("@time", time1);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            {
                temp = temp + row["serverip"].ToString() + ",";

            }
            serverip = temp.ToString().Split(',');

            if (serverip[0] == serverip[1])
            {
                BindChart4(serverip[0], serverip[1 + 1], serverip[2 + 1]);
                if (serverip[1 + 1] == serverip[2 + 1])
                {
                    BindChart4(serverip[0], serverip[1 + 1], serverip[2 + 2]);
                }
            }
            else if (serverip[0] == serverip[2])
            {
                BindChart4(serverip[0], serverip[1], serverip[2 + 1]);
                if (serverip[1] == serverip[2 + 1])
                {
                    BindChart4(serverip[0], serverip[1], serverip[2 + 2]);
                }
            }
            else if (serverip[1] == serverip[2])
            {
                BindChart4(serverip[0], serverip[1], serverip[2 + 1]);
                if (serverip[0] == serverip[2 + 1])
                {
                    BindChart4(serverip[0], serverip[1], serverip[2 + 2]);
                }
            }
            else
            {
                BindChart4(serverip[0], serverip[1], serverip[2]);
            }
        }

        private DataTable GetChartData(string query)
        {

            MySqlCommand cmd = new MySqlCommand(query, DB);
            MySqlDataAdapter ADT = new MySqlDataAdapter();
            DataTable DBSET = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = DB;
            ADT.SelectCommand = cmd;
            ADT.Fill(DBSET);
            ADT.Dispose();
            return DBSET;
        }

        private DataTable GetChartData2(string query2)
        {

            MySqlCommand cmd2 = new MySqlCommand(query2, DB);
            MySqlDataAdapter ADT2 = new MySqlDataAdapter();
            DataTable DBSET2 = new DataTable();
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = DB;
            ADT2.SelectCommand = cmd2;
            ADT2.Fill(DBSET2);
            ADT2.Dispose();
            return DBSET2;
        }

        private DataTable GetChartData3(string query3)
        {

            MySqlCommand cmd3 = new MySqlCommand(query3, DB);
            MySqlDataAdapter ADT3 = new MySqlDataAdapter();
            DataTable DBSET3 = new DataTable();
            cmd3.CommandType = CommandType.Text;
            cmd3.Connection = DB;
            ADT3.SelectCommand = cmd3;
            ADT3.Fill(DBSET3);
            ADT3.Dispose();
            return DBSET3;
        }

        private void BindChart(string serverip, string serverip1, string serverip2)
        {
            //TextBox1.Text = serverip;
            //TextBox2.Text = serverip1;
            //TextBox3.Text = serverip2;

            if (serverip == "")
            {
                serverip = "0";
            }
            if (serverip1 == "")
            {
                serverip1 = serverip;

            }
            if (serverip2 == "")
            {
                serverip2 = serverip;
            }

            //Label1.Text = "";
            string query = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where " +
                 "serverip = '" + serverip + "' and  time between '" + startdate.Text + "' and '" + enddate.Text + "' order by time desc";

            DataTable dt = GetChartData(query);

            string query2 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where  " +
               "serverip = '" + serverip1 + "' and time between '" + startdate.Text + "' and '" + enddate.Text + "' order by time desc";

            DataTable dt2 = GetChartData(query2);

            string query3 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where  " +
           "serverip = '" + serverip2 + "' and time between '" + startdate.Text + "' and '" + enddate.Text + "'  order by time desc";

            DataTable dt3 = GetChartData(query3);

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];

            string[] x2 = new string[dt2.Rows.Count];
            decimal[] y2 = new decimal[dt2.Rows.Count];

            string[] x3 = new string[dt3.Rows.Count];
            decimal[] y3 = new decimal[dt3.Rows.Count];


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    x[i] = dt.Rows[j][0].ToString();
            //    y[i] = Convert.ToInt32(dt.Rows[j][1]);
            //    j--;
            //}
            string name = dt.Rows[0][2].ToString();
            string name1 = dt2.Rows[0][2].ToString();
            string name2 = dt3.Rows[0][2].ToString();
            //LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = name, Data = y });
            //LineChart1.CategoriesAxis = string.Join(",", x);
            //LineChart1.ChartTitle = string.Format("Traffic
            //LineChart1.Width = 0;
            //LineChart1.BorderWidth = 0;

            title.InnerText = "네트워크/보안 Traffic 평균 차트";
            StringBuilder st = new StringBuilder();
            st.Append("<script type='text/javascript'>");
            //st.Append("var prm = Sys.WebForms.PageRequestManager.getInstance();");
            //st.Append("prm.add_beginRequest(BeginRequestHandler);");
            //st.Append("function BeginRequestHandler(sender, args){");
            //st.Append("prm._scrollPosition = null; }");
            //st.Append("prm.add_endRequest(function () {");
            st.Append("new Morris.Line({");
            st.Append("element: 'myfirstchart',");
            st.Append("data: [");
            int rowcount = 0;

            decimal[] arrayNumbers = { dt.Rows.Count, dt2.Rows.Count, dt3.Rows.Count };
            decimal arryNUmbersmall = arrayNumbers.Min();
            //Console.WriteLine(arrayNumbersBiggest);

            rowcount = Convert.ToInt32(arryNUmbersmall);



            decimal a = 0;
            decimal b = 0;
            decimal c = 0;

            int j = Convert.ToInt32(rowcount.ToString()) - 1;
            for (int i = 0; i < Convert.ToInt32(rowcount.ToString()); i++)
            {
                x[i] = dt.Rows[j][0].ToString();
                y[i] = Convert.ToInt64(dt.Rows[j][1]);
                x2[i] = dt2.Rows[j][0].ToString();
                y2[i] = Convert.ToInt64(dt2.Rows[j][1]);
                x3[i] = dt3.Rows[j][0].ToString();
                y3[i] = Convert.ToInt64(dt3.Rows[j][1]);

                st.Append("{ time: '" + x[i].Substring(5, 8) + " Hour', " +
                    " a: " + Math.Round((y[i] / 1024 / 1024), 2) + "," +
                    " b: " + Math.Round((y2[i] / 1024 / 1024), 2) + "," +
                    " c: " + Math.Round((y3[i] / 1024 / 1024), 2) + "},");

                j--;

                
                if (a < y[i])
                {
                    a = y[i];
                }
                if (b < y[i])
                {
                    b = y2[i];
                }
                if (c < y[i])
                {
                    c = y3[i];
                }
                //Label40.Text = name +": " + Math.Round((a / 1024 / 1024), 2) + " Mb/s";
                //Label41.Text = name1 + ": " + Math.Round((b / 1024 / 1024), 2) + " Mb/s";
                //Label42.Text = name2 + ": " + Math.Round((c / 1024 / 1024), 2) + " Mb/s";
            }
           

            st.Append("],");

            st.Append("xkey: 'time',");
            st.Append("ykeys: ['a','b','c'],");
            st.Append("parseTime: false,");
            st.Append("labels: ['" + name + "','" + name1 + "','" + name2 + "'],");
            st.Append(" lineColors: ['#3c8dbc','#fc8710','#819C79'],");
            st.Append("pointSize: 0,");
            st.Append("hideHover:true,");
            st.Append(" postUnits: ' Mb/s',");
            st.Append("fillOpacity: .1,");
            st.Append("resize: true");
            st.Append("   });");
            //st.Append("});");
            //st.Append("$('#myfirstchart').click(function() {");
            //st.Append("var url='../Service/Service_list.aspx?serverip=" + serverip.ToString() + "';");
            //st.Append("$(location).attr('href', url); ");
            //st.Append("});");
            st.Append("</script>");

            //Label1.Text = st.ToString();

            st = null;

        }

        private void BindChart2(string serverip, string serverip1, string serverip2)
        {
            //TextBox1.Text = serverip;
            //TextBox2.Text = serverip1;
            //TextBox3.Text = serverip2;

            if (serverip == "")
            {
                serverip = "0";
            }
            if (serverip1 == "")
            {
                serverip1 = serverip;

            }
            if (serverip2 == "")
            {
                serverip2 = serverip;
            }

            //Label2.Text = "";
            string query = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where " +
                  "serverip = '" + serverip + "' and  time between '" + startdate.Text + "' and '" + enddate.Text + "' order by time desc";

            DataTable dt = GetChartData(query);

            string query2 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where  " +
               "serverip = '" + serverip1 + "' and time between '" + startdate.Text + "' and '" + enddate.Text + "' order by time desc";

            DataTable dt2 = GetChartData(query2);

            string query3 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where  " +
           "serverip = '" + serverip2 + "' and time between '" + startdate.Text + "' and '" + enddate.Text + "'  order by time desc";

            DataTable dt3 = GetChartData(query3);

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];

            string[] x2 = new string[dt2.Rows.Count];
            decimal[] y2 = new decimal[dt2.Rows.Count];

            string[] x3 = new string[dt3.Rows.Count];
            decimal[] y3 = new decimal[dt3.Rows.Count];


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    x[i] = dt.Rows[j][0].ToString();
            //    y[i] = Convert.ToInt32(dt.Rows[j][1]);
            //    j--;
            //}
            string name = dt.Rows[0][2].ToString();
            string name1 = dt2.Rows[0][2].ToString();
            string name2 = dt3.Rows[0][2].ToString();
            //LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = name, Data = y });
            //LineChart1.CategoriesAxis = string.Join(",", x);
            //LineChart1.ChartTitle = string.Format("Traffic
            //LineChart1.Width = 0;
            //LineChart1.BorderWidth = 0;

            title.InnerText = "네트워크/보안 Traffic 평균 차트";
            StringBuilder st2 = new StringBuilder();
            st2.Append("<script type='text/javascript'>");
            //st.Append("var prm = Sys.WebForms.PageRequestManager.getInstance();");
            //st.Append("prm.add_beginRequest(BeginRequestHandler);");
            //st.Append("function BeginRequestHandler(sender, args){");
            //st.Append("prm._scrollPosition = null; }");
            //st.Append("prm.add_endRequest(function () {");
            st2.Append("new Morris.Line({");
            st2.Append("element: 'mysecchart',");
            st2.Append("data: [");
            int rowcount = 0;

            decimal[] arrayNumbers = { dt.Rows.Count, dt2.Rows.Count, dt3.Rows.Count };
            decimal arryNUmbersmall = arrayNumbers.Min();
            //Console.WriteLine(arrayNumbersBiggest);

            rowcount = Convert.ToInt32(arryNUmbersmall);




            decimal a = 0;
            decimal b = 0;
            decimal c = 0;

            int j = Convert.ToInt32(rowcount.ToString()) - 1;
            for (int i = 0; i < Convert.ToInt32(rowcount.ToString()); i++)
            {
                x[i] = dt.Rows[j][0].ToString();
                y[i] = Convert.ToInt64(dt.Rows[j][1]);
                x2[i] = dt2.Rows[j][0].ToString();
                y2[i] = Convert.ToInt64(dt2.Rows[j][1]);
                x3[i] = dt3.Rows[j][0].ToString();
                y3[i] = Convert.ToInt64(dt3.Rows[j][1]);

                st2.Append("{ time: '" + x[i].Substring(5, 8) + " Hour', " +
                    " a: " + Math.Round((y[i] / 1024 / 1024), 2) + "," +
                    " b: " + Math.Round((y2[i] / 1024 / 1024), 2) + "," +
                    " c: " + Math.Round((y3[i] / 1024 / 1024), 2) + "},");

                j--;


                if (a < y[i])
                {
                    a = y[i];
                }
                if (b < y[i])
                {
                    b = y2[i];
                }
                if (c < y[i])
                {
                    c = y3[i];
                }
                //Label43.Text = name + ": " + Math.Round((a / 1024 / 1024), 2) + " Mb/s";
                //Label44.Text = name1 + ": " + Math.Round((b / 1024 / 1024), 2) + " Mb/s";
                //Label45.Text = name2 + ": " + Math.Round((c / 1024 / 1024), 2) + " Mb/s";
            }
            st2.Append("],");

            st2.Append("xkey: 'time',");
            st2.Append("ykeys: ['a','b','c'],");
            st2.Append("parseTime: false,");
            st2.Append("labels: ['" + name + "','" + name1 + "','" + name2 + "'],");
            st2.Append(" lineColors: ['#3c8dbc','#fc8710','#819C79'],");
            st2.Append("pointSize: 0,");
            st2.Append("hideHover:true,");
            st2.Append(" postUnits: ' Mb/s',");
            st2.Append("fillOpacity: .1,");
            st2.Append("resize: true");
            st2.Append("   });");
            //st.Append("});");
            //st.Append("$('#myfirstchart').click(function() {");
            //st.Append("var url='../Service/Service_list.aspx?serverip=" + serverip.ToString() + "';");
            //st.Append("$(location).attr('href', url); ");
            //st.Append("});");
            st2.Append("</script>");

            //Label2.Text = st2.ToString();

            st2 = null;

        }
        private void BindChart3(string serverip, string serverip1, string serverip2)
        {
            //TextBox1.Text = serverip;
            //TextBox2.Text = serverip1;
            //TextBox3.Text = serverip2;

            if (serverip == "")
            {
                serverip = "0";
            }
            if (serverip1 == "")
            {
                serverip1 = serverip;

            }
            if (serverip2 == "")
            {
                serverip2 = serverip;
            }

            //Label7.Text = "";
            string query = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where " +
               "serverip = '" + serverip + "' and  time between '" + startdate.Text + "' and '" + enddate.Text + "' order by time desc";

            DataTable dt = GetChartData(query);

            string query2 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where  " +
               "serverip = '" + serverip1 + "' and time between '" + startdate.Text + "' and '" + enddate.Text + "' order by time desc";

            DataTable dt2 = GetChartData(query2);

            string query3 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where  " +
           "serverip = '" + serverip2 + "' and time between '" + startdate.Text + "' and '" + enddate.Text + "'  order by time desc";

            DataTable dt3 = GetChartData(query3);

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];

            string[] x2 = new string[dt2.Rows.Count];
            decimal[] y2 = new decimal[dt2.Rows.Count];

            string[] x3 = new string[dt3.Rows.Count];
            decimal[] y3 = new decimal[dt3.Rows.Count];


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    x[i] = dt.Rows[j][0].ToString();
            //    y[i] = Convert.ToInt32(dt.Rows[j][1]);
            //    j--;
            //}
            string name = dt.Rows[0][2].ToString();
            string name1 = dt2.Rows[0][2].ToString();
            string name2 = dt3.Rows[0][2].ToString();
            //LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = name, Data = y });
            //LineChart1.CategoriesAxis = string.Join(",", x);
            //LineChart1.ChartTitle = string.Format("Traffic
            //LineChart1.Width = 0;
            //LineChart1.BorderWidth = 0;

            title.InnerText = "네트워크/보안 Traffic 평균 차트";
            StringBuilder st2 = new StringBuilder();
            st2.Append("<script type='text/javascript'>");
            //st.Append("var prm = Sys.WebForms.PageRequestManager.getInstance();");
            //st.Append("prm.add_beginRequest(BeginRequestHandler);");
            //st.Append("function BeginRequestHandler(sender, args){");
            //st.Append("prm._scrollPosition = null; }");
            //st.Append("prm.add_endRequest(function () {");
            st2.Append("new Morris.Line({");
            st2.Append("element: 'mythirdchart',");
            st2.Append("data: [");
            int rowcount = 0;

            decimal[] arrayNumbers = { dt.Rows.Count, dt2.Rows.Count, dt3.Rows.Count };
            decimal arryNUmbersmall = arrayNumbers.Min();
            //Console.WriteLine(arrayNumbersBiggest);

            rowcount = Convert.ToInt32(arryNUmbersmall);



            decimal a = 0;
            decimal b = 0;
            decimal c = 0;

            int j = Convert.ToInt32(rowcount.ToString()) - 1;
            for (int i = 0; i < Convert.ToInt32(rowcount.ToString()); i++)
            {
                x[i] = dt.Rows[j][0].ToString();
                y[i] = Convert.ToInt64(dt.Rows[j][1]);
                x2[i] = dt2.Rows[j][0].ToString();
                y2[i] = Convert.ToInt64(dt2.Rows[j][1]);
                x3[i] = dt3.Rows[j][0].ToString();
                y3[i] = Convert.ToInt64(dt3.Rows[j][1]);

                st2.Append("{ time: '" + x[i].Substring(5, 8) + " Hour', " +
                    " a: " + Math.Round((y[i] / 1024 / 1024), 2) + "," +
                    " b: " + Math.Round((y2[i] / 1024 / 1024), 2) + "," +
                    " c: " + Math.Round((y3[i] / 1024 / 1024), 2) + "},");

                j--;

                if (a < y[i])
                {
                    a = y[i];
                }
                if (b < y[i])
                {
                    b = y2[i];
                }
                if (c < y[i])
                {
                    c = y3[i];
                }
                //Label46.Text = name + ": " + Math.Round((a / 1024 / 1024), 2) + " Mb/s";
                //Label47.Text = name1 + ": " + Math.Round((b / 1024 / 1024), 2) + " Mb/s";
                //Label48.Text = name2 + ": " + Math.Round((c / 1024 / 1024), 2) + " Mb/s";
            }
            st2.Append("],");

            st2.Append("xkey: 'time',");
            st2.Append("ykeys: ['a','b','c'],");
            st2.Append("parseTime: false,");
            st2.Append("labels: ['" + name + "','" + name1 + "','" + name2 + "'],");
            st2.Append(" lineColors: ['#3c8dbc','#fc8710','#819C79'],");
            st2.Append("pointSize: 0,");
            st2.Append("hideHover:true,");
            st2.Append(" postUnits: ' Mb/s',");
            st2.Append("fillOpacity: .1,");
            st2.Append("resize: true");
            st2.Append("   });");
            //st.Append("});");
            //st.Append("$('#myfirstchart').click(function() {");
            //st.Append("var url='../Service/Service_list.aspx?serverip=" + serverip.ToString() + "';");
            //st.Append("$(location).attr('href', url); ");
            //st.Append("});");
            st2.Append("</script>");

            //Label7.Text = st2.ToString();

            st2 = null;

        }
        private void BindChart4(string serverip, string serverip1, string serverip2)
        {
            //TextBox1.Text = serverip;
            //TextBox2.Text = serverip1;
            //TextBox3.Text = serverip2;

            if (serverip == "")
            {
                serverip = "0";
            }
            if (serverip1 == "")
            {
                serverip1 = serverip;

            }
            if (serverip2 == "")
            {
                serverip2 = serverip;
            }

            //Label8.Text = "";
            string query = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where " +
                "serverip = '" + serverip + "' and  time between '"+ startdate.Text +"' and '"+ enddate.Text +"' order by time desc";

            DataTable dt = GetChartData(query);

            string query2 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where  " +
               "serverip = '" + serverip1 + "' and time between '" + startdate.Text + "' and '" + enddate.Text + "' order by time desc";

            DataTable dt2 = GetChartData(query2);

            string query3 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where  " +
           "serverip = '" + serverip2 + "' and time between '" + startdate.Text + "' and '" + enddate.Text + "'  order by time desc";

            DataTable dt3 = GetChartData(query3);

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];

            string[] x2 = new string[dt2.Rows.Count];
            decimal[] y2 = new decimal[dt2.Rows.Count];

            string[] x3 = new string[dt3.Rows.Count];
            decimal[] y3 = new decimal[dt3.Rows.Count];


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    x[i] = dt.Rows[j][0].ToString();
            //    y[i] = Convert.ToInt32(dt.Rows[j][1]);
            //    j--;
            //}
            string name = dt.Rows[0][2].ToString();
            string name1 = dt2.Rows[0][2].ToString();
            string name2 = dt3.Rows[0][2].ToString();
            //LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = name, Data = y });
            //LineChart1.CategoriesAxis = string.Join(",", x);
            //LineChart1.ChartTitle = string.Format("Traffic
            //LineChart1.Width = 0;
            //LineChart1.BorderWidth = 0;

            title.InnerText = "네트워크/보안 Traffic 평균 차트";
            StringBuilder st2 = new StringBuilder();
            st2.Append("<script type='text/javascript'>");
            //st.Append("var prm = Sys.WebForms.PageRequestManager.getInstance();");
            //st.Append("prm.add_beginRequest(BeginRequestHandler);");
            //st.Append("function BeginRequestHandler(sender, args){");
            //st.Append("prm._scrollPosition = null; }");
            //st.Append("prm.add_endRequest(function () {");
            st2.Append("new Morris.Line({");
            st2.Append("element: 'myqurtchart',");
            st2.Append("data: [");
            int rowcount = 0;

            decimal[] arrayNumbers = { dt.Rows.Count, dt2.Rows.Count, dt3.Rows.Count };
            decimal arryNUmbersmall = arrayNumbers.Min();
            //Console.WriteLine(arrayNumbersBiggest);

            rowcount = Convert.ToInt32(arryNUmbersmall);



              decimal a = 0;
            decimal b = 0;
            decimal c = 0;

            int j = Convert.ToInt32(rowcount.ToString()) - 1;
            for (int i = 0; i < Convert.ToInt32(rowcount.ToString()); i++)
            {
                x[i] = dt.Rows[j][0].ToString();
                y[i] = Convert.ToInt64(dt.Rows[j][1]);
                x2[i] = dt2.Rows[j][0].ToString();
                y2[i] = Convert.ToInt64(dt2.Rows[j][1]);
                x3[i] = dt3.Rows[j][0].ToString();
                y3[i] = Convert.ToInt64(dt3.Rows[j][1]);

                st2.Append("{ time: '" + x[i].Substring(5, 8) + " Hour', " +
                    " a: " + Math.Round((y[i] / 1024 / 1024), 2) + "," +
                    " b: " + Math.Round((y2[i] / 1024 / 1024), 2) + "," +
                    " c: " + Math.Round((y3[i] / 1024 / 1024), 2) + "},");

                j--;

                if (a < y[i])
                {
                    a = y[i];
                }
                if (b < y[i])
                {
                    b = y2[i];
                }
                if (c < y[i])
                {
                    c = y3[i];
                }
                //Label49.Text = name + ": " + Math.Round((a / 1024 / 1024), 2) + " Mb/s";
                //Label50.Text = name1 + ": " + Math.Round((b / 1024 / 1024), 2) + " Mb/s";
                //Label51.Text = name2 + ": " + Math.Round((c / 1024 / 1024), 2) + " Mb/s";
            }
            st2.Append("],");

            st2.Append("xkey: 'time',");
            st2.Append("ykeys: ['a','b','c'],");
            st2.Append("parseTime: false,");
            st2.Append("labels: ['" + name + "','" + name1 + "','" + name2 + "'],");
            st2.Append(" lineColors: ['#3c8dbc','#fc8710','#819C79'],");
            st2.Append("pointSize: 0,");
            st2.Append("hideHover:true,");
            st2.Append(" postUnits: ' Mb/s',");
            st2.Append("fillOpacity: .1,");
            st2.Append("resize: true");
            st2.Append("   });");
            //st.Append("});");
            //st.Append("$('#myfirstchart').click(function() {");
            //st.Append("var url='../Service/Service_list.aspx?serverip=" + serverip.ToString() + "';");
            //st.Append("$(location).attr('href', url); ");
            //st.Append("});");
            st2.Append("</script>");

            //Label8.Text = st2.ToString();

            st2 = null;

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("day_traffic.aspx?&startdate=" + startdate.Text + "&enddate=" + enddate.Text + "");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
           // if(RadioButton1.Checked == true)
           // {
           //     DB.Open();
           //     MySqlCommand cmd = new MySqlCommand();
           //     cmd.Connection = DB;
           //     cmd.CommandType = System.Data.CommandType.Text;
           //     cmd.CommandText = "update log_time_config set log_time ='2'";
           //     cmd.ExecuteNonQuery();
           //     cmd.Dispose();
           //     cmd = null;
           //     DB.Close();
                
           // }

           //if(RadioButton2.Checked == true)
           // {
           //     DB.Open();
           //     MySqlCommand cmd = new MySqlCommand();
           //     cmd.Connection = DB;
           //     cmd.CommandType = System.Data.CommandType.Text;
           //     cmd.CommandText = "update log_time_config set log_time ='1'";
           //     cmd.ExecuteNonQuery();
           //     cmd.Dispose();
           //     cmd = null;
           //     DB.Close();
                
           // }
            Response.Redirect("day_traffic_main.aspx");


        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //RadioButton2.Checked = false;
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
           // RadioButton1.Checked = false;
        }

        protected void ExportToImage(object sender, EventArgs e)
        {
            string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64);
            Response.Clear();
            //Response.ContentType = "image/jpg";
            //Response.AddHeader("Content-Disposition", "attachment; filename=Result.jpg");
            Response.ContentType = "image/jpg";
            Response.AddHeader("Content-Disposition", "attachment; filename=Result.jpg");
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();


        }
    }
}