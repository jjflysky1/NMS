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

namespace WebApplication1
{
    public partial class Chart2 : System.Web.UI.Page
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {

              
           
                chart();
           
            
        }
        public int abcb = 0;
        public void chart()
        {
            // abcb++;

            try
            {
                string count = "";
                SQL = "select area_chart from Log_Time_Config";
                SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD4");
                foreach (DataRow row1 in DBSET.Tables["BD4"].Rows)
                {

                    count = row1["area_chart"].ToString();
                }




                string time1 = DateTime.Now.ToString("HH");
                string IN = "";
                //string time2 = DateTime.Now.ToString(row2["time"].ToString());
                //SQL = "select top 1 serverip, round(traffic,2) as traffic from System_Log_Traffic where serverip " +
                //    "IN (select serverip from Service where flag = '1') " +
                //    " and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = "+ time1.ToString() +"" +
                //    //" and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, getdate()), 121), 12, 2)" +
                //    " and left(time, 10) = LEFT(GETDATE(), 10) order by traffic desc";
                //SqlDataAdapter ADT3 = new SqlDataAdapter(SQL, DB);
                SqlDataAdapter ADT3 = new SqlDataAdapter("Traffic_top_server", DB);
                ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
                ADT3.SelectCommand.Parameters.AddWithValue("@time", time1);
                DataSet DBSET3 = new DataSet();
                ADT3.Fill(DBSET3, "BD3");
                string[] serverip = { };
                string temp = "";

                int str = 0;
                foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
                {

                    temp = temp + row["serverip"].ToString() + ",";
                    IN = "in";
                    if (row["serverip"].ToString() == "")
                    {

                        //그래프
                        SQL = "select top 3 serverip, round(traffic,2) as traffic from System_Log_Traffic " +
                            "where SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, getdate()), 121), 12, 2)" +
                            "  and left(time, 10) = LEFT(GETDATE(), 10) order by traffic desc";
                        SqlDataAdapter ADT4 = new SqlDataAdapter(SQL, DB);
                        DataSet DBSET4 = new DataSet();
                        ADT4.Fill(DBSET4, "BD4");
                        temp = "";
                        foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
                        {
                            temp = temp + row1["serverip"].ToString() + ",";

                        }
                    }
                }
                System.Text.RegularExpressions.Regex cntStr = new System.Text.RegularExpressions.Regex(",");
                str = int.Parse(cntStr.Matches(temp, 0).Count.ToString());
                if (str == 1)
                {

                    //그래프
                    SQL = "select top 3 serverip, round(traffic,2) as traffic from System_Log_Traffic " +
                        "where SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, getdate()), 121), 12, 2)" +
                        "  and left(time, 10) = LEFT(GETDATE(), 10) order by traffic desc";
                    SqlDataAdapter ADT4 = new SqlDataAdapter(SQL, DB);
                    DataSet DBSET4 = new DataSet();
                    ADT4.Fill(DBSET4, "BD4");
                    temp = "";
                    foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
                    {
                        temp = temp + row1["serverip"].ToString() + ",";

                    }
                }

                serverip = temp.ToString().Split(',');
                if (serverip[0] == serverip[1])
                {
                    BindChart(serverip[0], serverip[1 + 1], serverip[2 + 1], count);
                    if (serverip[1 + 1] == serverip[2 + 1])
                    {
                        BindChart(serverip[0], serverip[1 + 1], serverip[2 + 2], count);
                    }
                }
                else if (serverip[0] == serverip[2])
                {
                    BindChart(serverip[0], serverip[1], serverip[2 + 1], count);
                    if (serverip[1] == serverip[2 + 1])
                    {
                        BindChart(serverip[0], serverip[1], serverip[2 + 2], count);
                    }
                }
                else if (serverip[1] == serverip[2])
                {
                    BindChart(serverip[0], serverip[1], serverip[2 + 1], count);
                    if (serverip[0] == serverip[2 + 1])
                    {
                        BindChart(serverip[0], serverip[1], serverip[2 + 2], count);
                    }
                }
                else
                {
                    BindChart(serverip[0], serverip[1], serverip[2], count);
                }
            }
            catch(Exception e)
            {
                Label1.Text = e.Message;
              
            }
            

        }


        private DataTable GetChartData(string query)
        {

            SqlCommand cmd = new SqlCommand(query, DB);
            SqlDataAdapter ADT = new SqlDataAdapter();
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

            SqlCommand cmd2 = new SqlCommand(query2, DB);
            SqlDataAdapter ADT2 = new SqlDataAdapter();
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

            SqlCommand cmd2 = new SqlCommand(query3, DB);
            SqlDataAdapter ADT2 = new SqlDataAdapter();
            DataTable DBSET3 = new DataTable();
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = DB;
            ADT2.SelectCommand = cmd2;
            ADT2.Fill(DBSET3);
            ADT2.Dispose();
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
            
            Label1.Text = "";
            string query = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from System_Log_Traffic where " +
                "serverip = '" + serverip + "' and time between convert(varchar(16), getdate()-"+count+", 23) and convert(varchar(16), getdate()+1, 23)  order by time desc";

            DataTable dt = GetChartData(query);

            string query2 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from System_Log_Traffic where  " +
               "serverip = '" + serverip1 + "' and time between convert(varchar(16), getdate()-" + count + ", 23) and convert(varchar(16), getdate()+1, 23)  order by time desc";

            DataTable dt2 = GetChartData2(query2);

            string query3 = "select substring(convert(varchar(16), time, 120),0,16) as time, ISNULL(traffic,0) traffic, serverip from System_Log_Traffic where  " +
           "serverip = '" + serverip2 + "' and time between convert(varchar(16), getdate()-" + count + ", 23) and convert(varchar(16), getdate()+1, 23)  order by time desc";

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

            title.InnerText = "서버 Traffic 평균 차트";
            StringBuilder st = new StringBuilder();
            st.Append("<script type='text/javascript'>");
            //st.Append("var prm = Sys.WebForms.PageRequestManager.getInstance();");
            //st.Append("prm.add_beginRequest(BeginRequestHandler);");
            //st.Append("function BeginRequestHandler(sender, args){");
            //st.Append("prm._scrollPosition = null; }");
            //st.Append("prm.add_endRequest(function () {");
            st.Append("new Morris.Line({");
            st.Append("element: 'mysecchart',");
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

                st.Append("{ time: '" + x[i].Substring(5, 8) + " Hour', value: " + Math.Round((y[i] / 1024 / 1024), 2) + ", value1: " + Math.Round((y2[i] / 1024 / 1024), 2) + " " +
                    ", value2:" + Math.Round((y3[i] / 1024 / 1024), 2) + "},");

                j--;
            }
            st.Append("],");
 
            st.Append("xkey: 'time',");
            st.Append("ykeys: ['value','value1','value2'],");
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
          
            Label1.Text = st.ToString();

            st = null;

        }


      
    }

}
