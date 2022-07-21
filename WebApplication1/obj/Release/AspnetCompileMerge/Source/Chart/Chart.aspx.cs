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
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class Chart : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        soapchart.chart jsonchart = new soapchart.chart();
        protected void Page_Load(object sender, EventArgs e)
        {
            chart();

        }
        public int abcb = 0;
        public void chart()
        {
            //// abcb++;


            //string count = "";
            //SQL = "select area_chart from Log_Time_Config";
            //MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            //DataSet DBSET = new DataSet();
            //ADT.Fill(DBSET, "BD4");
            //foreach (DataRow row1 in DBSET.Tables["BD4"].Rows)
            //{

            //  count = row1["area_chart"].ToString();
            //}




            //string time1 = DateTime.Now.ToString("HH");
            //string IN = "";
            ////string time2 = DateTime.Now.ToString(row2["time"].ToString());
            ////SQL = "select top 1 serverip, round(traffic,2) as traffic from System_Log_Traffic where serverip " +
            ////    "IN (select serverip from Service where flag = '1') " +
            ////    " and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = "+ time1.ToString() +"" +
            ////    //" and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, now()), 121), 12, 2)" +
            ////    " and left(time, 10) = LEFT(now(), 10) order by traffic desc";
            ////MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            //ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            //ADT3.SelectCommand.Parameters.AddWithValue("@time1", time1);
            //DataSet DBSET3 = new DataSet();
            //ADT3.Fill(DBSET3, "BD3");
            //string[] serverip = { };
            //string temp = "";
            //foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            //{
            //    temp = temp + row["serverip"].ToString() + ",";


            //    IN = "in";

            //    if (row["serverip"].ToString() == "")
            //    {
            //        //그래프
            //        SQL = "select top 10 serverip, round(traffic,2) as traffic from Secure_Log " +
            //            "where SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, now()), 121), 12, 2)" +
            //            "  and left(time, 10) = LEFT(now(), 10) order by traffic desc";
            //        MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
            //        DataSet DBSET4 = new DataSet();
            //        ADT4.Fill(DBSET4, "BD4");
            //        temp = "";

            //        foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            //        {

            //            temp = temp + row["serverip"].ToString() + ",";

            //        }
            //    }


            //}
            //serverip = temp.ToString().Split(',');

            //if(serverip[0] == serverip[1])
            //{
            //    BindChart(serverip[0], serverip[1+1], serverip[2+1], count);
            //    if (serverip[1+1] == serverip[2 + 1])
            //    {
            //        BindChart(serverip[0], serverip[1+1], serverip[2 + 2], count);
            //    }
            //}
            //else if (serverip[0] == serverip[2])
            //{
            //    BindChart(serverip[0], serverip[1], serverip[2 + 1], count);
            //    if(serverip[1] == serverip[2 + 1])
            //    {
            //        BindChart(serverip[0], serverip[1], serverip[2 + 2], count);
            //    }
            //}
            //else if (serverip[1] == serverip[2])
            //{
            //    BindChart(serverip[0], serverip[1], serverip[2+1], count);
            //    if (serverip[0] == serverip[2 + 1])
            //    {
            //        BindChart(serverip[0], serverip[1], serverip[2 + 2], count);
            //    }
            //}
            //else
            //{
            //    BindChart(serverip[0], serverip[1], serverip[2], count);
            //}
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

        private void BindChart(string serverip, string serverip1, string serverip2, string count)
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
                "serverip = '" + serverip + "' and time between convert(varchar(16), now()-" + count + ", 23) and convert(varchar(16), now()+1, 23)  order by time desc";

            DataTable dt = GetChartData(query);

            string query2 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where  " +
               "serverip = '" + serverip1 + "' and time between convert(varchar(16), now()-" + count + ", 23) and convert(varchar(16), now()+1, 23)  order by time desc";

            DataTable dt2 = GetChartData2(query2);

            string query3 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from Secure_log where  " +
           "serverip = '" + serverip2 + "' and time between convert(varchar(16), now()-" + count + ", 23) and convert(varchar(16), now()+1, 23)  order by time desc";

            DataTable dt3 = GetChartData3(query3);

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

            //title.InnerText = "네트워크/보안 Traffic 평균 차트";
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
            }
            st.Append("],");

            st.Append("xkey: 'time',");
            st.Append("ykeys: ['a','b','c'],");
            st.Append("parseTime: false,");
            st.Append("labels: ['" + name + "','" + name1 + "','" + name2 + "'],");
            st.Append("lineColors: ['#3c8dbc','#fc8710','#819C79'],");
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


        public class Customer
        {
            public string serverip { get; set; }
            public string traffic { get; set; }
            public string time { get; set; }
        }
        [WebMethod]
        public static List<Customer> chart1()
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer> Parts = new List<Customer>();
            Parts.Clear();

            //string count = "";
            //string SQL = "select area_chart from Log_Time_Config";
            //MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            //DataSet DBSET = new DataSet();
            //ADT.Fill(DBSET, "BD");
            //foreach (DataRow row2 in DBSET.Tables["BD"].Rows)
            //{

            //    count = row2["area_chart"].ToString();
            //}

            //string time1 = DateTime.Now.ToString("HH");
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            //ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            //ADT3.SelectCommand.Parameters.AddWithValue("@time1", time1);
            //DataSet DBSET3 = new DataSet();
            //ADT3.Fill(DBSET3, "BD3");
            //string[] serverip = { };
            //string temp = "";
            //foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            //{
            //    //그래프
            //    //temp = row["serverip"].ToString()+",";
            //    temp += row["serverip"].ToString()+",";
            //}
            //serverip = temp.Split(',');
            //select distinct  serverip, sum(traffic) AS traffic from Secure_Log where time >= DATE_SUB(NOW(), INTERVAL 10 MINUTE) GROUP BY serverip order by traffic desc LIMIT 10


            string SQL1 = "select distinct  serverip, sum(traffic) AS traffic from Secure_Log where time >= DATE_SUB(NOW(), INTERVAL 10 MINUTE) GROUP BY serverip order by traffic desc LIMIT 10";
            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL1, DB);
            DataSet DBSET1 = new DataSet();
            ADT1.Fill(DBSET1, "BD");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET1.Tables["BD"].Rows)
            {
                temp += row["serverip"].ToString() + ",";
            }
            serverip = temp.Split(',');

            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + serverip[0] + "'";
            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET5 = new DataSet();
            ADT5.Fill(DBSET5, "BD");
            string mac = "";
            foreach (DataRow row in DBSET5.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }

            //일수 표현
            //string SQL2 = "select serverip, round((traffic / 1024 / 1024),2) as traffic, concat(DATE_FORMAT(time, '%e'), '일', left(DATE_FORMAT(time, '%T'),5)) as temptime from Secure_Log where serverip = " +
            // "'" + serverip[0] + "' order by time desc limit 60";
            string SQL2 = "select serverip, round((traffic / 1024 / 1024),2) as traffic, left(DATE_FORMAT(time, '%T'),5) as temptime from Secure_Log where serverip = " +
             "'" + serverip[0] + "' order by time desc limit 60";
            //and time between DATE_ADD(now(), INTERVAL -" + count + " DAY) and DATE_ADD(now(), INTERVAL 1 DAY) 
            //원래 limit 60 = 10분
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts.Add(new Customer
                {
                    serverip = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic = row1["traffic"].ToString(),
                    time = row1["temptime"].ToString()
                });
            }
            //string iresurlt = "";
            //iresurlt = JsonConvert.SerializeObject(Parts);




            return Parts;

        }
        public class Customer2
        {
            public string serverip2 { get; set; }
            public string traffic2 { get; set; }
            public string time2 { get; set; }
        }
        [WebMethod]
        public static List<Customer2> chart2()
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer2> Parts2 = new List<Customer2>();
            Parts2.Clear();

            //string count = "";
            //string SQL = "select area_chart from Log_Time_Config";
            //MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            //DataSet DBSET = new DataSet();
            //ADT.Fill(DBSET, "BD");
            //foreach (DataRow row2 in DBSET.Tables["BD"].Rows)
            //{

            //    count = row2["area_chart"].ToString();
            //}

            //string time1 = DateTime.Now.ToString("HH");
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            //ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            //ADT3.SelectCommand.Parameters.AddWithValue("@time1", time1);
            //DataSet DBSET3 = new DataSet();
            //ADT3.Fill(DBSET3, "BD3");
            //string[] serverip = { };
            //string temp = "";
            //foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            //{
            //    //그래프
            //    //temp = row["serverip"].ToString()+",";
            //    temp += row["serverip"].ToString() + ",";
            //}
            //serverip = temp.Split(',');

            string SQL1 = "select distinct  serverip, sum(traffic) AS traffic from Secure_Log where time >= DATE_SUB(NOW(), INTERVAL 10 MINUTE) GROUP BY serverip order by traffic desc LIMIT 10";
            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL1, DB);
            DataSet DBSET1 = new DataSet();
            ADT1.Fill(DBSET1, "BD");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET1.Tables["BD"].Rows)
            {
                temp += row["serverip"].ToString() + ",";
            }
            serverip = temp.Split(',');

            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + serverip[1] + "'";
            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET5 = new DataSet();
            ADT5.Fill(DBSET5, "BD");
            string mac = "";
            foreach (DataRow row in DBSET5.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }

            //string SQL2 = "select serverip, round((traffic / 1024 / 1024),2) as traffic, left(CONVERT(CHAR(8), time, 3),2) + '일' + left(CONVERT(CHAR(8), time, 24),5)  as temptime from Secure_Log where serverip = " +
            // "'" + serverip[1] + "' and time between convert(varchar(16), now()-" + count + ", 23) and convert(varchar(16), now()+1, 23)    order by time desc";
            string SQL2 = "select serverip, round((traffic / 1024 / 1024),2) as traffic, left(DATE_FORMAT(time, '%T'),5) as temptime from Secure_Log where serverip = " +
             "'" + serverip[1] + "'    order by time desc limit 60";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts2.Add(new Customer2
                {
                    serverip2 = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic2 = row1["traffic"].ToString(),
                    time2 = row1["temptime"].ToString()
                });
            }



            //string iresurlt = "";
            //iresurlt = JsonConvert.SerializeObject(Parts2);


            return Parts2;

        }

        public class Customer3
        {
            public string serverip3 { get; set; }
            public string traffic3 { get; set; }
            public string time3 { get; set; }
        }
        [WebMethod]
        public static List<Customer3> chart3()
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer3> Parts3 = new List<Customer3>();
            Parts3.Clear();

            //string count = "";
            //string SQL = "select area_chart from Log_Time_Config";
            //MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            //DataSet DBSET = new DataSet();
            //ADT.Fill(DBSET, "BD");
            //foreach (DataRow row2 in DBSET.Tables["BD"].Rows)
            //{

            //    count = row2["area_chart"].ToString();
            //}

            //string time1 = DateTime.Now.ToString("HH");
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            //ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            //ADT3.SelectCommand.Parameters.AddWithValue("@time1", time1);
            //DataSet DBSET3 = new DataSet();
            //ADT3.Fill(DBSET3, "BD3");
            //string[] serverip = { };
            //string temp = "";
            //foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            //{
            //    //그래프
            //    //temp = row["serverip"].ToString()+",";
            //    temp += row["serverip"].ToString() + ",";
            //}
            //serverip = temp.Split(',');
            //if(serverip[0] == serverip[2])
            //{
            //    serverip[2] = serverip[2 + 1];
            //}
            //if (serverip[1] == serverip[2])
            //{
            //    serverip[2] = serverip[2 + 1];
            //}

            string SQL1 = "select distinct  serverip, sum(traffic) AS traffic from Secure_Log where time >= DATE_SUB(NOW(), INTERVAL 10 MINUTE) GROUP BY serverip order by traffic desc LIMIT 10";
            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL1, DB);
            DataSet DBSET1 = new DataSet();
            ADT1.Fill(DBSET1, "BD");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET1.Tables["BD"].Rows)
            {
                temp += row["serverip"].ToString() + ",";
            }
            serverip = temp.Split(',');

            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + serverip[2] + "'";
            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET5 = new DataSet();
            ADT5.Fill(DBSET5, "BD");
            string mac = "";
            foreach (DataRow row in DBSET5.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }

            string SQL2 = "select serverip, round((traffic / 1024 / 1024),2) as traffic, left(DATE_FORMAT(time, '%T'),5) as temptime from Secure_Log where serverip = " +
             "'" + serverip[2] + "' order by time desc limit 60";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts3.Add(new Customer3
                {
                    serverip3 = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic3 = row1["traffic"].ToString(),
                    time3 = row1["temptime"].ToString()
                });
            }

            //string iresurlt = "";
            //iresurlt = JsonConvert.SerializeObject(Parts3);

            return Parts3;

        }

        public class Customer4
        {
            public string serverip4 { get; set; }
            public string traffic4 { get; set; }
            public string time4 { get; set; }
        }
        [WebMethod]
        public static List<Customer4> chart4()
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer4> Parts4 = new List<Customer4>();
            Parts4.Clear();

            string SQL1 = "select distinct  serverip, sum(traffic) AS traffic from Secure_Log where time >= DATE_SUB(NOW(), INTERVAL 10 MINUTE) GROUP BY serverip order by traffic desc LIMIT 10";
            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL1, DB);
            DataSet DBSET1 = new DataSet();
            ADT1.Fill(DBSET1, "BD");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET1.Tables["BD"].Rows)
            {
                temp += row["serverip"].ToString() + ",";
            }
            serverip = temp.Split(',');

            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + serverip[3] + "'";
            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET5 = new DataSet();
            ADT5.Fill(DBSET5, "BD");
            string mac = "";
            foreach (DataRow row in DBSET5.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }

            string SQL2 = "select serverip, round((traffic / 1024 / 1024),2) as traffic, concat(DATE_FORMAT(time, '%e'), '일', left(DATE_FORMAT(time, '%T'),5)) as temptime from Secure_Log where serverip = " +
             "'" + serverip[3] + "' order by time desc limit 60";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts4.Add(new Customer4
                {
                    serverip4 = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic4 = row1["traffic"].ToString(),
                    time4 = row1["temptime"].ToString()
                });
            }
            //string iresurlt = "";
            //iresurlt = JsonConvert.SerializeObject(Parts3);
            return Parts4;

        }
        [WebMethod]
        public static List<Customer4> chart5()
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer4> Parts4 = new List<Customer4>();
            Parts4.Clear();


            string SQL1 = "select distinct  serverip, sum(traffic) AS traffic from Secure_Log where time >= DATE_SUB(NOW(), INTERVAL 10 MINUTE) GROUP BY serverip order by traffic desc LIMIT 10";
            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL1, DB);
            DataSet DBSET1 = new DataSet();
            ADT1.Fill(DBSET1, "BD");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET1.Tables["BD"].Rows)
            {
                temp += row["serverip"].ToString() + ",";
            }
            serverip = temp.Split(',');

            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + serverip[3] + "'";
            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET5 = new DataSet();
            ADT5.Fill(DBSET5, "BD");
            string mac = "";
            foreach (DataRow row in DBSET5.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }

            string SQL2 = "select serverip, round((traffic / 1024 / 1024),2) as traffic, concat(DATE_FORMAT(time, '%e'), '일', left(DATE_FORMAT(time, '%T'),5)) as temptime from Secure_Log where serverip = " +
             "'" + serverip[3] + "'     order by time desc limit 60";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts4.Add(new Customer4
                {
                    serverip4 = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic4 = row1["traffic"].ToString(),
                    time4 = row1["temptime"].ToString()
                });
            }

            //string iresurlt = "";
            //iresurlt = JsonConvert.SerializeObject(Parts3);

            return Parts4;

        }
        [WebMethod]
        public static List<Customer4> chart6()
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer4> Parts4 = new List<Customer4>();
            Parts4.Clear();



            string SQL1 = "select distinct  serverip, sum(traffic) AS traffic from Secure_Log where time >= DATE_SUB(NOW(), INTERVAL 10 MINUTE) GROUP BY serverip order by traffic desc LIMIT 10";
            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL1, DB);
            DataSet DBSET1 = new DataSet();
            ADT1.Fill(DBSET1, "BD");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET1.Tables["BD"].Rows)
            {
                temp += row["serverip"].ToString() + ",";
            }
            serverip = temp.Split(',');

            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + serverip[3] + "'";
            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET5 = new DataSet();
            ADT5.Fill(DBSET5, "BD");
            string mac = "";
            foreach (DataRow row in DBSET5.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }

            string SQL2 = "select serverip, round((traffic / 1024 / 1024),2) as traffic, concat(DATE_FORMAT(time, '%e'), '일', left(DATE_FORMAT(time, '%T'),5)) as temptime from Secure_Log where serverip = " +
             "'" + serverip[3] + "'     order by time desc limit 60";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            temp = "";
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts4.Add(new Customer4
                {
                    serverip4 = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic4 = row1["traffic"].ToString(),
                    time4 = row1["temptime"].ToString()
                });
            }

            //string iresurlt = "";
            //iresurlt = JsonConvert.SerializeObject(Parts3);

            return Parts4;

        }

    }

}
